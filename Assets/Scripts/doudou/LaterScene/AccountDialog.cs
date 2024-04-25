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
    //public Text dialogContent;//电视机上的对话框里的文字部分
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
            //typeWriter.ORIGINAL_TEXT = "请按下“YES”键";
            //typeWriter.StartTypewriter();
    }
    void OnCloseButtonClick()
    {
        typeWriter.ORIGINAL_TEXT = "确定退出游戏吗^v^";
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
        typeWriter.ORIGINAL_TEXT = "今日总共接待员工数量:"+WorkerNum;
        typeWriter.StartTypewriter();
        yield return new WaitForSeconds(1.2f);
        typeWriter.ORIGINAL_TEXT = "其中"+SickWorkerNum+ "人身体状况不适";
        typeWriter.StartTypewriter();
        yield return new WaitForSeconds(1.2f);
        typeWriter.ORIGINAL_TEXT = SadWorkerNum+"人心理状况不适";
        typeWriter.StartTypewriter();
    }
    private IEnumerator TotalSituationTypeWriter()
    {
        typeWriter.ORIGINAL_TEXT = "公司大部分分员工身体状况（良好、糟糕）";
        typeWriter.StartTypewriter();
        yield return new WaitForSeconds(1.8f);
        typeWriter.ORIGINAL_TEXT = "精神状况（良好、糟糕）";
        typeWriter.StartTypewriter();
    }
    private IEnumerator NewDayTypeWriter()
    {
        typeWriter.ORIGINAL_TEXT = "又要开始新的一天的工作吗";
        typeWriter.StartTypewriter();
        yield return new WaitForSeconds(1.1f);
        typeWriter.ORIGINAL_TEXT = "好吧";
        typeWriter.StartTypewriter();
        yield return new WaitForSeconds(0.6f);
        typeWriter.ORIGINAL_TEXT = "按下'YES'键开始吧";
        typeWriter.StartTypewriter();

    }
}
