using Assets.BasicModule.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.BasicModule.Model
{
  [Serializable]
  public class Recipe
  {
    private static Recipe SingleRecipe;
    public Dictionary<string, int> Recipes { get; set; } = new Dictionary<string, int>();

    public static Recipe Instance()
    {
      if (SingleRecipe == null)
      {
        SingleRecipe = new Recipe();
      }
      return SingleRecipe;
    }
  }
}
