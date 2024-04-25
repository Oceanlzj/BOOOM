using Assets.BasicModule.Factory;
using Assets.BasicModule.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class ServePageController : MonoBehaviour
{
  // Start is called before the first frame update
  public Vector3 Begin;
  public Vector3 End;

  public List<Vector3> SnapPoints;
  public List<bool> Snapped = new List<bool>();

  public int seed = 0;
  public int DishCount = 5;
  public DishPack DishPack;
  public TextMeshProUGUI TextArea;


  public int WorkerID = 0;
  public Worker worker;

  public List<Worker> workers = new List<Worker>();
  public SpecialWorker CurrentSpecialWorker;


  public GameObject PackFolder;
  public GameObject PlateFolder;
  public GameObject MsgBox;

  public Animator PlateHandAnimator;
  public Animator MsgBoxAnimator;
  public Animator FlipAnimator;
  public SpriteRenderer HandSprite;
  public SpriteLibraryAsset HandLib;

  public int Seed = 2039;

  public List<DishPack> packs = new();
  public List<DishPack> packOnPlate = new List<DishPack>();
  public List<Dish> DishOnPlate;

  public int CurrentIndex = 0;
  public bool AllDone = false;


  void Start()
  {

    //load Workers in

    foreach (int WorkerID in PlayerStats.Instance().WorkersToday)
    {
      if (WorkerID >= 100)//special
      {
        workers.Add(PlayerStats.Instance().SpecialWorkers.Find(x => x.ID == WorkerID));
      }
      else
      {
        workers.Add(PlayerStats.Instance().Workers.Find(x => x.ID == WorkerID));
      }
    }

    //System.Random random = new System.Random(seed);
    //for (int i = workers.Count - 1; i >= 0; i--)
    //{
    //  int k = random.Next(0, i + 1);

    //  Worker Temp = workers[k];
    //  workers[k] = workers[i];
    //  workers[i] = Temp;
    //}

    int currentDish = 0;
    int order = 0;
    DishCount = PlayerStats.Instance().DishesInventory.Count;
    Vector3 Step = (End - Begin) / (DishCount - 1);

    Snapped.Clear();
    for (int j = 0; j < SnapPoints.Count; j++)
    {
      Snapped.Add(false);
    }

    foreach (var item in PlayerStats.Instance().DishesInventory)
    {
      float X = Begin.x + Step.x * currentDish;
      for (int i = 0; i < item.Value; i++)
      {
        Vector3 initPos = (Begin + End) / 2;
        initPos.y -= 3;
        initPos.x = X;
        DishPack pack = Instantiate(DishPack, initPos, Quaternion.identity, PackFolder.transform);
        pack.SnapPoints = SnapPoints;

        pack.Snapped = Snapped;
        float Y = (float)(Begin.y - i * 0.4);

        pack.InitPos = End - Step * packs.Count;
        pack.InitPos.x = X;
        pack.InitPos.y = Y;

        pack.sr_Pack.sortingOrder = order;
        order++;
        pack.sr_sticker.sortingOrder = order;
        order++;

        pack.DishID = item.Key.ID;
        packs.Add(pack);
      }
      currentDish++;
    }
    DishOnPlate = new List<Dish>();
    NextWorker();

  }

  public void NextWorker()
  {
    if(CurrentIndex +1 > workers.Count)
    {
      TextArea.text = "没有别的人了……\n\n结束一天……？";
      AllDone = true;
      return;
    }
    worker = workers[CurrentIndex];
    HandSprite.sprite = HandLib.GetSprite("Worker", worker.ID.ToString());
    PlateHandAnimator.Play("HandPlateIn");
    if (worker is SpecialWorker)
    {
      CurrentSpecialWorker = (SpecialWorker)worker;
      HandSprite.sprite = HandLib.GetSprite("Special", worker.ID.ToString());
      MsgBox.SetActive(true);
      MsgBoxAnimator.Play("MsgPopIn");
    }
  }
  private void UpdateText()
  {
    TextArea.text = "";
    foreach (Dish dish in DishOnPlate)
    {
      TextArea.text += dish.Name + " - " + dish.Description + "\n";
    }
    TextArea.text += "就这样吗？";
  }
  private void OnTriggerEnter2D(Collider2D collision)
  {

    if (collision.tag == "Dish")
    {
      DishPack dp = collision.gameObject.GetComponent<DishPack>();


      dp.transform.parent = PlateFolder.transform;
      packOnPlate.Add(dp);
      DishOnPlate.Add(dp.dish);
    }
    UpdateText();
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    if (collision.tag == "Dish")
    {
      DishPack dp = collision.gameObject.GetComponent<DishPack>();
      dp.transform.parent = PackFolder.transform;
      DishOnPlate.Remove(dp.dish);
    }
    UpdateText();
  }
  // Update is called once per frame
  void Update()
  {

  }
}
