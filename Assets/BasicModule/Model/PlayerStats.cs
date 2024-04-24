using Assets.BasicModule.Factory;
using System;
using System.Collections;
using System.Collections.Generic;
namespace Assets.BasicModule.Model
{
  [Serializable]
  public class PlayerStats
  {
    public List<Request> Tasks { get; set; } = new List<Request>();
    public int CurrentDay { get; set; } = 0;

    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    public Dictionary<Dish, int> DishesInventory { get; set; } = new Dictionary<Dish, int>();

    private static PlayerStats playerStats;
    private static int Seed = 20;
    private static Random rand = new Random(Seed);

    public void SetSeed(int seed)
    {
      Seed = seed;
    }
    private PlayerStats()
    {
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




    public double Sanity { get; set; } = 50;

    public List<Worker> Workers { get; set; } = new();
    public List<SpecialWorker> SpecialWorkers { get; set; } = new();

    public int WorkersCountsToday { get; set; } = 1;
    public int SpecialWorkerCountsToday { get; set; } = 2;
    public int IngredientCountToday { get; set; } = 6;
    public HashSet<int> WorkersToday { get; set; } = new();



    public void NewDay()
    {
      Ingredients.Clear();
      DishesInventory.Clear();

      WorkersToday.Clear();
      for (int i = 0; i < WorkersCountsToday; i++)
      {
        int index = rand.Next(0, Workers.Count);
        while (WorkersToday.Contains(index))
        {
          index = rand.Next(0, Workers.Count);
        }
        WorkersToday.Add(index);
      }

      for (int i = 0; i < SpecialWorkerCountsToday; i++)
      {
        int index = rand.Next(100, 100 + SpecialWorkers.Count);
        while (WorkersToday.Contains(index))
        {
          index = rand.Next(100, 100 + SpecialWorkers.Count);
        }
        WorkersToday.Add(index);
      }

      for(int i = 0; i < IngredientCountToday; i++)
      {
        Ingredients.Add(DataFactory.Instance().GetIngedientByID(rand.Next(0, DataFactory.Instance().GetIngredientCount())));
      }

      CurrentDay++;
    }

  }
}
