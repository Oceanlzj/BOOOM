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
    public Text dialogContent;//���ӻ��ϵĶԻ���������ֲ���
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
                Debug.Log("������Ϸ");
                //������Ϸ�����߼�
                //
                //
            }
            if (ButtonDownNum == 1)
            {
                Debug.Log("������Ϸ");
                //������Ϸ�����߼�
                //
                //
            }

        }
        else if (nowStatus == TabSelect.ContinueGame)
        {
            if (ButtonDownNum == 0)
            {
                Debug.Log("������Ϸ");
                //������Ϸ�����߼�
                //�浵��ȡ
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
            //    dialogContent.text = "�밴�¡�YES����";
            //    ButtonDownNum += 1;
            //}
            //if(ButtonDownNum==1)
            //{
                dialogContent.text = "�밴�¡�YES����";
            //}
            
        }
        else if (nowStatus == TabSelect.ContinueGame)
        {
            dialogContent.text = "�밴�¡�YES����";
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
        dialogContent.text = "���������¡�";
    }
    public void SwitchTabToContinueGame()
    {
        nowStatus = TabSelect.ContinueGame;
        dialogContent.text = "��Ҫ��ʼ�µ�һ��Ĺ�������";
    }
    public void SwitchTabToSetting()
    {
        nowStatus = TabSelect.Setting;
        dialogContent.text = "��ʾ�����ü���WORK BOY�����Ϸ�Ŷ";
    }
    public void SwitchTabToExit()
    {
        nowStatus = TabSelect.Exit;
        dialogContent.text = "�찴���Ǹ���ɫ�Ĺػ���ť��";
    }
}
