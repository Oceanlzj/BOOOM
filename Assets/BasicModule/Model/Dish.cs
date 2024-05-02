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
    [Category("ID"), Description("唯一ID")]
    public int ID { get; set; }
    [Category("Description"), Description("菜品名")]
    public string Name { get; set; }
    [Category("Description"), Description("菜品描述")]
    public string Description { get; set; }

    [Category("Value"), Description("菜品健康值")]
    public double DishHealth { get; set; }
    [Category("Value"), Description("菜品San值")]
    public double DishSanity { get; set; }
    [Category("Value"), Description("菜品饱食度")]
    public double DishSatiety { get; set; }
    //料理成功度系数
    [Category("Type"), Description("菜品类型")]
    public DishType Type { get; set; }
    [Category("Type"), Description("菜品特殊属性")]
    public List<FoodProperty> Properties { get; set; } = new List<FoodProperty>();

    public string ToFirstString()
    {
      string a = "\u2b9e[" + Name + "]\n";
      return a;
    }
    public override string ToString()
    {
      string HPBar = "健康\u2624";
      string SatBar = "饱食\u2615";
      string SanBar = "理智\u26ef";

      for (int i = 0; i < DishHealth / 10; i++) { HPBar += '\u271a'; }
      HPBar += "\n";

      for (int i = 0; i < DishSatiety / 10; i++) { SatBar += '\u271a'; }
      SatBar += "\n";

      for (int i = 0; i < DishSanity / 10; i++) { SanBar += "\u271a"; }
      SanBar += "\n";
      string p = "属性：";
      foreach (FoodProperty fp in Properties)
      {
        p += FoodPropertyString.FoodPropertyName(fp) + ' ';
      }


      string a = "\u2b9e[" + Name + "]\n" + HPBar + SatBar + SanBar + p + '\n';
      return a;
    }
  }
}