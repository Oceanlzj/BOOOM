using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
namespace Assets.BasicModule.Model
{
  [Serializable]
  public class Dish
  {
    public enum DishType
    {
      None = 0,
      Normal = 1,
      Special = 2
    }
    [Category("ID"), Description("ΨһID")]
    public int ID { get; set; }
    [Category("Description"), Description("��Ʒ��")]
    public string Name { get; set; }
    [Category("Description"), Description("��Ʒ����")]
    public string Description { get; set; }

    [Category("Value"), Description("��Ʒ����ֵ")]
    public double DishHealth { get; set; }
    [Category("Value"), Description("��ƷSanֵ")]
    public double DishSanity { get; set; }
    [Category("Value"), Description("��Ʒ��ʳ��")]
    public double DishSatiety { get; set; }
    //����ɹ���ϵ��
    [Category("Type"), Description("��Ʒ����")]
    public DishType Type { get; set; }
    [Category("Type"), Description("��Ʒ��������")]
    public List<FoodProperty> Properties { get; set; } = new List<FoodProperty>();


  }
}