using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DishManager : MonoBehaviour
{
    private Vector3 _position;
    public int maxNum;
    private int curNum;
    public float seconds;

    public MoveDish _dish;

    // Start is called before the first frame update
    void Start()
    {
        curNum = 0;
        _position = GetComponent<Transform>().position;
        StartCoroutine(StartNewDay());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {

    }

    private IEnumerator StartNewDay()
    {
        while (curNum < maxNum)
        {
            curNum++;
            MoveDish dish = Instantiate(_dish);
            dish.transform.position = _position;

            if (curNum >= maxNum)
            {
                print("创建结束");
                break;
            }

            yield return new WaitForSeconds(seconds);
        }
    }
}
   
