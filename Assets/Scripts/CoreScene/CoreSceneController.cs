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
      foreach (DishPack pack in page.packs)
      {
        pack.Hide();
      }
      page.FlipAnimator.Play("ServePageOut");
      page.MsgBox.SetActive(false);
      page.MsgBoxAnimator.Play("MsgPopOut");
      ConversationPage.SetActive(true);

      CurrentPage = ConversationPage;
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
    foreach (DishPack pack in page.packOnPlate)
    {
      page.packs.Remove(pack);
    }

    RemoveAllChildren(page.PlateFolder);
    foreach (Dish dish in page.DishOnPlate)
    {
      page.worker.Eat(dish);
    }
    page.NextWorker();
  }

  public void ConversationToServe()
  {
    ServePageController Spage = ServePage.GetComponent<ServePageController>();
    ConversationController Cpage = ConversationPage.GetComponent<ConversationController>();

    Spage.FlipAnimator.Play("ServePageIn");

    foreach (DishPack pack in Spage.packs)
    {
      pack.gameObject.SetActive(true);
    }
    Spage.MsgBox.SetActive(true);
    ConversationPage.SetActive(false);

    CurrentPage = ServePage;
  }

  public void OnYesClicked()
  {
    ServePageController Spage = ServePage.GetComponent<ServePageController>();
    ConversationController Cpage = ConversationPage.GetComponent<ConversationController>();
    if (CurrentPage == ServePage)
    {
      foreach (DishPack pack in Spage.packOnPlate)
      {
        pack.isServing = true;
      }
      Spage.PlateHandAnimator.Play("HandPlateOut");
      Spage.MsgBoxAnimator.Play("MsgPopOut");
      return;
    }

    if (CurrentPage == ConversationPage)
    {

      if (Cpage.ConversationEnd)
      {
        //Add effect
        //return to serve
        ConversationToServe();


      }
      else
      {
        Cpage.UpdateALine();

      }
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
      return;
    }

    if (CurrentPage == ConversationPage)
    {
      ConversationController page = ConversationPage.GetComponent<ConversationController>();
      if (page.ConversationEnd)
      {
        //add effect
        //return to serve
      }
      else
      {
        page.UpdateALine();
      }
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
