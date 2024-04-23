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
    [Category("Value"), Description("食材健康值")]
    public double IngredientHealth { get; set; }
    [Category("Value"), Description("食材San值")]
    public double IngredientSanity { get; set; }
    [Category("Value"), Description("食材饱食度")]
    public double IngredientSatiety { get; set; }
    [Category("Type"), Description("食材特殊属性")]
    public FoodProperty IngredientProperty { get; set; }

    public Ingredient() { }
    public Ingredient(int iD, IngredientType type, string name, string description, double ingredientHealth, double ingredientSanity, double ingredientSatiety, FoodProperty ingredientProperty)
    {
      ID = iD;
      Type = type;
      Name = name;
      Description = description;
      IngredientHealth = ingredientHealth;
      IngredientSanity = ingredientSanity;
      IngredientSatiety = ingredientSatiety;
      IngredientProperty = ingredientProperty;
    }
  }
}