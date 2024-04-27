using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
namespace Assets.BasicModule.Model
{
  [Serializable]
  public class Ingredient
  {
    public enum IngredientType
    {
      None = 0,
      Normal = 1,
      Special = 2
    }

    [Category("ID"), Description("唯一ID")]
    public int ID { get; set; }
    [Category("Type"), Description("食材类型")]
    public IngredientType Type { get; set; }
    [Category("Description"), Description("食材名")]
    public string Name { get; set; }
    [Category("Description"), Description("食材描述")]
    public string Description { get; set; }
    [Category("Type"), Description("食材特殊属性")]
    public FoodProperty IngredientProperty { get; set; }

    public Ingredient() { }
  }
}