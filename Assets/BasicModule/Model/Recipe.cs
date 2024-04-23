using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.BasicModule.Model
{
  [Serializable]
  public class Recipe
  {
    public int Ingredient_ID1 {get; set;}
    public int Ingredient_ID2 { get; set;}
    public int Dish_ID { get; set;}
    public double Difficulty { get; set;}
  }
}
