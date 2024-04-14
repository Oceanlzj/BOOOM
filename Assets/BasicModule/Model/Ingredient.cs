using System.Collections;
using System.Collections.Generic;

public class Ingredient
{
  public enum IngredienType
  {
    None = 0,
    Normal = 1,
    Special = 2
  }

  public int ID {  get; set; }
  public IngredienType Type { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }

}