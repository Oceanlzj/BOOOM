using Assets.BasicModule.Factory;
using Assets.BasicModule.Model;
using System;
using UnityEngine;
using UnityEngine.U2D.Animation;

public enum DishStatus
{
  Unknow, Creating, Waiting, Destroy
}

public class IngredientItem : MonoBehaviour
{
  public int IngredientID = 0;

  public Ingredient Ingredient;
  public SpriteRenderer SR;
  public SpriteLibraryAsset Asset;

  public float createSpeed = 3f;         //盘子移动速度 暂定为3
  public Vector3 stopPos;   //盘子停止位置
  public int number;
  private DishStatus _status;

  private int OrderInLayerLast = 10;

  private Vector2 _distance;
  private Vector2 _mousePos;
  private bool _mouseDown = false;
  private Vector3 _velocity = Vector3.zero;
  public float retrunTime = 0.3f;

  private bool _canDestroy = false;

  public void SetPos(Vector3 _stopPos)
  {
    stopPos = _stopPos;
  }



  public void SetStatu(DishStatus status)
  {
    _status = status;
  }

  void Start()
  {
    Ingredient = DataFactory.Instance().GetIngedientByID(IngredientID);
    SR.sprite = Asset.GetSprite("Ingredient", IngredientID.ToString());
    OrderInLayerLast = SR.sortingOrder;
  }

  void Update()
  {
    _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    if (_status == DishStatus.Creating && transform.position != stopPos)
    {
      float step = createSpeed * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.position, stopPos, step);
    }
    else if (_status == DishStatus.Creating && transform.position == stopPos)
    {
      _status = DishStatus.Waiting;
    }
    else if (_status == DishStatus.Waiting && transform.position != stopPos)
    {
      if (!_mouseDown)
      {
        transform.position = Vector3.SmoothDamp(transform.position, stopPos, ref _velocity, retrunTime);
      }
    }
    else
    {

    }
  }
  private void Move(float speed)  //移动
  {

    transform.position = Vector3.MoveTowards(transform.position, stopPos, speed);
  }

  private void OnMouseDown()
  {
    if (_status == DishStatus.Waiting)
    {
      _mouseDown = true;
      _distance = new Vector2(transform.position.x, transform.position.y) - _mousePos;
      SR.sortingOrder = 100;
    }

  }

  private void OnMouseDrag()
  {
    if (_status == DishStatus.Waiting)
    {
      transform.position = _mousePos + _distance;
    }
  }

  private void OnMouseUp()
  {
    _mouseDown = false;
    SR.sortingOrder = OrderInLayerLast;
  }


  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "DropPort")
    {
      _canDestroy = true;
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.tag == "DropPort")
    {
      _canDestroy = false;
    }
  }

  private void OnDestroy()
  {
    ProcessSceneManager.Instance.RemoveDish(this);
  }
}
