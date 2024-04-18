using System;
using System.Collections;
using System.Collections.Generic;
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

    public int ID { get; set; }
    public IngredientType Type { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double IngredientHealth { get; set; }
    public double IngredientSanity { get; set; }
    public double IngredientSatiety { get; set; }
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