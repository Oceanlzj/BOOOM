using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Assets.BasicModule.Model
{
  [Serializable]
  public class Worker
  {
    public enum WorkerType
    {
      None = 0,
      Human = 1,//and more��
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

    [Category("[0] ID"), Description("ΨһID")]
    public int ID { get; set; }

    [Category("[1] Description"), Description("Ա����")]
    public string Name { get; set; }
    [Category("[1] Description"), Description("Ա������")]
    public string Description { get; set; }

    //������
    [Category("[2] Health"), Description("��ʼ����ֵ"), DisplayName("[0] Health")]
    public double Health { get; set; }
    [Category("[2] Health"), Description("����ֵ����"), DisplayName("[1] HealthMax")]
    public double HealthMax { get; set; }
    [Category("[2] Health"), Description("����ֵ�仯��"), DisplayName("[2] BasicHealthDecrease")]
    public double BasicHealthDecrease { get; set; }
    [Category("[2] San"), Description("��׼��HP�ͱ�׼ : ([0, 1])"), DisplayName("[3] NormalizedLowHealthBar")]
    public double NormalizedLowHealthBar { get; set; }

    [Category("[2] Health"), Description("���")]
    public bool IsAlive { get; set; } = true;

    //��ʳ��
    [Category("[3] Satiety"), Description("��ʼ��ʳ��"), DisplayName("[0] Satiety")]
    public double Satiety { get; set; }
    [Category("[3] Satiety"), Description("��ʳ������"), DisplayName("[1] SatietyMax")]
    public double SatietyMax { get; set; }
    [Category("[3] Satiety"), Description("��ʳ�ȱ仯��"), DisplayName("[2] BasicSatietyDecrease")]
    public double BasicSatietyDecrease { get; set; }
    [Category("[3] San"), Description("��׼��Sat�ͱ�׼ : ([0, 1])"), DisplayName("[3] NormalizedLowSatietyBar")]
    public double NormalizedLowSatietyBar { get; set; }


    //San
    [Category("[4] San"), Description("Sanֵ"), DisplayName("[0] Sanity")]
    public double Sanity { get; set; }
    [Category("[4] San"), Description("Sanֵ����"), DisplayName("[1] SanityMax")]
    public double SanityMax { get; set; }
    [Category("[4] San"), Description("Sanֵ�仯��"), DisplayName("[2] BasicSanityDecrase")]
    public double BasicSanityDecrase { get; set; }
    [Category("[4] San"), Description("��׼��San�ͱ�׼ : ([0, 1])"), DisplayName("[3] NormalizedLowSanityBar")]
    public double NormalizedLowSanBar { get; set; }



    [Category("[5] Type"), Description("Ա����������(used)")]
    public WorkerType Type { get; set; }
    [Category("[5] Type"), Description("��Ʒ���Ա���")]
    public List<WorkerFoodFavor> FoodPopertyMultiplier { get; set; } = new List<WorkerFoodFavor>();

    [Category("[6] Revolt"), Description("Ա���Ƿ񷴿�")]
    public bool WillRevolt { get; set; }


    [Category("[7] Lines"), Description("Ա����׼�Ի� - ���μ���"), DisplayName("[0] HelloLines")]
    public List<string> HelloLines { get; set; } = new List<string>();

    [Category("[7] Lines"), Description("Ա����׼�Ի� - San����"), DisplayName("[1] StandardLines_NormalSAN")]
    public List<string> StandardLines_NormalSAN { get; set; } = new List<string>();
    [Category("[7] Lines"), Description("Ա����׼�Ի� - San����"), DisplayName("[2] StandardLines_LowlSAN")]
    public List<string> StandardLines_LowlSAN { get; set; } = new();


    [Category("[7] Lines"), Description("Ա���ͽ����ȶԻ� - San����"), DisplayName("[3] LowHealthLines_NormalSAN")]
    public List<string> LowHealthLines_NormalSAN { get; set; } = new();
    [Category("[7] Lines"), Description("Ա���ͽ����ȶԻ� - San����"), DisplayName("[4] LowHealthLines_LowSAN")]
    public List<string> LowHealthLines_LowSAN { get; set; } = new();

    [Category("[7] Lines"), Description("Ա���ͱ�ʳ�ȶԻ� - San����"), DisplayName("[5] LowSatietyLines_NormalSAN")]
    public List<string> LowSatietyLines_NormalSAN { get; set; } = new();
    [Category("[7] Lines"), Description("Ա���ͱ�ʳ�ȶԻ� - San����"), DisplayName("[6] LowSatietyLines_LowSAN")]
    public List<string> LowSatietyLines_LowSAN { get; set; } = new();

    [Category("[8] Lines"), Description("Ա��0San�Ի� "), DisplayName("[7] ZeroSatietyLines")]
    public List<string> ZeroSatietyLines { get; set; } = new();

    [Category("[8] Lines"), Description("Ա�����ҶԻ� - San����"), DisplayName("[7] RevoltLine_NormalSAN")]
    public List<string> RevoltLine_NormalSAN { get; set; } = new();
    [Category("[8] Lines"), Description("Ա��0San�Ի� - San����"), DisplayName("[7] RevoltLine_LowSAN")]
    public List<string> RevoltLine_LowSAN { get; set; } = new();




    [Category("[8] Events"), Description("Ա��San�����¼�")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public GameEventManager.GameEvent OnSanIsZero { get; set; } = new();
    [Category("[8] Events"), Description("Ա�������¼�")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public GameEventManager.GameEvent OnDead { get; set; } = new();

    public double RivoltPoint
    {
      get
      {
        double R = 0.5 * NormalizedHealth * (2 * (0.4 - NormalizedSatiety) + 8 * (0.4 - NormalizedSanity));
        return WillRevolt ? 1.5 * R : R;
      }
    }

    private bool Met = false;



    public double NormalizedHealth { get { return Health / HealthMax; } }
    public double NormalizedSatiety { get { return Satiety / SatietyMax; } }
    public double NormalizedSanity { get { return Sanity / SanityMax; } }

    private List<GameEventManager.GameEvent> TriggeredGameEvents = new() { };

    //All Sentence should be displayed
    public List<string> LineBank = new List<string>();

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

      Sanity += dish.DishSanity * mul;
      Health += dish.DishHealth * mul;
      Satiety += dish.DishSatiety;
    }
    public void LoadBank()
    {
      LineBank.Clear();

      if (Sanity == 0)
      {
        LineBank.Add(ZeroSatietyLines[GameManager.Instance.RNG.Next(ZeroSatietyLines.Count)]);
        return;
      }

      //first met
      if (!Met)
      {
        LineBank.Add(HelloLines[GameManager.Instance.RNG.Next(HelloLines.Count)]);
        Met = true;
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

      if (CurrentMostEvent.ID > 500)//is presist or not
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
      else if (Met)
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
      if(GameManager.Instance.IsOnRevolt)
      {
        if(NormalizedSanity < NormalizedLowSanBar)
        {
          LineBank.Add(RevoltLine_LowSAN[GameManager.Instance.RNG.Next(RevoltLine_LowSAN.Count)]);
        }
        else
        {
          LineBank.Add(RevoltLine_NormalSAN[GameManager.Instance.RNG.Next(RevoltLine_NormalSAN.Count)]);
        }
      }







    }
    public void UpdateStatus()
    {

      double HealthAdjustment = (NormalizedSatiety - 0.3) * 40 + (NormalizedSanity - 0.3) * 20;
      Health = HealthAdjustment + Health > HealthMax ? HealthMax : HealthAdjustment + Health;

    }

  }
}