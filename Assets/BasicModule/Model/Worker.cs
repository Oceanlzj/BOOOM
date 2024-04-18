using System;
using System.Collections.Generic;

namespace Assets.BasicModule.Model
{
  [Serializable]
  public class Worker
  {
    public enum WorkerType
    {
      None = 0,
      Human = 1//and more
    }

    public int ID { get; set; }
    public WorkerType Type { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    //½¡¿µ¶È
    public double Health { get; set; }
    public double BasicHealthDecrease { get; set; }
    public double HealthMultiplier { get; set; }

    //±¥Ê³¶È
    public double Satiety { get; set; }
    public double BasicSatietyDecrease { get; set; }
    public double SanietyMultiplier { get; set; }

    //San
    public double Sanity { get; set; }
    public double BasicSanityDecrase { get; set; }
    public double SanityMultiplier { get; set; }

    public bool IsAlive { get; set; }

    public Worker() { }
    public Worker(int iD, WorkerType type, string name, string description, double health, double basicHealthDecrease, double healthMultiplier, double satiety, double basicSatietyDecrease, double sanietyMultiplier, double sanity, double basicSanityDecrase, double sanityMultiplier, bool isAlive, Dictionary<FoodProperty, double> foodPopertyMultiplier)
    {
      ID = iD;
      Type = type;
      Name = name;
      Description = description;
      Health = health;
      BasicHealthDecrease = basicHealthDecrease;
      HealthMultiplier = healthMultiplier;
      Satiety = satiety;
      BasicSatietyDecrease = basicSatietyDecrease;
      SanietyMultiplier = sanietyMultiplier;
      Sanity = sanity;
      BasicSanityDecrase = basicSanityDecrase;
      SanityMultiplier = sanityMultiplier;
      IsAlive = isAlive;
      FoodPopertyMultiplier = foodPopertyMultiplier;
    }

    public Dictionary<FoodProperty, double> FoodPopertyMultiplier { get; set; }

    public void Eat(Dish dish)
    {
      double mul = 0.0;
      foreach (FoodProperty fp in dish.Properties)
      {
        if (FoodPopertyMultiplier.ContainsKey(fp))
        { mul += FoodPopertyMultiplier[fp]; }
      }

      Sanity += dish.DishSanity * dish.Successfulness * SanityMultiplier * mul;
      Health += dish.DishHealth * dish.Successfulness * HealthMultiplier * mul;
      Satiety += dish.DishSatiety * dish.Successfulness * SanietyMultiplier;
    }

  }
}