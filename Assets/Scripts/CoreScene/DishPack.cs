using Assets.BasicModule.Factory;
using Assets.BasicModule.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class DishPack : MonoBehaviour
{
  public int DishID = 0;
  public Dish dish;
  public SpriteRenderer sr_sticker;
  public SpriteRenderer sr_Pack;
  public SpriteLibraryAsset lib;
  //-6 - 1.6
  private enum PackStatus
  {
    OffPlate = 0,
    OnPlate = 1,
  }

  private int initSO_Sticker;
  private int initSO_Pack;

  public List<Vector3> SnapPoints;
  public List<bool> Snapped;
  private Vector3 stopPos;   //≈Ã◊”Õ£÷πŒª÷√
  public Vector3 InitPos;   //≈Ã◊”Õ£÷πŒª÷√
  private Vector2 _distance;

  public bool isServing = false;
  private Vector2 _mousePos;
  private PackStatus _status;
  private bool _mouseDown = false;
  private Vector3 _velocity = Vector3.zero;
  public float retrunTime = 0.3f;
  private int snappedIndex = -1;
  // Start is called before the first frame update
  void Start()
  {
    stopPos = InitPos;
    dish = DataFactory.Instance().GetDishByID(DishID);
    _status = PackStatus.OffPlate;
    sr_sticker.sprite = lib.GetSprite("Dish", DishID.ToString());
  }

  // Update is called once per frame
  void Update()
  {
    _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    if (transform.position != stopPos && !isServing)
    {
      if (!_mouseDown)
      {
        transform.position = Vector3.SmoothDamp(transform.position, stopPos, ref _velocity, retrunTime);
      }
    }
  }
  private void Move(float speed)  //“∆∂Ø
  {

    transform.position = Vector3.MoveTowards(transform.position, stopPos, speed);
  }

  private void OnMouseDown()
  {

    _mouseDown = true;
    _distance = new Vector2(transform.position.x, transform.position.y) - _mousePos;
    initSO_Pack = sr_Pack.sortingOrder;
    initSO_Sticker = sr_sticker.sortingOrder;

    sr_sticker.sortingOrder = 100;
    sr_Pack.sortingOrder = 99;

  }

  public void Hide()
  {
    this.gameObject.SetActive(false);
  }
  public void UnServerFromPlate()
  {
    isServing = false;
    _status = PackStatus.OffPlate;
    stopPos = InitPos;
    if (snappedIndex != -1)
    {
      Snapped[snappedIndex] = false;
    }
  }



  private void OnMouseDrag()
  {
    Vector2 V = (_mousePos + _distance);
    transform.position = new Vector3(V.x, V.y, -8);

  }

  private void OnMouseUp()
  {
    _mouseDown = false;
    if (_status == PackStatus.OnPlate)
    {
      List<double> Distance = new List<double>();
      foreach (Vector3 sp in SnapPoints)
      {
        Distance.Add(Vector3.Distance(transform.position, sp));
      }

      int index = Distance.IndexOf(Distance.Min());


      if (Snapped[index])
      {
        _status = PackStatus.OffPlate;

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
    sr_sticker.sortingOrder = initSO_Sticker;
    sr_Pack.sortingOrder = initSO_Pack;
  }


  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Plate" && _mouseDown)
    {
      _status = PackStatus.OnPlate;
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.tag == "Plate" && _mouseDown)
    {
      stopPos = InitPos;
      _status = PackStatus.OffPlate;
      if (snappedIndex != -1)
      {
        Snapped[snappedIndex] = false;
      }
      snappedIndex = -1;
    }
  }

  public void OnDestroy()
  {
    if (snappedIndex != -1)
    {
      Snapped[snappedIndex] = false;
    }
    Destroy(gameObject);
  }

}
