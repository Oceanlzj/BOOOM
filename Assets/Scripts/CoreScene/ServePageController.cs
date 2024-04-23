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



  public GameObject PackFolder;
  public GameObject PlateFolder;
  public GameObject MsgBox;

  public Animator PlateHandAnimator;
  public Animator MsgBoxAnimator;
  public Animator FlipAnimator;
  public SpriteRenderer HandSprite;
  public SpriteLibraryAsset HandLib;


  public List<DishPack> packs = new();
  public List<DishPack> packOnPlate = new List<DishPack>();
  public List<Dish> DishOnPlate;
  void Start()
  {
    //current random inventory
    Random.InitState(seed);
    for (int i = 0; i < DishCount; i++)
    {
      PlayerStats.Instance().DishesInventory.Add(DataFactory.Instance().GetDishByID(i), Random.Range(1, 5));
    }
    NextWorker();

    //end of temp block
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
  }

  public void NextWorker()
  {
    WorkerID = Random.Range(0, 7);
    worker = DataFactory.Instance().GetWorkerByID(WorkerID);

    HandSprite.sprite = HandLib.GetSprite("Worker", WorkerID.ToString());
    PlateHandAnimator.Play("HandPlateIn");
    MsgBoxAnimator.Play("MsgPopIn");

  }
  private void UpdateText()
  {
    TextArea.text = "";
    foreach (Dish dish in DishOnPlate)
    {
      TextArea.text += dish.Name + " - " + dish.Description + "\n";
    }
    TextArea.text += "¾ÍÕâÑùÂð£¿";
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
