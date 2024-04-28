using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QteStatus
{
  Unknow, Waiting, Cutting, FinishCutting, Cooking, FinishCooking, WaitingForCompress
}

public class QteManager : Singleton<QteManager>
{
  public CutQte CutQte;
  public CookQte CookQte;

  public QteStatus Status {  get; set; }



  public void StartCutGame()
  {
    //print("开始切菜");
    Status = QteStatus.Cutting;
    CutQte.StartGame();
  }

  public void FinishCutGame()
  {
    //print("完成切菜");
    Status = QteStatus.FinishCutting;
    CutQte.StopGame();
  }

  public void StopCutGame()
  {
    Status = QteStatus.Waiting;
    CutQte.StopGame();
  }

  public void StartCookGame()
  {
    //print("开始烹饪");
    Status = QteStatus.Cooking;
    CookQte.StartGame();
  }

  public void FinishCookGame()
  {
    //print("完成烹饪");
    Status = QteStatus.FinishCooking;
    CutQte.StopGame();
  }
  public void StopCookGame()
  {
    Status = QteStatus.Waiting;
    CookQte.StopGame();
  }

  public int GetCutVectorNum()
  {
    return CutQte.GetVectorNum();
  }

  public float GetCookTotalTime()
  {
    return CookQte.GetStayTimePercentage();
  }

  // Start is called before the first frame update
  void Start()
  {
    Status = QteStatus.Waiting;
  }

  // Update is called once per frame
  void Update()
  {
  }
}
