using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UIElements;

public enum ManagerStutes{
    Unknow, Creating, Waiting, Destroy
}

public class DishManager : Singleton<DishManager>
{
    public MoveDish dishProfab;
    //public Transform startPoint;
    public Transform[] Pipes;

    private List<MoveDish> _dishesList;
    private int _destroyNum;
    
    public float seconds;       // 创建时间间隔


    private ManagerStutes _stutes;
    
    void Start()
    {
        _dishesList = new List<MoveDish>();
        _stutes = ManagerStutes.Creating;
        StartCoroutine(StartNewDay());
    }

    // Update is called once per frame
    void Update()
    {
       if (_stutes == ManagerStutes.Destroy && _dishesList.Count < Pipes.Length)
       {
           // 有盘子销毁
          // bool frontFinish = true;
           
          // _dishesList.ForEach(dish =>
          // {
          //     if (dish.number - _destroyNum == 1)
          //     {
          //         frontFinish = false;
          //         dish.SetPos(Pipes[_destroyNum].position);
          //         dish.SetStatu(DishStatus.Creating);
          //         dish.number--;
          //         _destroyNum++;
          //     }
          // });
           
          //if (frontFinish)
          //{
          CreateDish(_destroyNum);
          _destroyNum = 0;
          _stutes = ManagerStutes.Waiting;
           //}
       }
    }
    
    private IEnumerator StartNewDay()
    {
        int curNum = 0;
        while (curNum < Pipes.Length)
        {
            CreateDish(curNum);
            curNum++;
            yield return new WaitForSeconds(seconds);
        }

        _stutes = ManagerStutes.Waiting;
    }

    private void CreateDish(int number)
    {
        MoveDish dish = Instantiate(dishProfab, Pipes[number].GetChild(0).position, Quaternion.identity);
        _dishesList.Add(dish);
        dish.SetPos(Pipes[number].GetChild(1).position);
        dish.number = number;
        dish.SetStatu(DishStatus.Creating);
    }

    public void RemoveDish(MoveDish dish)
    {
        _destroyNum = dish.number;
        _dishesList.Remove(dish);
        _stutes = ManagerStutes.Destroy;
    }
}
   
