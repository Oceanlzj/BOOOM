using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineCollisionController : MonoBehaviour
{
  // Start is called before the first frame update
  public ProcessSceneManager ProcessSceneManager;


  private void OnTriggerEnter2D(Collider2D collision)
  {
    ProcessSceneManager.StopText = false;
    if (collision.tag == "Dish")
    {
      IngredientItem ig = collision.gameObject.GetComponent<IngredientItem>();


      ig.transform.parent = ProcessSceneManager.MachineFolder.transform;
      ProcessSceneManager.IngsOnMachine.Add(ig);
      ProcessSceneManager.IngredientsOnMachine.Add(ig.Ingredient);
    }
    ProcessSceneManager.UpdateText();
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    if (collision.tag == "Dish")
    {
      IngredientItem ig = collision.gameObject.GetComponent<IngredientItem>();

      ig.transform.parent = ProcessSceneManager.PrepFolder.transform;
      ProcessSceneManager.IngsOnMachine.Remove(ig);
      ProcessSceneManager.IngredientsOnMachine.Remove(ig.Ingredient);
    }
    if (!ProcessSceneManager.StopText)
    { ProcessSceneManager.UpdateText(); }
  }



  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}
