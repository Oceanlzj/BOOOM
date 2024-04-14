using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ingredient
{
  public enum IngredientType
  {
    None = 0,
    Standard = 1,
    Special = 2
  }

  public int ID {  get; set; }
  public IngredientType Type {get; set;}
  public string Name { get; set;}
  public string Description { get; set;}

}
