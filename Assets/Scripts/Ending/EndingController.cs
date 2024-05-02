using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class EndingController : MonoBehaviour
{
  public TW_Regular EndingTextTW;
  public TextMeshProUGUI EndingNameTextArea;

  public string EndingName;
  public List<string> EndingTexts;
  public int CurrentIndex = 0;

  public Image CG;
  public SpriteLibraryAsset CGLib;

  private bool EndingTextDone = false;

  // Start is called before the first frame update
  void Start()
  {
    EndingNameTextArea.text = EndingName;
    CurrentIndex = 0;
    CG.sprite = CGLib.GetSprite("Ending", "0");
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyUp(KeyCode.Escape) || Input.GetMouseButtonUp(0))
    {
      UpdateALine();
      if(EndingTextDone)
      {
        //updateCG
        CG.enabled = true;
      }
    }
  }

  public void UpdateALine()
  {
    if (CurrentIndex + 1 <= EndingTexts.Count)
    {
      EndingTextTW.ORIGINAL_TEXT = EndingTexts[CurrentIndex];
      EndingTextTW.StartTypewriter();
      CurrentIndex++;
    }
    else
    {
      EndingTextDone = true;
    }
  }
}
