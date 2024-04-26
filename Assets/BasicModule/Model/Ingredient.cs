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

    [Category("ID"), Description("ΨһID")]
    public int ID { get; set; }
    [Category("Type"), Description("ʳ������")]
    public IngredientType Type { get; set; }
    [Category("Description"), Description("ʳ����")]
    public string Name { get; set; }
    [Category("Description"), Description("ʳ������")]
    public double IngredientSatiety { get; set; }
    [Category("Type"), Description("ʳ����������")]
    public FoodProperty IngredientProperty { get; set; }

    public Ingredient() { }
    public Ingredient(int iD, IngredientType type, string name, string description, double ingredientHealth, double ingredientSanity, double ingredientSatiety, FoodProperty ingredientProperty)
    {
      ID = iD;
      Type = type;
      Name = name;
      Name = name;
      IngredientSatiety = ingredientSatiety;
      IngredientProperty = ingredientProperty;
    }
  }
}