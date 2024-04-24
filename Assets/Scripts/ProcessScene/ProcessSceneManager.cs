using Assets.BasicModule.Factory;
using Assets.BasicModule.Model;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public enum ManagerStutes
{
  Unknow, Creating, Waiting, Destroy
}

public class ProcessSceneManager : Singleton<ProcessSceneManager>
{
  public IngredientItem dishProfab;
  public CookedDish CookedDish;
  //public Transform startPoint;
  public Transform[] Pipes;

  public List<Vector3> SnapPoints;
  public List<bool> Snapped = new();

  public TextMeshProUGUI TextArea;

  CookedDish cd;

  public bool StopText = false;


  public GameObject MachineFolder;
  public GameObject PrepFolder;

  public Collider2D Machine;

  public List<IngredientItem> IngsOnMachine;
  public List<Ingredient> IngredientsOnMachine;

  private bool NoMoreIngredient = false;
  private bool GoNext = false;
  private int Portion = 0;

  //public List
  public int PipeNum1;
  public int PipeNum2;

  private List<IngredientItem> _dishesList;
  public int CurrentIndex = 0;

  public float seconds;       // 创建时间间隔


  private ManagerStutes _stutes;

  void Start()
  {
    PlayerStats.Instance().NewDay();

    Snapped.Clear();
    for (int j = 0; j < SnapPoints.Count; j++)
    {
      Snapped.Add(false);
    }


    _dishesList = new List<IngredientItem>();
    _stutes = ManagerStutes.Creating;
    StartCoroutine(StartNewDay());
  }

  // Update is called once per frame
  void Update()
  {
    if (CurrentIndex < PlayerStats.Instance().Ingredients.Count && _stutes == ManagerStutes.Destroy && _dishesList.Count < Pipes.Length)
    {
      //CreateDish(_destroyNum);
      //_destroyNum = 0;
      //_stutes = ManagerStutes.Waiting;
    }
  }

  private IEnumerator StartNewDay()
  {
    int curNum = 0;
    while (curNum < Pipes.Length)
    {
      CreateIngredient(curNum);
      curNum++;
      yield return new WaitForSeconds(seconds);
    }

    _stutes = ManagerStutes.Waiting;
  }
  public void UpdateText()
  {
    TextArea.text = "";
    foreach (Ingredient dish in IngredientsOnMachine)
    {
      TextArea.text += dish.Name + " - " + dish.Description + "\n";
    }
    TextArea.text += "就这样吗？";
  }

  private void CreateIngredient(int number)
  {
    if (CurrentIndex < PlayerStats.Instance().Ingredients.Count)
    {
      IngredientItem dish = Instantiate(dishProfab, Pipes[number].GetChild(0).position, Quaternion.identity);
      dish.Ingredient = PlayerStats.Instance().Ingredients[CurrentIndex];
      _dishesList.Add(dish);
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
    _dishesList.Remove(dish);
    _stutes = ManagerStutes.Destroy;
  }


  public void OnYesClicked()
  {
    if (GoNext)
    {
      //goto serve scene(CoreScene)

      return;
    }

    if (Machine.enabled)
    {
      //cook ani
      //cook destory
      if (IngsOnMachine.Count < 2) { return; }
      PipeNum1 = IngsOnMachine[0].PipeNum;
      PipeNum2 = IngsOnMachine[1].PipeNum;
      try
      {
        Dish dish = DataFactory.Instance().GetDishByRecipe(IngredientsOnMachine[0].ID, IngredientsOnMachine[1].ID);
        Portion = Random.Range(1, 3);

        Destroy(IngsOnMachine[1].gameObject);
        Destroy(IngsOnMachine[0].gameObject);

        //cook Dish
        cd = Instantiate(CookedDish);
        cd.Dish = dish;
        IngredientsOnMachine.Clear();
        Machine.enabled = false;

        //new 
        CreateIngredient(PipeNum1);
        CreateIngredient(PipeNum2);

        TextArea.text = "确定压缩？";


      }
      catch
      {
        StopText = true;
        TextArea.text = "无效的配方，请重试！";
        if(_dishesList.Count == 2)
        {
          
          TextArea.text = "没有更多可料理的了……\n\n 去服务窗口吧";
          GoNext = true;
        }
        OnNoClicked();
      }
    }
    else
    {

      PlayerStats.Instance().DishesInventory.Add(cd.Dish, Portion);
      Destroy(cd.gameObject);
      if (NoMoreIngredient && _dishesList.Count < 2)
      {
        TextArea.text = "没有更多食材了……\n\n 去服务窗口吧";
        GoNext = true;
      }
      else
      {
        Machine.enabled = true;
      }
    }




  }
  public void OnNoClicked()
  {
    if (Machine.enabled)
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

