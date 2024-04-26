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

    public enum WorkerStatus
    {
      Good = 0,
      Normal = 1,
      Dying = 2
    };
    public class WorkerFoodFavor
    {
      public FoodProperty foodProperty { get; set; }
      public double Multiplier { get; set; }
    }

    [Category("[0] ID"), Description("唯一ID")]
    public int ID { get; set; }
    [Category("[6] Type"), Description("员工生物类型")]
    public WorkerType Type { get; set; }
    [Category("[2] Description"), Description("员工名")]
    public string Name { get; set; }
    [Category("[2] Description"), Description("员工描述")]
    public string Description { get; set; }

    //健康度
    [Category("[3] Health"), Description("初始健康值"), DisplayName("[0] Health")]
    public double Health { get; set; }
    [Category("[3] Health"), Description("健康值上限"), DisplayName("[1] HealthMax")]
    public double HealthMax { get; set; }
    [Category("[3] Health"), Description("健康值变化量"), DisplayName("[2] BasicHealthDecrease")]
    public double BasicHealthDecrease { get; set; }

    [Category("[3] Health"), Description("健康值变化倍率"), DisplayName("[3] HealthMultiplier")]
    public double HealthMultiplier { get; set; }

    //饱食度
    [Category("[4] Satiety"), Description("初始饱食度"), DisplayName("[0] Satiety")]
    public double Satiety { get; set; }
    [Category("[4] Satiety"), Description("饱食度上限"), DisplayName("[1] SatietyMax")]
    public double SatietyMax { get; set; }
    [Category("[4] Satiety"), Description("饱食度变化量"), DisplayName("[2] BasicSatietyDecrease")]
    public double BasicSatietyDecrease { get; set; }

    [Category("[4] Satiety"), Description("饱食度变化倍率"), DisplayName("[3] SanietyMultiplier")]
    public double SanietyMultiplier { get; set; }

    //San
    [Category("[5] San"), Description("San值"), DisplayName("[0] Sanity")]
    public double Sanity { get; set; }
    [Category("[5] San"), Description("San值上限"), DisplayName("[1] SanityMax")]
    public double SanityMax { get; set; }
    [Category("[5] San"), Description("San值变化量"), DisplayName("[2] BasicSanityDecrase")]
    public double BasicSanityDecrase { get; set; }

    [Category("[5] San"), Description("San值变化倍率"), DisplayName("[3] SanityMultiplier")]
    public double SanityMultiplier { get; set; }

    [Category("[3] Health"), Description("存活")]
    public bool IsAlive { get; set; } = true;
    [Category("[6] Type"), Description("菜品属性倍率")]
    public List<WorkerFoodFavor> FoodPopertyMultiplier { get; set; } = new List<WorkerFoodFavor>();

    [Category("[8] LineBank"), Description("正常数值员工对话")]
    private List<string> NormalLineBank { get; set; } = new();
    [Category("[8] LineBank"), Description("濒死员工对话")]
    private List<string> DyingLineBank { get; set; } = new();
    [Category("[8] LineBank"), Description("良好员工对话")]
    private List<string> GoodLineBank { get; set; } = new();

    [Category("[7] Contest"), Description("员工是否参与暴乱")]
    public bool IsParticatedInContest { get; set; }




    private List<string>[] LineBanks;

    public Worker()
    {
      LineBanks = new List<string>[3];
      LineBanks[(int)WorkerStatus.Good] = GoodLineBank;
      LineBanks[(int)WorkerStatus.Normal] = NormalLineBank;
      LineBanks[(int)WorkerStatus.Dying] = DyingLineBank;
    }
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
    public void UpdateStatus()
    {
      if()
    }
  }
}