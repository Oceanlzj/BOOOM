using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButtonController : MonoBehaviour
{
    public Button closeButton;
    private int ClickNum;
    public GameObject MainPageManager;
    public GameObject ClosedTV;
    public GameObject InformationPanel;
    public GameObject YesButtonImage;
    public GameObject NoButtonImage;
    public GameObject LeftScreen;
    public Button SettingButton;
    public Button ExitButton;
    public GameObject TVSnowEffect;

    //右侧信息区的指向箭头
    public GameObject Pointer1;
    public GameObject Pointer2;
    public GameObject Pointer3;
    public GameObject Pointer4;


    void Start()
    {
        closeButton.onClick.AddListener(OnCloseButtonClick);
        ClickNum = 0;
        SettingButton.interactable = false;
        ExitButton.interactable = false;
    }

    void OnCloseButtonClick()
    {
        ClickNum += 1;
        if(ClickNum%2==0)
        {
            MainPageManager.SetActive(false);
            ClosedTV.SetActive(true);
            InformationPanel.SetActive(false);
            YesButtonImage.SetActive(true);
            NoButtonImage.SetActive(true);
            LeftScreen.SetActive(false);
            SettingButton.interactable = false;
            ExitButton.interactable = false;

            //指向箭头归零
            Pointer1.SetActive(false);
            Pointer2.SetActive(false);
            Pointer3.SetActive(false);
            Pointer4.SetActive(false);
        }
        else
        {
            MainPageManager.SetActive(true);
            ClosedTV.SetActive(false);
            InformationPanel.SetActive(true);
            YesButtonImage.SetActive(false);
            NoButtonImage.SetActive(false);
            SettingButton.interactable = true;
            ExitButton.interactable = true;
            TVSnowEffect.SetActive(true);
        }
    }
}
