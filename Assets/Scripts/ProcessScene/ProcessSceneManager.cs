using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UIElements;

public enum ManagerStutes{
    Unknow, Creating, Waiting, Destroy
}

public class ProcessSceneManager : Singleton<ProcessSceneManager>
{
    public IngredientItem dishProfab;
    //public Transform startPoint;
    public Transform[] Pipes;

    private List<IngredientItem> _dishesList;
    private int _destroyNum;
    
    public float seconds;       // 创建时间间隔


    private ManagerStutes _stutes;
    
    void Start()
    {
        _dishesList = new List<IngredientItem>();
        _stutes = ManagerStutes.Creating;
        StartCoroutine(StartNewDay());
    }

    // Update is called once per frame
    void Update()
    {
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
        IngredientItem dish = Instantiate(dishProfab, Pipes[number].GetChild(0).position, Quaternion.identity);
        _dishesList.Add(dish);
        dish.SetPos(Pipes[number].GetChild(1).position + new Vector3(0, 1, 0));
        dish.number = number;
        dish.SetStatu(DishStatus.Creating);
    }

    public void RemoveDish(IngredientItem dish)
    {
        _destroyNum = dish.number;
        _dishesList.Remove(dish);
        _stutes = ManagerStutes.Destroy;
    }
}
   
