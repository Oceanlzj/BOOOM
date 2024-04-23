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
    public GameObject IDcard;

    //右侧信息区的指向箭头
    public GameObject Pointer1;
    public GameObject Pointer2;
    public GameObject Pointer3;
    public GameObject Pointer4;

    private bool PlayIDcardIn;
    private bool PlayIDcardOut;
    public float IDcardSpeed;
    void Start()
    {
        closeButton.onClick.AddListener(OnCloseButtonClick);
        ClickNum = 0;
        SettingButton.interactable = false;
        ExitButton.interactable = false;
        PlayIDcardIn = false;
        PlayIDcardOut = false;
    }
    private void Update()
    {
        if (PlayIDcardIn == true)
        {
            IDcard.transform.position += new Vector3(0, -1) * IDcardSpeed * Time.deltaTime;
        }
        else
        {
            StopCoroutine(IDcardIn());
        }
        if (PlayIDcardOut == true)
        {
            IDcard.transform.position += new Vector3(0, 1) * IDcardSpeed * Time.deltaTime;
        }
        else
        {
            StopCoroutine(IDcardOut());
        }
    }

    void OnCloseButtonClick()
    {
        ClickNum += 1;
        if (ClickNum % 2 == 0)
        {
            MainPageManager.SetActive(false);
            ClosedTV.SetActive(true);
            //InformationPanel.SetActive(false);
            StartCoroutine(IDcardOut());

            LeftScreen.SetActive(false);


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
            StartCoroutine(IDcardIn());


            TVSnowEffect.SetActive(true);
        }
    }
    private IEnumerator IDcardIn()
    {
        yield return new WaitForSeconds(5f);
        PlayIDcardIn = true;
        yield return new WaitForSeconds(1.5f);
        InformationPanel.SetActive(true);
        YesButtonImage.SetActive(false);
        NoButtonImage.SetActive(false);
        SettingButton.interactable = true;
        ExitButton.interactable = true;
        PlayIDcardIn = false;

    }
    private IEnumerator IDcardOut()
    {
        //yield return new WaitForSeconds(5f);
        PlayIDcardOut = true;
        InformationPanel.SetActive(false);
        YesButtonImage.SetActive(true);
        NoButtonImage.SetActive(true);
        SettingButton.interactable = false;
        ExitButton.interactable = false;
        yield return new WaitForSeconds(1.5f);

        PlayIDcardOut = false;

    }
}

