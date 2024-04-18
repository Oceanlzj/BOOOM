using System;
using System.Collections;
using System.Collections.Generic;
namespace Assets.BasicModule.Model
{
  [Serializable]
  public class PlayerStats
  {
    public string Name { get; set; }
    public string Description { get; set; }

    public List<Task> Tasks { get; set; }
    public int CurrentDay { get; set; }

    public List<Ingredient> Ingredients { get; set; }
    public Dictionary<Dish, int> DishesInventory { get; set; }

    

    public double Sanity { get; set; }

  }
}
