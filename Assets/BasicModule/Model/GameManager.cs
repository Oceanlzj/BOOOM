using Assets.BasicModule.Factory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace Assets.BasicModule.Model
{
  [Serializable]
  public class GameManager
  {
    public int CurrentDay { get; set; } = 0;
    public double Revolt { get; set; } = 10;

    public bool IsEndgame { get; set; } = false;


    private static Lazy<GameManager> _Inastance = new Lazy<GameManager>(() => new GameManager());
    public static GameManager Instance
    {
      get { return _Inastance.Value; }
    }

    public Random RNG { get; private set; }
    public List<Dish> CookedDish { get; set; }


    public int CurrentWorkerID;
    public double Sanity { get; set; } = 50;


    public int WorkersCountsToday { get; set; } = 1;
    public int SpecialWorkerCountsToday { get; set; } = 0;
    public int IngredientCountToday { get; set; } = 8;


    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    public Dictionary<Dish, int> DishesInventory { get; set; } = new Dictionary<Dish, int>();
    public List<Worker> Workers { get; set; } = new();
    public List<SpecialWorker> SpecialWorkers { get; set; } = new();
    public HashSet<int> WorkersToday { get; set; } = new();
    public List<EventWorkerLine> EventWorkerLines { get; set; }

    public double RevoltBar { get; set; } = 50.0;
    public double RevoltAlertBar { get; set; } = 0.8;

    public bool IsOnRevolt { get { return Revolt > RevoltBar * RevoltAlertBar; } }


    private GameManager()
    {
      RNG = new Random((int)DateTime.Now.Ticks);
      CookedDish = new();
      foreach (Worker worker in DataFactory.Instance().GetWorksers())
      {
        if (worker.ID >= 100)
        {
          SpecialWorker sw = DataFactory.Instance().GetSpecialWorker(worker.ID);
          SpecialWorkers.Add(sw);
          Workers.Add(sw);
        }
        else
        {
          Workers.Add(worker);
        }
      }
      EventWorkerLines = new List<EventWorkerLine>();
    }




    public double UpdateRevolt()
    {
      int MetCount = 0;
      int DeadCount = 0;
      double RevoltSum = 0;
      foreach (Worker wk in Workers)
      {
        if(wk.Met)
        {
          MetCount++;
          RevoltSum += wk.RevoltPoint;
          if(!wk.IsAlive)
          {
            DeadCount++;
          }
        }
      }
      Revolt = (RevoltSum + 3 * (DeadCount  / MetCount));
      return Revolt;
    }

    public void NewDay()
    {
      Ingredients.Clear();
      DishesInventory.Clear();

      WorkersToday.Clear();
      for (int i = 0; i < WorkersCountsToday; i++)
      {
        int index = RNG.Next(0, Workers.Count);
        while (WorkersToday.Contains(Workers[index].ID))
        {
          index = RNG.Next(0, Workers.Count);
        }
        WorkersToday.Add(Workers[index].ID);
      }

      for (int i = 0; i < SpecialWorkerCountsToday; i++)
      {
        int index = RNG.Next(0, SpecialWorkers.Count);
        while (WorkersToday.Contains(SpecialWorkers[index].ID))
        {
          index = RNG.Next(0, SpecialWorkers.Count);
        }
        WorkersToday.Add(SpecialWorkers[index].ID);
      }

      for (int i = 0; i < IngredientCountToday; i++)
      {
        Ingredients.Add(DataFactory.Instance().GetIngedientByID(RNG.Next(0, DataFactory.Instance().GetIngredientCount())));
      }

      CurrentDay++;
    }


    public void Accounting()
    {
      foreach(Worker worker in Workers)
      {
        if(worker.Met)
        {
          if(WorkersToday.Contains(worker.ID))
          {
            worker.UpdateStatus();
          }
        }
      }
    }

  }
}
