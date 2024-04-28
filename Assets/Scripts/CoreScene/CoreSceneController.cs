using System.Collections;
using System.Collections.Generic;
using Assets.BasicModule.Factory;
using Assets.BasicModule.Model;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

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
  public Collider2D PlateCollider;


  private GameObject CurrentPage;


  public void OnMsgBoxClicked()
  {
    ServePageController page = ServePage.GetComponent<ServePageController>();
    if (page.worker is SpecialWorker && page.CurrentSpecialWorker.CurrentTask != null)
    {
      PlateCollider.enabled = false;

      page.PlateFolder.gameObject.SetActive(false);
      page.PackFolder.gameObject.SetActive(false);

      ConversationPage.GetComponent<ConversationController>().NewIn();
      page.FlipAnimator.Play("ServePageOut");
      //page.MsgBox.SetActive(false);
      page.MsgBoxAnimator.Play("MsgPopOut");
      ConversationPage.SetActive(true);


      CurrentPage = ConversationPage;
    }
    else
    {
      page.UpdateMsgBoxLine();
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

  public void ConversationToServe()
  {
    ServePageController Spage = ServePage.GetComponent<ServePageController>();
    ConversationController Cpage = ConversationPage.GetComponent<ConversationController>();

    Spage.MsgBox.SetActive(false);
    Spage.FlipAnimator.Play("ServePageIn");

    Spage.PlateFolder.gameObject.SetActive(true);
    Spage.PackFolder.gameObject.SetActive(true);

    Spage.MsgBox.SetActive(false);
    PlateCollider.enabled = true;
    ConversationPage.SetActive(false);


    CurrentPage = ServePage;
  }

  public void OnYesClicked()
  {
    ServePageController Spage = ServePage.GetComponent<ServePageController>();
    ConversationController Cpage = ConversationPage.GetComponent<ConversationController>();

    if (CurrentPage == ServePage)
    {
      if (Spage.AllDone)
      {
        SceneManager.LoadScene(2);
      }
      else
      {
        if (Spage.TalkDone)
        {
          foreach (DishPack pack in Spage.packOnPlate)
          {
            pack.isServing = true;
          }


          Spage.MsgBox.SetActive(false);
          Spage.PlateHandAnimator.Play("HandPlateOut");
          Spage.MsgBoxAnimator.Play("MsgPopOut");

          Spage.CurrentIndex++;
          return;
        }
        else
        {
          Spage.UpdateMsgBoxLine();
        }
      }
    }

    if (CurrentPage == ConversationPage)
    {

      if (Cpage.ConversationEnd)
      {
        //Add effect
        //return to serve
        Spage.CurrentSpecialWorker.NextRequest(true);
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
    ServePageController Spage = ServePage.GetComponent<ServePageController>();
    ConversationController Cpage = ConversationPage.GetComponent<ConversationController>();
    if (CurrentPage == ServePage)
    {
      foreach (DishPack pack in Spage.packOnPlate)
      {
        pack.UnServerFromPlate();
      }

      return;
    }

    if (CurrentPage == ConversationPage)
    {
      if (Cpage.ConversationEnd)
      {
        //add effect
        Spage.CurrentSpecialWorker.NextRequest(false);
        //return to serve
        ConversationToServe();
      }
      else
      {
        Cpage.UpdateALine();
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
