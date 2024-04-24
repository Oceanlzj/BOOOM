using Assets.BasicModule.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class CookedDish : MonoBehaviour
{
  // Start is called before the first frame update
  public Dish Dish;
  public SpriteRenderer sr;
  public SpriteLibraryAsset lib;



  void Start()
  {
    sr.sprite = lib.GetSprite("Dish", Dish.ID.ToString());
  }

  // Update is called once per frame
  void Update()
  {

  }


}
