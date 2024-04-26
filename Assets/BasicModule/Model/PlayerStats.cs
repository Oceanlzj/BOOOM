using Assets.BasicModule.Factory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace Assets.BasicModule.Model
{
  [Serializable]
  public class PlayerStats
  {
    public List<Request> Tasks { get; set; } = new List<Request>();
    public int CurrentDay { get; set; } = 0;

    public double Revolt { get; set; } = 10;


    private static PlayerStats playerStats;
    private static Random rand;


    public int CurrentWorkerID;
    public double Sanity { get; set; } = 50;


    public int WorkersCountsToday { get; set; } = 1;
    public int SpecialWorkerCountsToday { get; set; } = 3;
    public int IngredientCountToday { get; set; } = 8;


    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    public Dictionary<Dish, int> DishesInventory { get; set; } = new Dictionary<Dish, int>();
    public List<Worker> Workers { get; set; } = new();
    public List<SpecialWorker> SpecialWorkers { get; set; } = new();
    public HashSet<int> WorkersToday { get; set; } = new();


    private PlayerStats()
    {
      rand = new Random((int)DateTime.Now.Ticks);
      foreach (Worker worker in DataFactory.Instance().GetWorksers())
      {
        if (worker.ID >= 100)
        {
          SpecialWorkers.Add(DataFactory.Instance().GetSpecialWorker(worker.ID));
        }
        else
        {
          Workers.Add(worker);
        }
      }
    }


    public static PlayerStats Instance()
    {
      if (playerStats == null)
      {
        playerStats = new PlayerStats();
      }
      return playerStats;
    }

    public double UpdateRevolt()
    {

      return Revolt;
    }

    public void NewDay()
    {
      Ingredients.Clear();
      DishesInventory.Clear();

      WorkersToday.Clear();
      for (int i = 0; i < WorkersCountsToday; i++)
      {
        int index = rand.Next(0, Workers.Count);
        while (WorkersToday.Contains(Workers[index].ID))
        {
          index = rand.Next(0, Workers.Count);
        }
        WorkersToday.Add(Workers[index].ID);
      }

      for (int i = 0; i < SpecialWorkerCountsToday; i++)
      {
        int index = rand.Next(0, SpecialWorkers.Count);
        while (WorkersToday.Contains(SpecialWorkers[index].ID))
        {
          index = rand.Next(0, SpecialWorkers.Count);
        }
        WorkersToday.Add(SpecialWorkers[index].ID);
      }

      for (int i = 0; i < IngredientCountToday; i++)
      {
        Ingredients.Add(DataFactory.Instance().GetIngedientByID(rand.Next(0, DataFactory.Instance().GetIngredientCount())));
      }

      CurrentDay++;
    }

  }
}
