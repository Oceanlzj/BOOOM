using System;
using System.Collections;
using System.Collections.Generic;
namespace Assets.BasicModule.Model
{
  [Serializable]
  public class PlayerStats 
  {
    public List<Request> Tasks { get; set; } = new List<Request>();
    public int CurrentDay { get; set; } = 0;

    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    public Dictionary<Dish, int> DishesInventory { get; set; } = new Dictionary<Dish, int>();



    public double Sanity { get; set; } = 50;

  }
}
