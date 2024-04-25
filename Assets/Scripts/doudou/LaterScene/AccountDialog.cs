using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class AccountDialog : MonoBehaviour
{
    private TabSelect nowStatus;
    public Button YesButton;
    public Button NoButton;
    public Button CloseButton;
    //public Text dialogContent;//���ӻ��ϵĶԻ���������ֲ���
    public TW_Regular typeWriter;

    private Coroutine TodayInformationCoroutine;
    private Coroutine TotalSituationCoroutine;
    private Coroutine NewDayCoroutine;

    public int WorkerNum=0;
    public int SickWorkerNum=0;
    public int SadWorkerNum=0;
    public enum TabSelect
    {
        TodayInformation,
        TotalSituation,
        NewDay,
        Close,
    }

    void Start()
    {
        YesButton.onClick.AddListener(OnYesButtonClick);
        NoButton.onClick.AddListener(OnNoButtonClick);
        CloseButton.onClick.AddListener(OnCloseButtonClick);
    }

    void OnYesButtonClick()
    {
        if (nowStatus == TabSelect.TodayInformation)
        {
            
        }
        else if (nowStatus == TabSelect.TotalSituation)
        {
            
        }
        else if (nowStatus == TabSelect.NewDay)
        {

            SceneManager.LoadScene(3);
        }
        else if(nowStatus==TabSelect.Close)
        {
            SceneManager.LoadScene(0);
        }
        
    }
    void OnNoButtonClick()
    {
            //typeWriter.ORIGINAL_TEXT = "�밴�¡�YES����";
            //typeWriter.StartTypewriter();
    }
    void OnCloseButtonClick()
    {
        typeWriter.ORIGINAL_TEXT = "ȷ���˳���Ϸ��^v^";
        typeWriter.StartTypewriter();
        nowStatus = TabSelect.Close;
    }

    public void SwitchTabToTodayInformation()
    {
        nowStatus = TabSelect.TodayInformation;
        
        if (TodayInformationCoroutine == null)
        {
            TodayInformationCoroutine = StartCoroutine(TodayInformationTypeWriter());
        }
        else
        {
            StopCoroutine(TodayInformationCoroutine);
            TodayInformationCoroutine = StartCoroutine(TodayInformationTypeWriter());
        }
    }
    public void SwitchTabToTotalSituation()
    {
        nowStatus = TabSelect.TotalSituation;
        
        if (TotalSituationCoroutine == null)
        {
            TotalSituationCoroutine = StartCoroutine(TotalSituationTypeWriter());
        }
        else
        {
            StopCoroutine(TotalSituationCoroutine);
            TotalSituationCoroutine = StartCoroutine(TotalSituationTypeWriter());
        }
    }
    public void SwitchTabToNewDay()
    {
        nowStatus = TabSelect.NewDay;
        if (NewDayCoroutine == null)
        {
            NewDayCoroutine = StartCoroutine(NewDayTypeWriter());
        }
        else
        {
            StopCoroutine(NewDayCoroutine);
            NewDayCoroutine = StartCoroutine(NewDayTypeWriter());
        }
    }
    


    private IEnumerator TodayInformationTypeWriter()
    {
        typeWriter.ORIGINAL_TEXT = "�����ܹ��Ӵ�Ա������:"+WorkerNum;
        typeWriter.StartTypewriter();
        yield return new WaitForSeconds(1.2f);
        typeWriter.ORIGINAL_TEXT = "����"+SickWorkerNum+ "������״������";
        typeWriter.StartTypewriter();
        yield return new WaitForSeconds(1.2f);
        typeWriter.ORIGINAL_TEXT = SadWorkerNum+"������״������";
        typeWriter.StartTypewriter();
    }
    private IEnumerator TotalSituationTypeWriter()
    {
        typeWriter.ORIGINAL_TEXT = "��˾�󲿷ַ�Ա������״�������á���⣩";
        typeWriter.StartTypewriter();
        yield return new WaitForSeconds(1.8f);
        typeWriter.ORIGINAL_TEXT = "����״�������á���⣩";
        typeWriter.StartTypewriter();
    }
    private IEnumerator NewDayTypeWriter()
    {
        typeWriter.ORIGINAL_TEXT = "��Ҫ��ʼ�µ�һ��Ĺ�����";
        typeWriter.StartTypewriter();
        yield return new WaitForSeconds(1.1f);
        typeWriter.ORIGINAL_TEXT = "�ð�";
        typeWriter.StartTypewriter();
        yield return new WaitForSeconds(0.6f);
        typeWriter.ORIGINAL_TEXT = "����'YES'����ʼ��";
        typeWriter.StartTypewriter();

    }
}
