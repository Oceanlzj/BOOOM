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
    //public Text dialogContent;//���ӻ��ϵĶԻ���������ֲ���
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
                //�浵��ȡ�߼�+��ת����Ӧ��ҳ��
                SceneManager.LoadScene(2);
        }
        else if (nowStatus == TabSelect.Setting)
        {
            SettingButtonDownNum++;
            if (SettingButtonDownNum >= 3)
            {
                
                //dialogContent.text = "��ʾ��Ϊ�˼���ά�޳ɱ����Ǳ�Ҫ����벻Ҫ�ظ�ʹ����Щ��ť";
                typeWriter.ORIGINAL_TEXT = "��ʾ��Ϊ�˼���ά�޳ɱ����Ǳ�Ҫ����벻Ҫ�ظ�ʹ����Щ��ť";
                typeWriter.StartTypewriter();
            }
        }
        else if (nowStatus == TabSelect.Exit)
        {
            ExitButtonDownNum++;
            if (ExitButtonDownNum >= 3)
            {
                //ialogContent.text = "��ʾ��Ϊ�˼���ά�޳ɱ����Ǳ�Ҫ����벻Ҫ�ظ�ʹ����Щ��ť";
                typeWriter.ORIGINAL_TEXT = "��ʾ��Ϊ�˼���ά�޳ɱ����Ǳ�Ҫ����벻Ҫ�ظ�ʹ����Щ��ť";
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
            //    dialogContent.text = "�밴�¡�YES����";
            //    ButtonDownNum += 1;
            //}
            //if(ButtonDownNum==1)
            //{
            //dialogContent.text = "�밴�¡�YES����";
            typeWriter.ORIGINAL_TEXT = "�밴�¡�YES����";
            typeWriter.StartTypewriter();
            //}

        }
        else if (nowStatus == TabSelect.ContinueGame)
        {
            //dialogContent.text = "�밴�¡�YES����";
            typeWriter.ORIGINAL_TEXT = "�밴�¡�YES����";
            typeWriter.StartTypewriter();
        }
        else if (nowStatus == TabSelect.Setting)
        {
            SettingButtonDownNum++;
            if (SettingButtonDownNum >= 3)
            {
                //dialogContent.text = "��ʾ��Ϊ�˼���ά�޳ɱ����Ǳ�Ҫ����벻Ҫ�ظ�ʹ����Щ��ť";
                typeWriter.ORIGINAL_TEXT = "��ʾ��Ϊ�˼���ά�޳ɱ����Ǳ�Ҫ����벻Ҫ�ظ�ʹ����Щ��ť";
                typeWriter.StartTypewriter();
            }
        }
        else if (nowStatus == TabSelect.Exit)
        {
            ExitButtonDownNum++;
            if (ExitButtonDownNum >= 3)
            {
                //dialogContent.text = "��ʾ��Ϊ�˼���ά�޳ɱ����Ǳ�Ҫ����벻Ҫ�ظ�ʹ����Щ��ť";
                typeWriter.ORIGINAL_TEXT = "��ʾ��Ϊ�˼���ά�޳ɱ����Ǳ�Ҫ����벻Ҫ�ظ�ʹ����Щ��ť";
                typeWriter.StartTypewriter();
            }
        }
    }
    public void SwitchTabToStartGame()
    {
        nowStatus = TabSelect.StartGame;
        //dialogContent.text = "���������¡�";
        typeWriter.ORIGINAL_TEXT = "���������¡�\n����'YES'����ʼ��";
        typeWriter.StartTypewriter();
    }
    public void SwitchTabToContinueGame()
    {
        nowStatus = TabSelect.ContinueGame;
        //dialogContent.text = "��Ҫ��ʼ�µ�һ��Ĺ�������";
        typeWriter.ORIGINAL_TEXT = "��Ҫ��ʼ�µ�һ��Ĺ�������\n�ð�\n����'YES'����ʼ��";
        typeWriter.StartTypewriter();
    }
    public void SwitchTabToSetting()
    {
        nowStatus = TabSelect.Setting;
        //dialogContent.text = "��ʾ�����ü���WORK BOY���Ҳ�Ŷ";
        typeWriter.ORIGINAL_TEXT = "��ʾ�����ü���WORK BOY���Ҳ�Ŷ";
        typeWriter.StartTypewriter();
    }
    public void SwitchTabToExit()
    {
        nowStatus = TabSelect.Exit;
        //dialogContent.text = "�찴���Ǹ���ɫ�Ĺػ���ť��";
        typeWriter.ORIGINAL_TEXT = "�찴���Ǹ���ɫ�Ĺػ���ť��\n��ʾ���ػ�����Ļ���·�Ŷ";
        typeWriter.StartTypewriter();
    }
}
