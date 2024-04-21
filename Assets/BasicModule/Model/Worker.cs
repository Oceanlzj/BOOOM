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
    public class WorkerFoodFavor
    {
      public FoodProperty foodProperty { get; set; }
      public double Multiplier { get; set; }
    }

    [Category("ID"), Description("ΨһID")]
    public int ID { get; set; }
    [Category("Type"), Description("Ա����������")]
    public WorkerType Type { get; set; }
    [Category("Description"), Description("Ա����")]
    public string Name { get; set; }
    [Category("Description"), Description("Ա������")]
    public string Description { get; set; }

    //������
    [Category("Health"), Description("��ʼ����ֵ")]
    public double Health { get; set; }
    [Category("Health"), Description("����ֵ����")]
    public double HealthMax { get; set; }
    [Category("Health"), Description("����ֵ�仯��")]
    public double BasicHealthDecrease { get; set; }
    [Category("Health"), Description("����ֵ�仯����")]
    public double HealthMultiplier { get; set; }

    //��ʳ��
    [Category("Satiety"), Description("��ʼ��ʳ��")]
    public double Satiety { get; set; }
    [Category("Satiety"), Description("��ʳ������")]
    public double SatietyMax { get; set; }
    [Category("Satiety"), Description("��ʳ�ȱ仯��")]
    public double BasicSatietyDecrease { get; set; }
    [Category("Satiety"), Description("��ʳ�ȱ仯����")]
    public double SanietyMultiplier { get; set; }

    //San
    [Category("San"), Description("Sanֵ")]
    public double Sanity { get; set; }
    [Category("San"), Description("Sanֵ����")]
    public double SanityMax {  get; set; }
    [Category("San"), Description("Sanֵ�仯��")]
    public double BasicSanityDecrase { get; set; }
    [Category("San"), Description("Sanֵ�仯����")]
    public double SanityMultiplier { get; set; }

    [Category("Health"), Description("���")]
    public bool IsAlive { get; set; } = true;
    [Category("Type"), Description("��Ʒ���Ա���")]
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