using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogManagement : MonoBehaviour
{
    private TabSelect nowStatus;
    public Button YesButton;
    public Button NoButton;
    private int ButtonDownNum;
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
        ButtonDownNum = 0;
        YesButton.onClick.AddListener(OnYesButtonClick);
        NoButton.onClick.AddListener(OnNoButtonClick);
    }

    void OnYesButtonClick()
    {
        if(nowStatus==TabSelect.StartGame)
        {
            if(ButtonDownNum==0)
            {
                Debug.Log("进入游戏");
                //进入游戏界面逻辑
                //
                //
            }
            if (ButtonDownNum == 1)
            {
                Debug.Log("进入游戏");
                //进入游戏界面逻辑
                //
                //
            }

        }
        else if (nowStatus == TabSelect.ContinueGame)
        {
            if (ButtonDownNum == 0)
            {
                Debug.Log("进入游戏");
                //进入游戏界面逻辑
                //存档读取
                //
            }
        }
        else if (nowStatus == TabSelect.Setting)
        {

        }
        else if (nowStatus == TabSelect.Exit)
        {

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

        }
        else if (nowStatus == TabSelect.Exit)
        {

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
