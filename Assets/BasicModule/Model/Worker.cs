using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Assets.BasicModule.Model
{
  [Serializable]
  public class Worker
  {
    public enum WorkerType
    {
      None = 0,
      Human = 1,//and more，
    }
    public class WorkerFoodFavor
    {
      public FoodProperty foodProperty { get; set; }
      public double Multiplier { get; set; }
    }

    [Category("ID"), Description("唯一ID")]
    public int ID { get; set; }
    [Category("Type"), Description("员工生物类型")]
    public WorkerType Type { get; set; }
    [Category("Description"), Description("员工名")]
    public string Name { get; set; }
    [Category("Description"), Description("员工描述")]
    public string Description { get; set; }

    //健康度
    [Category("Health"), Description("初始健康值")]
    public double Health { get; set; }
    [Category("Health"), Description("健康值上限")]
    public double HealthMax { get; set; }
    [Category("Health"), Description("健康值变化量")]
    public double BasicHealthDecrease { get; set; }
    [Category("Health"), Description("健康值变化倍率")]
    public double HealthMultiplier { get; set; }

    //饱食度
    [Category("Satiety"), Description("初始饱食度")]
    public double Satiety { get; set; }
    [Category("Satiety"), Description("饱食度上限")]
    public double SatietyMax { get; set; }
    [Category("Satiety"), Description("饱食度变化量")]
    public double BasicSatietyDecrease { get; set; }
    [Category("Satiety"), Description("饱食度变化倍率")]
    public double SanietyMultiplier { get; set; }

    //San
    [Category("San"), Description("San值")]
    public double Sanity { get; set; }
    [Category("San"), Description("San值上限")]
    public double SanityMax {  get; set; }
    [Category("San"), Description("San值变化量")]
    public double BasicSanityDecrase { get; set; }
    [Category("San"), Description("San值变化倍率")]
    public double SanityMultiplier { get; set; }

    [Category("Health"), Description("存活")]
    public bool IsAlive { get; set; } = true;
    [Category("Type"), Description("菜品属性倍率")]
    public List<WorkerFoodFavor> FoodPopertyMultiplier { get; set; } = new List<WorkerFoodFavor>();

    public void Eat(Dish dish)
    {
      double mul = 0.0;
      foreach (FoodProperty fp in dish.Properties)
      {
        foreach (WorkerFoodFavor favor in FoodPopertyMultiplier)
        {
          if (favor.foodProperty == fp)
          {
            mul += favor.Multiplier;
          }
        }
      }

      Sanity += dish.DishSanity * dish.Successfulness * SanityMultiplier * mul;
      Health += dish.DishHealth * dish.Successfulness * HealthMultiplier * mul;
      Satiety += dish.DishSatiety * dish.Successfulness * SanietyMultiplier;
    }

  }
}