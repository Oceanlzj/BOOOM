using Assets.BasicModule.Factory;
using Assets.BasicModule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.Animation;

public enum DishStatus
{
  Unknow, Creating, Waiting, Destroy, Cooking
}

public class IngredientItem : MonoBehaviour
{
  //public int IngredientID = 0;
  public Vector3 initPos;

  public Collider2D machine;

  public List<Vector3> SnapPoints;
  public List<bool> Snapped;

  public int snappedIndex = -1;





  public int PipeNum;

  public Ingredient Ingredient;
  public SpriteRenderer SR;
  public SpriteLibraryAsset Asset;

  public float createSpeed = 3f;      
  public Vector3 stopPos;  
  public DishStatus _status;

  private int OrderInLayerLast = 10;

  private Vector2 _distance;
  private Vector2 _mousePos;
  public bool _mouseDown = false;
  private Vector2 _velocity = Vector2.zero;
  public float retrunTime = 0.3f;

  public bool OnMachine = false;

  public float OnMouseDownScale;
  public Transform Transform;

  public void SetPos(Vector3 _stopPos)
  {
    initPos = _stopPos;
    stopPos = _stopPos;
  }


  public void UnSelect()
  {
    OnMachine = false;
    stopPos = initPos;
    if (snappedIndex != -1)
    {
      Snapped[snappedIndex] = false;
    }
  }
  public void SetStatu(DishStatus status)
  {
    _status = status;
  }

  void Start()
  {
    SR.sprite = Asset.GetSprite("Ingredient", Ingredient.ID.ToString());
    OrderInLayerLast = SR.sortingOrder;
  }

  void Update()
  {
    _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    if (_status == DishStatus.Creating && transform.position != stopPos)
    {
      float step = createSpeed * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.position, stopPos, step);
      return;
    }

    if (_status == DishStatus.Creating && transform.position == stopPos)
    {
      _status = DishStatus.Waiting;
      return;
    }

    if (transform.position != stopPos)
    {
      if (!_mouseDown)
      {
        ///_status = DishStatus.Returning;
        transform.position = Vector2.SmoothDamp(transform.position, stopPos, ref _velocity, retrunTime);
      }
    }
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

  private void OnMouseEnter()
  {
    Transform.localScale += Vector3.one * OnMouseDownScale;
  }

  private void OnMouseExit()
  {
    Transform.localScale -= Vector3.one * OnMouseDownScale;
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

    if (OnMachine)
    {
      List<double> Distance = new List<double>();
      foreach (Vector3 sp in SnapPoints)
      {
        Distance.Add(Vector3.Distance(transform.position, sp));
      }

      int index = Distance.IndexOf(Distance.Min());

      if (Snapped[index])
      {
        OnMachine = false;
      }
      else
      {
        if (snappedIndex != -1)
        {
          Snapped[snappedIndex] = false;
        }
        stopPos = SnapPoints[index];
        Snapped[index] = true;
        snappedIndex = index;
      }



    }

  }


  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Machine" && _mouseDown)
    {
      OnMachine = true;
    }
  }


  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.tag == "Machine" && _mouseDown)
    {
      stopPos = initPos;
      OnMachine = false;

      if (snappedIndex != -1)
      {
        Snapped[snappedIndex] = false;
      }
      snappedIndex = -1;

    }

  }

  private void OnDestroy()
  {
    if (snappedIndex != -1)
    {
      Snapped[snappedIndex] = false;
    }
    Destroy(gameObject);
  }
}
