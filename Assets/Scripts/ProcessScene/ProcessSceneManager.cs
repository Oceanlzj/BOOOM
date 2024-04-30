using Assets.BasicModule.Factory;
using Assets.BasicModule.Model;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public enum ManagerStutes
{
  Unknow, Creating, Waiting, Destroy
}

public class ProcessSceneManager : Singleton<ProcessSceneManager>
{
  public IngredientItem dishProfab;
  public CookedDish CookedDish;
  public QteManager qteManager;
  //public Transform startPoint;
  public Transform[] Pipes;
  public Animator[] PipeAnimator;

  public List<Vector3> SnapPoints;
  public List<bool> Snapped = new();

  public Animator DoorAnimator;

  public TextMeshProUGUI TextArea;

  CookedDish cd;

  public bool StopText = false;


  public GameObject MachineFolder;
  public GameObject PrepFolder;

  public Collider2D Machine;

  public List<IngredientItem> IngsOnMachine;
  public List<Ingredient> IngredientsOnMachine;

  public Transform CookedDishPos;

  private bool NoMoreIngredient = false;
  private bool GoNext = false;
  private int Portion = 0;

  //public List
  public int PipeNum1;
  public int PipeNum2;

  public List<IngredientItem> IngerdientItemList;
  public int CurrentIndex = 0;


  public float seconds;
  private bool IsCompressing = false;



  void Start()
  {
    GameManager.Instance.Save();
    GameManager.Instance.NewDay();

    Snapped.Clear();
    for (int j = 0; j < SnapPoints.Count; j++)
    {
      Snapped.Add(false);
    }


    IngerdientItemList = new List<IngredientItem>();
    StartNewDay();
    DoorAnimator.Play("DoorOpen");
  }

  // Update is called once per frame
  void Update()
  {
    if (qteManager.Status == QteStatus.FinishCutting)
    {
      print(qteManager.GetCutVectorNum());
      qteManager.StartCookGame();
      return;
    }

    if (qteManager.Status == QteStatus.FinishCooking)
    {
      //qteManager.FinishCookGame();
      DishOut();
      print(qteManager.GetCookTotalTime());
      DoorAnimator.Play("DoorOpen");
      qteManager.Status = QteStatus.WaitingForCompress;
      return;
    }

    if (IsCompressing && DoorAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0)
    {
      TextArea.text = "";
      Destroy(cd.gameObject);
      if (NoMoreIngredient && IngerdientItemList.Count < 2)
      {
        TextArea.text = "没有更多食材了……\n\n去服务窗口吧";
        GoNext = true;
      }
      else
      {
        Machine.enabled = true;
      }

      IsCompressing = false;
      DoorAnimator.Play("DoorOpen");
    }

  }

  private void StartNewDay()
  {
    int curNum = 0;
    while (curNum < Pipes.Length)
    {
      CreateIngredient(curNum);
      curNum++;
    }

  }
  public void UpdateText()
  {
    TextArea.text = "";
    foreach (Ingredient dish in IngredientsOnMachine)
    {
      TextArea.text += '\u2b24' + dish.ToString() + '\n';
    }
    if (IngredientsOnMachine.Count == 2)
    {
      Dish d = DataFactory.Instance().GetDishByRecipe(IngredientsOnMachine[0].ID, IngredientsOnMachine[1].ID);
      if (GameManager.Instance.CookedDish.Exists(x => x.ID == d.ID))
      {

        TextArea.text += "\n--------\n\n" + d.ToString();
      }
      else
      {
        TextArea.text += "\n--------\n\n" + d.ToFirstString();
      }
    }
  }

  private void CreateIngredient(int number)
  {
    if (CurrentIndex < GameManager.Instance.Ingredients.Count)
    {
      PipeAnimator[number].Play("PipeOut");
      IngredientItem dish = Instantiate(dishProfab, Pipes[number].GetChild(0).position /*+ new Vector3(0, 0.5f, 0)*/, Quaternion.identity);
      dish.Ingredient = GameManager.Instance.Ingredients[CurrentIndex];
      IngerdientItemList.Add(dish);
      dish.SetPos(Pipes[number].GetChild(1).position + new Vector3(0, 1, 0));
      dish.PipeNum = number;

      dish.machine = Machine;
      dish.Snapped = Snapped;
      dish.SnapPoints = SnapPoints;
      dish.transform.parent = PrepFolder.transform;

      dish.SetStatu(DishStatus.Creating);

      CurrentIndex++;
    }
    else
    {
      NoMoreIngredient = true;
    }

  }


  public void RemoveIngerdient(IngredientItem dish)
  {
    IngerdientItemList.Remove(dish);
  }

  public void DishOut()
  {

    Dish dish = DataFactory.Instance().GetDishByRecipe(IngredientsOnMachine[0].ID, IngredientsOnMachine[1].ID);
    GameManager.Instance.CookedDish.Add(dish);
    Destroy(IngsOnMachine[1].gameObject);
    Destroy(IngsOnMachine[0].gameObject);

    //cook Dish
    cd = Instantiate(CookedDish, CookedDishPos);
    cd.Dish = dish;
    IngredientsOnMachine.Clear();
    Machine.enabled = false;

    //new 

    Portion = (int)((qteManager.GetCutVectorNum() * qteManager.GetCookTotalTime()) + 1);
    TextArea.text = dish.ToString() + " x " + Portion;
    TextArea.text += "\n\n确定压缩？";

  }

  public void OnYesClicked()
  {
    if (GoNext)
    {
      //goto serve scene(CoreScene)
      SceneManager.LoadScene(4);
      return;
    }

    if (Machine.enabled && qteManager.Status == QteStatus.Waiting)
    {
      //cook ani
      PipeNum1 = IngsOnMachine[0].PipeNum;
      PipeNum2 = IngsOnMachine[1].PipeNum;
      RemoveIngerdient(IngsOnMachine[0]);
      RemoveIngerdient(IngsOnMachine[1]);
      CreateIngredient(PipeNum1);
      CreateIngredient(PipeNum2);


      //cook destory
      if (IngsOnMachine.Count < 2) { return; }
      IngsOnMachine[0]._status = DishStatus.Cooking;
      IngsOnMachine[1]._status = DishStatus.Cooking;

      DoorAnimator.Play("DoorClose");

      qteManager.StartCutGame();


    }
    else if (qteManager.Status == QteStatus.WaitingForCompress)
    {

      if (GameManager.Instance.DishesInventory.ContainsKey(cd.Dish))
      {
        GameManager.Instance.DishesInventory[cd.Dish] += Portion;
      }
      else
      {
        GameManager.Instance.DishesInventory.Add(cd.Dish, Portion);
      }

      DoorAnimator.Play("DoorClose");
      IsCompressing = true;
      qteManager.Status = QteStatus.Waiting;



    }




  }
  public void OnNoClicked()
  {
    if (Machine.enabled && qteManager.Status == QteStatus.Waiting)
    {
      foreach (IngredientItem item in IngsOnMachine)
      {
        item.UnSelect();

      }
      IngredientsOnMachine.Clear();
    }
    else
    {

    }
  }
}

