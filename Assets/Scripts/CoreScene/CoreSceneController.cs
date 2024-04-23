using System.Collections;
using System.Collections.Generic;
using Assets.BasicModule.Factory;
using Assets.BasicModule.Model;
using Unity.VisualScripting;
using UnityEngine;

public class CoreSceneController : MonoBehaviour
{

  private enum Operation
  {
    None,
    Serving,
    Talking,
  }
  public GameObject ServePage;
  public GameObject ConversationPage;

  private GameObject CurrentPage;


  public void OnMsgBoxClicked()
  {
    ServePageController page = ServePage.GetComponent<ServePageController>();
    // if ()//is special worker; 
    {
      foreach(DishPack pack in page.packs)
      {
        pack.Hide();
      }
      page.FlipAnimator.Play("ServePageOut");
    }
  }

  public static void RemoveAllChildren(GameObject parent)
  {
    Transform transform;
    int count = parent.transform.childCount;
    for (int i = 0; i < count; i++)
    {
      transform = parent.transform.GetChild(0);
      GameObject.Destroy(transform.gameObject);
    }
  }

  public void HandInEnd()
  {

  }


  public void HandOutEnd()
  {
    ServePageController page = ServePage.GetComponent<ServePageController>();
    RemoveAllChildren(page.PlateFolder);
    foreach (Dish dish in page.DishOnPlate)
    {
      page.worker.Eat(dish);
    }
    page.NextWorker();
  }


  public void OnYesClicked()
  {
    if (CurrentPage == ServePage)
    {

      ServePageController page = ServePage.GetComponent<ServePageController>();
      foreach (DishPack pack in page.packOnPlate)
      {
        pack.isServing = true;
      }
      page.PlateHandAnimator.Play("HandPlateOut");
      page.MsgBoxAnimator.Play("MsgPopOut");
      return;
    }
  }

  public void OnNoClicked()
  {
    if (CurrentPage == ServePage)
    {
      ServePageController page = ServePage.GetComponent<ServePageController>();

      foreach (DishPack pack in page.packOnPlate)
      {
        pack.UnServerFromPlate();
      }
    }

    if (CurrentPage == ConversationPage)
    {

    }

  }

  // Start is called before the first frame update
  void Start()
  {
    CurrentPage = ServePage;
  }

  // Update is called once per frame
  void Update()
  {
    if (ServePage.activeSelf)
    {
      if (ServePage.GetComponent<ServePageController>().PlateHandAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
      {
        if (ServePage.GetComponent<ServePageController>().PlateHandAnimator.GetCurrentAnimatorStateInfo(0).IsName("HandPlateOut"))
        {
          HandOutEnd();
        }
      }
    }
  }
}
