using Assets.BasicModule.Model;
using Assets.BasicModule.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TestMain : MonoBehaviour
{


  void Start()
  {
    Assets.BasicModule.Factory.DataFactory.Instance().getTaskByID(0);
  }

  void Update()
  {

  }
}
