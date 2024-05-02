using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutQte : MonoBehaviour
{
  public Transform poiter;            // 滑块
  public Transform area;              // 识别区
  public int maxGameNum;

  private int _vectorNum;
  private int _gameNum;

  private Pointer _pointer;
  // Start is called before the first frame update
  void Start()
  {
    _vectorNum = 0;
    _gameNum = 0;
    _pointer = poiter.GetComponent<Pointer>();
    area.gameObject.SetActive(false);
  }

  // Update is called once per frame
  void Update()
  {
    //InitGame();
    //print(area.transform.rotation.z);
  }

  public void StartGame()
  {
    _vectorNum = 0;
    _gameNum = 0;
    _pointer.StartGame();
    area.gameObject.SetActive(true);
    InitGame();
  }

  public void StopGame()
  {
    //_vectorNum = 0;
    //_gameNum = 0;
    area.gameObject.SetActive(false);
    _pointer.StopGame();
  }

  public int GetVectorNum()
  {
    return _vectorNum;
  }

  public void InitGame()
  {
    //30~-155
    area.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-150f, 30f));
    _pointer.Reset();
  }

  public void CheckVector(bool vector)
  {
    _gameNum++;
    if (vector)
    {
      _vectorNum++;
    }

    if (_gameNum >= maxGameNum)
    {
      QteManager.Instance.FinishCutGame();
      StopGame();
    }
    else
    {
      InitGame();
    }
  }
}
