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
  public TextMeshProUGUI NarratorArea;

  public SpecialWorker Worker;
  public Request Request;

  private int CurrentIndex = 0;
  public bool ConversationEnd = false;


  public void UpdateALine()
  {
    if (CurrentIndex + 1 >= Request.ConversationSentences.Count)
    {
      ConversationEnd = true;
      return;
    }

    if (Request.ConversationSentences[CurrentIndex].IsTalk)
    {
      TalkTextArea.text = Request.ConversationSentences[CurrentIndex].Line;
      NarratorArea.text = "";
    }
    else
    {
      NarratorArea.text = Request.ConversationSentences[CurrentIndex].Line;
      TalkTextArea.text = "";
    }
    CurrentIndex++;

  }

  public void NewIn()
  {
    ServePageController sp = ServeScene.GetComponent<ServePageController>();
    Worker = sp.CurrentSpecialWorker;
    Request = sp.CurrentSpecialWorker.CurrentTaskID;
    WorkerSprite.sprite = SpriteLibrary.GetSprite(Worker.ID.ToString(), "0");
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
