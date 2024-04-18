using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public double DishHealth { get; set; }
    public double DishSanity { get; set; }
    public double DishSatiety { get; set; }
    //料理成功度系数
    public double Successfulness { get; set; }
    public DishType Type { get; set; }
    public List<FoodProperty> Properties { get; set; }

    public Dish() { }
    public Dish(int iD, string name, string description, double dishHealth, double dishSanity, double dishSatiety, double successfulness, DishType type, List<FoodProperty> properties)
    {
      ID = iD;
      Name = name;
      Description = description;
      DishHealth = dishHealth;
      DishSanity = dishSanity;
      DishSatiety = dishSatiety;
      Successfulness = successfulness;
      Type = type;
      Properties = properties;
    }
    
    public Dish (Ingredient ingredient1, Ingredient ingredient2)
    {

    }

  }
}