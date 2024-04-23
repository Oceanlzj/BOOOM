using Assets.BasicModule.Factory;
using Assets.BasicModule.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class DishPack : MonoBehaviour
{
  public int DishID = 0;
  public Dish dish;
  public SpriteRenderer sr;
  public SpriteLibraryAsset lib;
  //-6 - 1.6
  private enum PackStatus
  {
    OffPlate = 0,
    OnPlate = 1,
  }

  private Vector3 stopPos;   //≈Ã◊”Õ£÷πŒª÷√
  public Vector3 InitPos;   //≈Ã◊”Õ£÷πŒª÷√
  private Vector2 _distance;

  private Vector2 _mousePos;
  private PackStatus _status;
  private bool _mouseDown = false;
  private Vector3 _velocity = Vector3.zero;
  public float retrunTime = 0.3f;

  private bool _canDestroy = false;
  // Start is called before the first frame update
  void Start()
  {
    stopPos = InitPos;
    dish = DataFactory.Instance().GetDishByID(DishID);
    _status = PackStatus.OffPlate;
    sr.sprite = lib.GetSprite("Dish", DishID.ToString());
  }

  // Update is called once per frame
  void Update()
  {
    _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    if (_status == PackStatus.OffPlate && transform.position != stopPos)
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
      stopPos = transform.position;
    }
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
    }
  }

}
