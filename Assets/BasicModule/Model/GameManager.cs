using Assets.BasicModule.Factory;
using Assets.BasicModule.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace Assets.BasicModule.Model
{
  [Serializable]
  public class GameManager
  {
    public int CurrentDay { get; set; } = 0;
    public double Revolt { get; set; } = 10;

    public bool IsEndgame { get; set; } = false;


    private static Lazy<GameManager> _Inastance = new(() => new GameManager());
    public static GameManager Instance
    {
      get { return _Inastance.Value; }
    }

    public Random RNG { get; private set; }
    public List<Dish> CookedDish { get; set; }


    public int CurrentWorkerID;
    public double Sanity { get; set; } = 50;


    public int AddInWorkersCountsToday { get; set; } = 0;
    public int AddInSpecialWorkerCountsToday { get; set; } = 0;
    public int IngredientCountToday { get; set; } = 8;


    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    [JsonIgnore]
    public Dictionary<Dish, int> DishesInventory { get; set; } = new Dictionary<Dish, int>();
    public List<Worker> Workers { get; set; } = new();
    public List<SpecialWorker> SpecialWorkers { get; set; } = new();

    public HashSet<int> WorkersToday { get; set; } = new();
    public List<EventWorkerLine> EventWorkerLines { get; set; }
    public List<GameEventManager.GameEvent> GameEvents { get; set; }

    public double RevoltBar { get; set; } = 50.0;
    public double RevoltAlertBar { get; set; } = 0.8;
    public int NutrientSolutionCount { get; set; } = 5;

    public int seed { get; set; }
    public int BigEventBeginDay { get; set; }
    public int TotalDays { get; set; } = 20;
    public bool IsOnRevolt { get { return Revolt > RevoltBar * RevoltAlertBar; } }
    public List<int> BigEventPool { get; set; } = new() { 10001 };
    public List<int> NewWorkerAddCount { get; set; } = new()
    { 3,1,0,0,0,0,0,
      2,1,0,0,0,1,0,
      0,0,0,0,0,0 };
    public List<int> NewSpecialWorkerCount { get; set; } = new()
    { 1,0,0,0,1,0,0,
      0,1,0,0,0,1,0,
      0,0,0,0,0,0 };
    private GameManager()
    {
      seed = (int)DateTime.Now.Ticks;
      RNG = new Random(seed);
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
      WorkersToday.Clear();
      BigEventBeginDay = RNG.Next(7, 10);
      GameEvents = DataFactory.Instance().GetGameEvents();
      EventWorkerLines = new List<EventWorkerLine>();
    }
    public double UpdateRevolt()
    {
      int MetCount = 0;
      int DeadCount = 0;
      double RevoltSum = 0;
      foreach (Worker wk in Workers)
      {
        if (wk.Met)
        {
          MetCount++;
          RevoltSum += wk.RevoltPoint;
          if (!wk.IsAlive)
          {
            DeadCount++;
          }
        }
      }
      Revolt = (RevoltSum + 3 * (DeadCount / MetCount));
      return Revolt;
    }
    public void NewDay()
    {
      NewDayDish();
      NewDayWorkers();
      NewDayEvent();

      CurrentDay++;
    }
    public void NewDayDish()
    {
      Ingredients.Clear();
      DishesInventory.Clear();

      DishesInventory.Add(DataFactory.Instance().GetDishByID(0), NutrientSolutionCount);
      for (int i = 0; i < IngredientCountToday; i++)
      {
        Ingredients.Add(DataFactory.Instance().GetIngedientByID(RNG.Next(0, DataFactory.Instance().GetIngredientCount())));
      }

    }
    public void NewDayWorkers()
    {
      AddInWorkersCountsToday = NewWorkerAddCount[CurrentDay];
      AddInSpecialWorkerCountsToday = NewSpecialWorkerCount[CurrentDay];

      if (WorkersToday.Count(x => x < 100) + AddInWorkersCountsToday > Workers.Count(x => x.ID < 100))
      {
        AddInWorkersCountsToday = WorkersToday.Count(x => x < 100) - Workers.Count(x => x.ID < 100);
      }

      if (WorkersToday.Count(x => x > 100) + AddInSpecialWorkerCountsToday > Workers.Count(x => x.ID > 100))
      {
        AddInSpecialWorkerCountsToday = WorkersToday.Count(x => x > 100) - Workers.Count(x => x.ID > 100);
      }

      for (int i = 0; i < AddInWorkersCountsToday; i++)
      {
        int index = RNG.Next(0, Workers.Count);
        while (WorkersToday.Contains(Workers[index].ID))
        {
          index = RNG.Next(0, Workers.Count);
        }
        WorkersToday.Add(Workers[index].ID);
      }

      for (int i = 0; i < AddInSpecialWorkerCountsToday; i++)
      {
        int index = RNG.Next(0, SpecialWorkers.Count);
        while (WorkersToday.Contains(SpecialWorkers[index].ID))
        {
          index = RNG.Next(0, SpecialWorkers.Count);
        }
        WorkersToday.Add(SpecialWorkers[index].ID);
      }
    }
    public void NewDayEvent()
    {
      foreach (GameEventManager.GameEvent e in GameEventManager.Instance.Events)
      {
        if (e.NextID != -1)
        {
          if (GameEvents.Find(x => x.ID == e.NextID) != null)
          {
            GameEvents.Find(x => x.ID == e.NextID).Raise();
            e.Clear();
          }
        }
      }

      if (CurrentDay == BigEventBeginDay)
      {
        int BigEventID = BigEventPool[RNG.Next(BigEventPool.Count)];
        GameEvents.Find(x => x.ID == BigEventID).Raise();
      }

    }
    public void Accounting()
    {
      foreach (int i in WorkersToday)
      {
        Workers[Workers.IndexOf(Workers.Find(x => x.ID == i))].UpdateStatus();
      }
    }
    public void Save()
    {
      JsonWriter<GameManager>.Write(this, "Save.json");
    }
    public void Load()
    {
      JsonReader<GameManager>.Read("Save.json");
    }


  }
}
