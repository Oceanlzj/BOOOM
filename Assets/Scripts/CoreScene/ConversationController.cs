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

  public Worker Worker;
  public Request Request;

  private int CurrentIndex = 0;
  public bool ConversationEnd = false;


  public void UpdateALine()
  {
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
    if(CurrentIndex >= Request.ConversationSentences.Count)
    {
      ConversationEnd = true;
    }
  }

  // Start is called before the first frame update
  void Start()
  {
    Worker = DataFactory.Instance().GetWorkerByID(0);
    Request = DataFactory.Instance().getTaskByID(TaskID);

    WorkerSprite.sprite = SpriteLibrary.GetSprite(WorkerID.ToString(), "0");
    UpdateALine();
  }


  private void OnMouseDown()
  {
    UpdateALine() ;
  }

  // Update is called once per frame
  void Update()
  {

  }
}
