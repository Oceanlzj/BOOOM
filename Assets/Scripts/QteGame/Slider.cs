using System;
using UnityEngine;

public class Slider : MonoBehaviour
{
  public float speed = 3f;

  private Vector3 _minXPos;
  private Vector3 _maxXPos;
  private bool _inArea = false;
  private bool _isRight = false;
  private bool _stop = false;
  private bool _isActive = false;
  private float _timeStart;
  private float _timeBegin;
  private float _timeEnd;
  private bool _totalTimerStarted = false;
  private float GameTimeToFinish = 2.0f;
  private CookQte _parentGameObject;
  public Animator HintAnimator;
  void Start()
  {
    // 获取父物体
    Transform parentTransform = transform.parent;

    // 如果父物体存在
    if (parentTransform == null)
    {

      Debug.Log("这是一个根物体，没有父物体。");
    }
    else
    {
      _parentGameObject = parentTransform.gameObject.GetComponent<CookQte>();
    }
  }


  void Update()
  {
    if (!_isActive)
    {
      return;
    }
    if (Input.GetKeyDown(KeyCode.Space))
    {
      _parentGameObject.transform.position -= new Vector3(0, 0.05f, 0);
      _isRight = true;
    }
    else if (Input.GetKeyUp(KeyCode.Space))
    {
      _parentGameObject.transform.position += new Vector3(0, 0.05f, 0);
      _isRight = false;
    }

    if (_inArea)
    {
      _timeStart += Time.deltaTime;
      if (_timeStart >= GameTimeToFinish)
      {
        _stop = true;
        _parentGameObject.CheckVector();
      }
    }
    else
    {
      _timeStart = 0;
    }
  }

  private void FixedUpdate()
  {
    if (_stop)
    {
      return;
    }
    if (_isRight)
    {
      if (transform.position.x > _maxXPos.x)
      {
        transform.position = _maxXPos;
        return;
      }
      float step = speed * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.position, _maxXPos, step);

    }
    else
    {
      if (transform.position.x <= _minXPos.x)
      {
        transform.position = _minXPos;
        return;
      }
      float step = speed * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.position, _minXPos, step);
    }
  }

  public void SetPos(Vector3 minX, Vector3 maxX)
  {
    _minXPos = minX;
    _maxXPos = maxX;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "DropPort")
    {
      _inArea = true;
      if (!_totalTimerStarted)
      {
        _totalTimerStarted = true;
        _timeBegin = Time.time;
      }
      if(!_isRight)
      {
        HintAnimator.Play("MouseHint");
      }
    }

  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.tag == "DropPort")
    {
      _inArea = false;
    }
    if(_isRight)
    {
      HintAnimator.Play("MouseIdle");
    }
    else
    {
      HintAnimator.Play("MouseHint");
    }
  }

  public void StartGame()
  {
    HintAnimator.Play("MouseHint");
    transform.position = _minXPos;
    _isActive = true;
    _stop = false;
    _totalTimerStarted = false;
  }

  public void StopGame()
  {
    _isActive = false;
    _inArea = false;
    transform.position = _minXPos;
    _timeEnd = Time.time;
    HintAnimator.Play("MouseIdle");
  }

  public float GetTotalTime()
  {
    return _timeEnd - _timeBegin;
  }

  //Requiered time / Time from first enter trigger to finish;
  //Higher is better
  public float GetStayTimePercentage()
  {
    return GameTimeToFinish / GetTotalTime();
  }

  public float GetHoldTime()
  {
    return _timeStart;
  }
}
