using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DialogManagement : MonoBehaviour
{
    private TabSelect nowStatus;
    public Button YesButton;
    public Button NoButton;
    private int SettingButtonDownNum;
    private int ExitButtonDownNum;
    public Text dialogContent;//电视机上的对话框里的文字部分
    public enum TabSelect
    {
        StartGame,
        ContinueGame,
        Setting,
        Exit,
    }
    
    void Start()
    {
        SettingButtonDownNum = 0;
        ExitButtonDownNum = 0;
        YesButton.onClick.AddListener(OnYesButtonClick);
        NoButton.onClick.AddListener(OnNoButtonClick);
    }

    void OnYesButtonClick()
    {
        if(nowStatus==TabSelect.StartGame)
        {
            
                SceneManager.LoadScene(2);
            
            
                SceneManager.LoadScene(2);
            

        }
        else if (nowStatus == TabSelect.ContinueGame)
        {
            
                //
                //
                //存档读取逻辑+跳转到相应的页面
                SceneManager.LoadScene(2);
            
        }
        else if (nowStatus == TabSelect.Setting)
        {
            SettingButtonDownNum++;
            if (SettingButtonDownNum >= 3)
            {
                dialogContent.text = "提示：为了减少维修成本，非必要情况请不要重复使用这些按钮";
                //typeWriter.ORIGINAL_TEXT = "提示：为了减少维修成本，非必要情况请不要重复使用这些按钮";
                //typeWriter.StartTypewriter();
            }
        }
        else if (nowStatus == TabSelect.Exit)
        {
            ExitButtonDownNum++;
            if (ExitButtonDownNum >= 3)
            {
                dialogContent.text = "提示：为了减少维修成本，非必要情况请不要重复使用这些按钮";
                //typeWriter.ORIGINAL_TEXT = "提示：为了减少维修成本，非必要情况请不要重复使用这些按钮";
                //typeWriter.StartTypewriter();
            }
        }
    }
    void OnNoButtonClick()
    {
        if (nowStatus == TabSelect.StartGame)
        {
            //if (ButtonDownNum == 0)
            //{
            //    dialogContent.text = "请按下“YES”键";
            //    ButtonDownNum += 1;
            //}
            //if(ButtonDownNum==1)
            //{
                dialogContent.text = "请按下“YES”键";
            //}
            
        }
        else if (nowStatus == TabSelect.ContinueGame)
        {
            dialogContent.text = "请按下“YES”键";
        }
        else if (nowStatus == TabSelect.Setting)
        {
            SettingButtonDownNum++;
            if (SettingButtonDownNum >= 3)
            {
                dialogContent.text = "提示：为了减少维修成本，非必要情况请不要重复使用这些按钮";
                //typeWriter.ORIGINAL_TEXT = "提示：为了减少维修成本，非必要情况请不要重复使用这些按钮";
                //typeWriter.StartTypewriter();
            }
        }
        else if (nowStatus == TabSelect.Exit)
        {
            ExitButtonDownNum++;
            if (ExitButtonDownNum >= 3)
            {
                dialogContent.text = "提示：为了减少维修成本，非必要情况请不要重复使用这些按钮";
                //typeWriter.ORIGINAL_TEXT = "提示：为了减少维修成本，非必要情况请不要重复使用这些按钮";
                //typeWriter.StartTypewriter();
            }
        }
    }
    public void SwitchTabToStartGame()
    {
        nowStatus = TabSelect.StartGame;
        dialogContent.text = "【背景故事】";
    }
    public void SwitchTabToContinueGame()
    {
        nowStatus = TabSelect.ContinueGame;
        dialogContent.text = "又要开始新的一天的工作了吗？";
    }
    public void SwitchTabToSetting()
    {
        nowStatus = TabSelect.Setting;
        dialogContent.text = "提示：设置键在WORK BOY的右上方哦";
    }
    public void SwitchTabToExit()
    {
        nowStatus = TabSelect.Exit;
        dialogContent.text = "快按下那个红色的关机按钮吧";
    }
}
