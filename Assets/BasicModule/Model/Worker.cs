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
    [Category("[6] Type"), Description("Ա����������")]
    public WorkerType Type { get; set; }
    [Category("[2] Description"), Description("Ա����")]
    public string Name { get; set; }
    [Category("[2] Description"), Description("Ա������")]
    public string Description { get; set; }

    //������
    [Category("[3] Health"), Description("��ʼ����ֵ"), DisplayName("[0] Health")]
    public double Health { get; set; }
    [Category("[3] Health"), Description("����ֵ����"), DisplayName("[1] HealthMax")]
    public double HealthMax { get; set; }
    [Category("[3] Health"), Description("����ֵ�仯��"), DisplayName("[2] BasicHealthDecrease")]
    public double BasicHealthDecrease { get; set; }

    [Category("[3] Health"), Description("����ֵ�仯����"), DisplayName("[3] HealthMultiplier")]
    public double HealthMultiplier { get; set; }

    //��ʳ��
    [Category("[4] Satiety"), Description("��ʼ��ʳ��"), DisplayName("[0] Satiety")]
    public double Satiety { get; set; }
    [Category("[4] Satiety"), Description("��ʳ������"), DisplayName("[1] SatietyMax")]
    public double SatietyMax { get; set; }
    [Category("[4] Satiety"), Description("��ʳ�ȱ仯��"), DisplayName("[2] BasicSatietyDecrease")]
    public double BasicSatietyDecrease { get; set; }

    [Category("[4] Satiety"), Description("��ʳ�ȱ仯����"), DisplayName("[3] SanietyMultiplier")]
    public double SanietyMultiplier { get; set; }

    //San
    [Category("[5] San"), Description("Sanֵ"), DisplayName("[0] Sanity")]
    public double Sanity { get; set; }
    [Category("[5] San"), Description("Sanֵ����"), DisplayName("[1] SanityMax")]
    public double SanityMax { get; set; }
    [Category("[5] San"), Description("Sanֵ�仯��"), DisplayName("[2] BasicSanityDecrase")]
    public double BasicSanityDecrase { get; set; }

    [Category("[5] San"), Description("Sanֵ�仯����"), DisplayName("[3] SanityMultiplier")]
    public double SanityMultiplier { get; set; }

    [Category("[3] Health"), Description("���")]
    public bool IsAlive { get; set; } = true;
    [Category("[6] Type"), Description("��Ʒ���Ա���")]
    public List<WorkerFoodFavor> FoodPopertyMultiplier { get; set; } = new List<WorkerFoodFavor>();

    [Category("[8] LineBank"), Description("������ֵԱ���Ի�")]
    private List<string> NormalLineBank { get; set; } = new();
    [Category("[8] LineBank"), Description("����Ա���Ի�")]
    private List<string> DyingLineBank { get; set; } = new();
    [Category("[8] LineBank"), Description("����Ա���Ի�")]
    private List<string> GoodLineBank { get; set; } = new();

    [Category("[7] Contest"), Description("Ա���Ƿ���뱩��")]
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