using Assets.BasicModule.Factory;
using Assets.BasicModule.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ServePageController : MonoBehaviour
{
  // Start is called before the first frame update
  public Vector3 Begin;
  public Vector3 End;
  public int seed = 0;
  public int DishCount = 5;
  public DishPack DishPack;
  public GameObject PackFolder;

  public PlayerStats playerStats;


  public List<DishPack> packs = new();
  public List<Dish> DishOnPlate;
  void Start()
  {
    //current random inventory
    Random.InitState(seed);
    for (int i = 0; i < DishCount; i++)
    {
      playerStats.DishesInventory.Add(
          DataFactory.Instance().GetDishByID(
              (int)Random.Range(0, 11)), (int)Random.Range(1, 3)
          );
    }
    //end of temp block
    int currentDish = 0;
    DishCount = playerStats.DishesInventory.Count;
    Vector3 Step = (End - Begin) / (DishCount - 1);

    foreach (var item in playerStats.DishesInventory)
    {
      float X = Begin.x + Step.x * currentDish;
      for (int i = 0; i < item.Value; i++)
      {
        DishPack pack = Instantiate(DishPack);
        float Y = (float)(Begin.y - i * 0.4);

        pack.InitPos = End - Step * packs.Count;
        pack.InitPos.x = X;
        pack.InitPos.y = Y;

        pack.DishID = item.Key.ID;
        packs.Add(pack);
      }
      currentDish++;
    }
    DishOnPlate = new List<Dish>();
  }

  // Update is called once per frame
  void Update()
  {

  }
}
