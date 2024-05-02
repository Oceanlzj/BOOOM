using Assets.BasicModule.Factory;
using Assets.BasicModule.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class ConversationController : MonoBehaviour
{
  public GameObject ServeScene;

  public SpriteRenderer WorkerSprite;
  public SpriteLibraryAsset SpriteLibrary;

  public int WorkerID = 100;
  public int TaskID = 0;
  public TextMeshProUGUI TalkTextArea;

  public SpriteRenderer CGSprite;
  public SpriteLibraryAsset CGSpriteLib;

  public SpecialWorker Worker;
  public Request Request;

  public bool NeedToShowCGYes = false;
  public bool NeedToShowCGNo = false;

  private int CurrentIndex = 0;
  public bool ConversationEnd = false;


  public void UpdateALine()
  {
    if (CurrentIndex + 1 > Request.ConversationSentences.Count)
    {
      ConversationEnd = true;
      return;
    }

    TalkTextArea.text = Request.ConversationSentences[CurrentIndex];

    CurrentIndex++;

  }

  public void NewIn()
  {
    ServePageController sp = ServeScene.GetComponent<ServePageController>();
    Worker = sp.CurrentSpecialWorker;
    Request = sp.CurrentSpecialWorker.CurrentTask;
    WorkerSprite.sprite = SpriteLibrary.GetSprite(Worker.ID.ToString(), "0");
    CurrentIndex = 0;
    NeedToShowCGNo = Worker.CurrentTask.CGID_NO != -1;
    NeedToShowCGYes = Worker.CurrentTask.CGID_Yes != -1;

    UpdateALine();
  }

  // Start is called before the first frame update
  void Start()
  {
    NewIn();



  }


  private void OnMouseDown()
  {
    UpdateALine();
  }

  // Update is called once per frame
  void Update()
  {

  }
}
