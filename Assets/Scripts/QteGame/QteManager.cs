using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum QteStatus
{
    Unknow, Waiting, Cutting, FinishCutting,Cooking, FinishCooking
}

public class QteManager : Singleton<QteManager>
{
    public CutQte CutQte;
    public CookQte CookQte;

  public ProcessSceneManager ProcessSceneManager;
    
    private QteStatus _status;

    public void StartQteGame()
    {
        StartCutGame();
    }
    
    public void StartCutGame()
    {
        //print("开始切菜");


        _status = QteStatus.Cutting;
        //CutQte.StartGame();
    }
    
    public void FinishCutGame()
    {
        print("完成切菜");
        _status = QteStatus.FinishCutting;
        //CutQte.StopGame();
    }
    
    public void StopCutGame()
    {
        _status = QteStatus.Waiting;
        //CutQte.StopGame();
    }
    
    public void StartCookGame()
    {
        print("开始烹饪");
        _status = QteStatus.Cooking;
        //CookQte.StartGame();
    }
    
    public void FinishCookGame()
    {
        print("完成烹饪");
        _status = QteStatus.FinishCooking;
        //CutQte.StopGame();
    }
    public void StopCookGame()
    {
        _status = QteStatus.Waiting;
        //CookQte.StopGame();
    }

    public int GetCutVectorNum()
    {
        return CutQte.GetVectorNum();
    }

    public float GetResidualTime()
    {
        return CookQte.GetHoldTime();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _status = QteStatus.Waiting;
    }

    // Update is called once per frame
    void Update()
    {
        if (_status == QteStatus.FinishCutting)
        {
            StartCookGame();
        }
    }
}
