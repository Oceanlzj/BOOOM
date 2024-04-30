﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using Unity.Mathematics;

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

    [Category("[1] Description"), Description("员工名")]
    public string Name { get; set; }
    [Category("[1] Description"), Description("员工描述")]
    public string Description { get; set; }

    //健康度
    [Category("[2] Health"), Description("初始健康值"), DisplayName("[0] Health")]
    public double Health { get; set; }
    [Category("[2] Health"), Description("健康值上限"), DisplayName("[1] HealthMax")]
    public double HealthMax { get; set; }
    [Category("[2] Health"), Description("健康值变化量"), DisplayName("[2] BasicHealthDecrease")]
    public double BasicHealthDecrease { get; set; }
    [Category("[2] San"), Description("标准化HP低标准 : ([0, 1])"), DisplayName("[3] NormalizedLowHealthBar")]
    public double NormalizedLowHealthBar { get; set; }

    [Category("[2] Health"), Description("存活")]
    public bool IsAlive { get; set; } = true;

    //饱食度
    [Category("[3] Satiety"), Description("初始饱食度"), DisplayName("[0] Satiety")]
    public double Satiety { get; set; }
    [Category("[3] Satiety"), Description("饱食度上限"), DisplayName("[1] SatietyMax")]
    public double SatietyMax { get; set; }
    [Category("[3] Satiety"), Description("饱食度变化量"), DisplayName("[2] BasicSatietyDecrease")]
    public double BasicSatietyDecrease { get; set; }
    [Category("[3] San"), Description("标准化Sat低标准 : ([0, 1])"), DisplayName("[3] NormalizedLowSatietyBar")]
    public double NormalizedLowSatietyBar { get; set; }


    //San
    [Category("[4] San"), Description("San值"), DisplayName("[0] Sanity")]
    public double Sanity { get; set; }
    [Category("[4] San"), Description("San值上限"), DisplayName("[1] SanityMax")]
    public double SanityMax { get; set; }
    [Category("[4] San"), Description("San值变化量"), DisplayName("[2] BasicSanityDecrase")]
    public double BasicSanityDecrase { get; set; }
    [Category("[4] San"), Description("标准化San低标准 : ([0, 1])"), DisplayName("[3] NormalizedLowSanityBar")]
    public double NormalizedLowSanBar { get; set; }



    [Category("[5] Type"), Description("员工生物类型(used)")]
    public WorkerType Type { get; set; }
    [Category("[5] Type"), Description("菜品属性倍率")]
    public List<WorkerFoodFavor> FoodPopertyMultiplier { get; set; } = new List<WorkerFoodFavor>();

    [Category("[6] Revolt"), Description("员工是否反抗")]
    public bool WillRevolt { get; set; }


    [Category("[7] Lines"), Description("员工标准对话 - 初次见面"), DisplayName("[0] HelloLines")]
    public List<string> HelloLines { get; set; } = new List<string>();

    [Category("[7] Lines"), Description("员工标准对话 - San正常"), DisplayName("[1] StandardLines_NormalSAN")]
    public List<string> StandardLines_NormalSAN { get; set; } = new List<string>();
    [Category("[7] Lines"), Description("员工标准对话 - San低下"), DisplayName("[2] StandardLines_LowlSAN")]
    public List<string> StandardLines_LowlSAN { get; set; } = new();


    [Category("[7] Lines"), Description("员工低健康度对话 - San正常"), DisplayName("[3] LowHealthLines_NormalSAN")]
    public List<string> LowHealthLines_NormalSAN { get; set; } = new();
    [Category("[7] Lines"), Description("员工低健康度对话 - San低下"), DisplayName("[4] LowHealthLines_LowSAN")]
    public List<string> LowHealthLines_LowSAN { get; set; } = new();

    [Category("[7] Lines"), Description("员工低饱食度对话 - San正常"), DisplayName("[5] LowSatietyLines_NormalSAN")]
    public List<string> LowSatietyLines_NormalSAN { get; set; } = new();
    [Category("[7] Lines"), Description("员工低饱食度对话 - San低下"), DisplayName("[6] LowSatietyLines_LowSAN")]
    public List<string> LowSatietyLines_LowSAN { get; set; } = new();

    [Category("[8] Lines"), Description("员工0San对话 "), DisplayName("[7] ZeroSatietyLines")]
    public List<string> ZeroSatietyLines { get; set; } = new();

    [Category("[8] Lines"), Description("员工暴乱对话 - San正常"), DisplayName("[7] RevoltLine_NormalSAN")]
    public List<string> RevoltLine_NormalSAN { get; set; } = new();
    [Category("[8] Lines"), Description("员工0San对话 - San低下"), DisplayName("[7] RevoltLine_LowSAN")]
    public List<string> RevoltLine_LowSAN { get; set; } = new();




    [Category("[8] Events"), Description("员工San归零事件")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public GameEventManager.GameEvent OnSanIsZero { get; set; } = new();
    [Category("[8] Events"), Description("员工死亡事件")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public GameEventManager.GameEvent OnDead { get; set; } = new();

    public double RevoltPoint
    {
      get
      {
        double R = 0.5 * NormalizedHealth * (2 * (0.4 - NormalizedSatiety) + 8 * (0.4 - NormalizedSanity));
        return WillRevolt ? 1.5 * R : R;
      }
    }

    public bool Met = false;




    public double NormalizedHealth { get { return Health / HealthMax; } }
    public double NormalizedSatiety { get { return Satiety / SatietyMax; } }
    public double NormalizedSanity { get { return Sanity / SanityMax; } }

    private List<GameEventManager.GameEvent> TriggeredGameEvents = new() { };

    //All Sentence should be displayed
    public List<string> LineBank = new List<string>();

    public void Eat(Dish dish)
    {
      double mul = 1.0;
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

      if (Sanity > 0)
      {
        Sanity += dish.DishSanity * mul;
        Sanity = Sanity > SanityMax ? SanityMax : Sanity;
      }

      Health += dish.DishHealth * mul;
      Health = Health > HealthMax ? HealthMax : Health;
      Satiety += dish.DishSatiety;
      Satiety = Satiety > SatietyMax ? SatietyMax : Satiety;
    }
    public void LoadBank()
    {
      LineBank.Clear();
      try
      {
        if (Sanity == 0)
        {
          LineBank.Add(ZeroSatietyLines[GameManager.Instance.RNG.Next(ZeroSatietyLines.Count)]);
          return;
        }

        //first met
        if (!Met)
        {
          LineBank.Add(HelloLines[GameManager.Instance.RNG.Next(HelloLines.Count)]);
        }

        //Dying
        if (NormalizedHealth < NormalizedLowHealthBar)
        {
          if (NormalizedSanity < NormalizedLowSanBar)
          {
            LineBank.Add(LowHealthLines_LowSAN[GameManager.Instance.RNG.Next(LowHealthLines_LowSAN.Count)]);
          }
          else
          {
            LineBank.Add(LowHealthLines_NormalSAN[GameManager.Instance.RNG.Next(LowHealthLines_NormalSAN.Count)]);
          }
          return;
        }

        //low sat
        if (NormalizedSatiety < NormalizedLowSatietyBar)
        {
          if (NormalizedSanity < NormalizedLowSanBar)
          {
            LineBank.Add(LowSatietyLines_LowSAN[GameManager.Instance.RNG.Next(LowSatietyLines_LowSAN.Count)]);
          }
          else
          {
            LineBank.Add(LowSatietyLines_NormalSAN[GameManager.Instance.RNG.Next(LowSatietyLines_NormalSAN.Count)]);
          }
        }

        //event
        GameEventManager.GameEvent CurrentMostEvent = new();
        foreach (GameEventManager.GameEvent Event in GameEventManager.Instance.Events)
        {
          if (!TriggeredGameEvents.Contains(Event))
          {
            CurrentMostEvent = Event;
            break;
          }
        }

        if (CurrentMostEvent.ID < 10000)//is presist or not
        {
          TriggeredGameEvents.Add(CurrentMostEvent);
        }

        EventWorkerLine ewl = GameManager.Instance.EventWorkerLines.Find(x => x.WorkerID == ID && x.EventID == CurrentMostEvent.ID);
        if (ewl != null)
        {
          if (NormalizedSanity < NormalizedLowSanBar)
          {
            LineBank.Add(ewl.Lines_LowSan[GameManager.Instance.RNG.Next(ewl.Lines_LowSan.Count)]);
          }
          else
          {
            LineBank.Add(ewl.Lines_NormalSan[GameManager.Instance.RNG.Next(ewl.Lines_NormalSan.Count)]);
          }

        }
        else if (Met && !GameManager.Instance.IsOnRevolt)
        {
          if (NormalizedSanity < NormalizedLowSanBar)
          {
            LineBank.Add(StandardLines_LowlSAN[GameManager.Instance.RNG.Next(StandardLines_LowlSAN.Count)]);
          }
          else
          {
            LineBank.Add(StandardLines_NormalSAN[GameManager.Instance.RNG.Next(StandardLines_NormalSAN.Count)]);
          }
        }

        //On Revolt
        if (GameManager.Instance.IsOnRevolt)
        {
          if (NormalizedSanity < NormalizedLowSanBar)
          {
            LineBank.Add(RevoltLine_LowSAN[GameManager.Instance.RNG.Next(RevoltLine_LowSAN.Count)]);
          }
          else
          {
            LineBank.Add(RevoltLine_NormalSAN[GameManager.Instance.RNG.Next(RevoltLine_NormalSAN.Count)]);
          }
        }
        if (!Met) { Met = true; }


      }
      catch { }




    }

    public void EndDay()
    {
      Health = Health + BasicHealthDecrease < 0 ? 0 : Health + BasicHealthDecrease;
      Sanity = Sanity + BasicSanityDecrase < 0 ? 0 : Sanity + BasicSanityDecrase;
      Satiety = Satiety + BasicSatietyDecrease < 0 ? 0 : Satiety + BasicSanityDecrase;
    }
    public void UpdateStatus()
    {
      EndDay();

      double HealthAdjustment = (6 * (NormalizedSatiety - 0.85) * (NormalizedSatiety - 0.85) * (NormalizedSatiety - 0.85) +
                                  5.6 * (NormalizedSanity - 0.85) * (NormalizedSanity - 0.85) * (NormalizedSanity - 0.85)) * HealthMax;
      Health += HealthAdjustment;



      if (Health <= 0)
      {
        IsAlive = false;
        OnDead.Raise();
      }

      if (Sanity <= 0)
      {
        Sanity = 0;
        if (GameManager.Instance.RNG.Next(0, 10) < 3)
        {
          OnSanIsZero.Raise();
        }
        SanityMax = 999999;
      }

    }

    override public string ToString()
    {
      string HPBar = "健康\u2624";
      string SatBar = "饱食\u2615";
      string SanBar = "理智\u26ef";

      for (int i = 0; i < NormalizedHealth * 10; i++) { HPBar += '\u2b1b'; }
      while (HPBar.Length < 12) { HPBar += '\u2b1c'; }
      HPBar += "\n";

      for (int i = 0; i < NormalizedSatiety * 10; i++) { SatBar += '\u2b1b'; }
      while (SatBar.Length < 12) { SatBar += "\u2b1c"; }
      SatBar += "\n";

      for (int i = 0; i < NormalizedSanity * 10; i++) { SanBar += "\u2b1b"; }
      while (SanBar.Length < 12) { SanBar += "\u2b1c"; }
      SanBar += "\n";


      string a = Name + "\n" + HPBar + SatBar + SanBar;
      return a;
    }
  }


}