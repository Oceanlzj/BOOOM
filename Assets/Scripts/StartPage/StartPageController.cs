using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPageController : MonoBehaviour
{
  public FadeOut FadeOut;
  public GameObject ButtonA;
  public GameObject ButtonB;
  public GameObject ButtonC;

  private List<TextMeshProUGUI> ButtonTextList;

  public TextMeshProUGUI TextArea;

  public Color TextColorNormal;
  public Color TextColorSelected;

  public TextMeshProUGUI ButtonAText;
  public TextMeshProUGUI ButtonBText;
  public TextMeshProUGUI ButtonCText;

  public Animator IntroAnimator;




  private int currentSelection = 0;

  private bool Starting = true;

  // Start is called before the first frame update
  void Start()
  {
    ButtonTextList = new List<TextMeshProUGUI>() { null, ButtonAText, ButtonBText, ButtonCText };
    
    //FadeOut.StartScene();
  }

  public void ChangeSelection(int selection)
  {
    if (currentSelection != 0)
    {
      ButtonTextList[currentSelection].text = ButtonTextList[currentSelection].text.Remove(0, 1);
      ButtonTextList[currentSelection].color = TextColorNormal;
    }
    currentSelection = selection;
    if (selection != 0)
    {
      ButtonTextList[currentSelection].text = '\u2b9e' + ButtonTextList[currentSelection].text;
      ButtonTextList[currentSelection].color = TextColorSelected;
    }

    UpdateText();
  }
  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyUp(KeyCode.UpArrow))
    {
      ChangeSelection(currentSelection < 2 ? 1 : currentSelection - 1);
    }

    if (Input.GetKeyUp(KeyCode.DownArrow))
    {
      ChangeSelection(currentSelection > 2 ? 3 : currentSelection + 1);
    }



    if (Starting && IntroAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
    {
      Starting = false;
      ButtonA.SetActive(true);
      ButtonB.SetActive(true);
      ButtonC.SetActive(true);
      UpdateText();
    }
  }

  public void UpdateText()
  {
    switch (currentSelection)
    {
      case 0:
        TextArea.text = "使用方向键进行选择";
        return;
      case 1:
        TextArea.text = "开始游戏！";
        break;
      case 2:
        TextArea.text = "施工中…………";
        break;
      case 3:
        TextArea.text = "退出游戏";
        break;
    }
    TextArea.text += "\n按下[YES]按键来继续";
  }




  public void OnYesButtonClick()
  {
    switch (currentSelection)
    {
      case 1:
        SceneManager.LoadScene(3);
        break;
      case 2: break;
      case 3:
        Application.Quit();
        break;
    }
  }
  public void OnNoButtonClick()
  {
    ChangeSelection(0);
  }

}
