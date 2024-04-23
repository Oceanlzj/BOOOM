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
    //public Text dialogContent;//电视机上的对话框里的文字部分
    public TW_Regular typeWriter;
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
                
                //dialogContent.text = "提示：为了减少维修成本，非必要情况请不要重复使用这些按钮";
                typeWriter.ORIGINAL_TEXT = "提示：为了减少维修成本，非必要情况请不要重复使用这些按钮";
                typeWriter.StartTypewriter();
            }
        }
        else if (nowStatus == TabSelect.Exit)
        {
            ExitButtonDownNum++;
            if (ExitButtonDownNum >= 3)
            {
                //ialogContent.text = "提示：为了减少维修成本，非必要情况请不要重复使用这些按钮";
                typeWriter.ORIGINAL_TEXT = "提示：为了减少维修成本，非必要情况请不要重复使用这些按钮";
                typeWriter.StartTypewriter();
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
            //dialogContent.text = "请按下“YES”键";
            typeWriter.ORIGINAL_TEXT = "请按下“YES”键";
            typeWriter.StartTypewriter();
            //}

        }
        else if (nowStatus == TabSelect.ContinueGame)
        {
            //dialogContent.text = "请按下“YES”键";
            typeWriter.ORIGINAL_TEXT = "请按下“YES”键";
            typeWriter.StartTypewriter();
        }
        else if (nowStatus == TabSelect.Setting)
        {
            SettingButtonDownNum++;
            if (SettingButtonDownNum >= 3)
            {
                //dialogContent.text = "提示：为了减少维修成本，非必要情况请不要重复使用这些按钮";
                typeWriter.ORIGINAL_TEXT = "提示：为了减少维修成本，非必要情况请不要重复使用这些按钮";
                typeWriter.StartTypewriter();
            }
        }
        else if (nowStatus == TabSelect.Exit)
        {
            ExitButtonDownNum++;
            if (ExitButtonDownNum >= 3)
            {
                //dialogContent.text = "提示：为了减少维修成本，非必要情况请不要重复使用这些按钮";
                typeWriter.ORIGINAL_TEXT = "提示：为了减少维修成本，非必要情况请不要重复使用这些按钮";
                typeWriter.StartTypewriter();
            }
        }
    }
    public void SwitchTabToStartGame()
    {
        nowStatus = TabSelect.StartGame;
        //dialogContent.text = "【背景故事】";
        typeWriter.ORIGINAL_TEXT = "【背景故事】\n按下'YES'键开始吧";
        typeWriter.StartTypewriter();
    }
    public void SwitchTabToContinueGame()
    {
        nowStatus = TabSelect.ContinueGame;
        //dialogContent.text = "又要开始新的一天的工作了吗？";
        typeWriter.ORIGINAL_TEXT = "又要开始新的一天的工作了吗？\n好吧\n按下'YES'键开始吧";
        typeWriter.StartTypewriter();
    }
    public void SwitchTabToSetting()
    {
        nowStatus = TabSelect.Setting;
        //dialogContent.text = "提示：设置键在WORK BOY的右侧哦";
        typeWriter.ORIGINAL_TEXT = "提示：设置键在WORK BOY的右侧哦";
        typeWriter.StartTypewriter();
    }
    public void SwitchTabToExit()
    {
        nowStatus = TabSelect.Exit;
        //dialogContent.text = "快按下那个红色的关机按钮吧";
        typeWriter.ORIGINAL_TEXT = "快按下那个红色的关机按钮吧\n提示：关机在屏幕正下方哦";
        typeWriter.StartTypewriter();
    }
}
