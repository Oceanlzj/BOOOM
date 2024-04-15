using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    private Vector3 direction;//传送带移动方向
    public float speed;//履带移动速度
    public bool isFull=false;//履带上食材是否已满
    void Start()
    {
        direction = new Vector2(1, 0);
    }

    void Update()
    {
        if(isFull==false)
            Move();
        
    }
    private void Move()//履带移动
    {
        transform.position += direction * Time.deltaTime * speed;
    }
}
