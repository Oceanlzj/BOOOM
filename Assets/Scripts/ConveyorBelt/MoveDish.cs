using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class MoveDish : MonoBehaviour
{
    public float speed;//盘子移动速度 暂定为3
    private Vector3 direction;//盘子带移动方向
    private Vector2 mousePos;
    private Vector2 distance;
    private bool arriveStop = false;
    private bool touchDish = false;
    private bool onConveyorBelt = true;
    private bool mouseMove = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //print("mouseMove:" + mouseMove);
        //print("onConveyorBelt:" + onConveyorBelt);
        //print("arriveStop:" + arriveStop);
        //print("touchDish:" + touchDish);
       //if (Input.GetMouseButtonDown(0))
       //{
       //    print("mouseMove:" + mouseMove);
       //    mouseMove = true;
       //    distance = new Vector2(transform.position.x, transform.position.y) - mousePos;
       //}
       //else if (Input.GetMouseButtonUp(0))
       //{
       //    mouseMove = false;
       //}
       if (!mouseMove && onConveyorBelt && !arriveStop && !touchDish)
       {
           Move();
       }
    }

   private void FixedUpdate()
   {
       
   }

    private void OnMouseDown()
    {
        print("mouseMove:" + mouseMove);
        mouseMove = true;
        distance = new Vector2(transform.position.x, transform.position.y) - mousePos;
    }

    private void OnMouseDrag()
    {
        transform.position = mousePos + distance;
    }
    
    private void OnMouseUp()
    {
        mouseMove = false;
    }
    
    private void Move()//履带移动
    {
        transform.position += direction * Time.deltaTime * speed;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dish")
        {
            //print("盘子停盘了");
            touchDish = true;
        }
        else if (collision.tag == "StopArea")
        {
           // print("盘子停止了");
            arriveStop = true;
        }
        else if (collision.tag == "ConveyorBelt")
        {
            //print("盘子进来了");
            arriveStop = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Dish")
        {
            //print("盘子不盘了");
            touchDish = false;
        }
        else if (collision.tag == "StopArea")
        {
            //print("盘子不停了");
            arriveStop = false;
        }
        else if (collision.tag == "ConveyorBelt")
        {
            //print("盘子出来了");
            onConveyorBelt = false;
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Dish")
        {
            print("Stay盘子不盘了");
            touchDish = false;
        }
        else if (other.tag == "StopArea")
        {
            print("Stay盘子不停了");
            arriveStop = false;
        }
        else if (other.tag == "ConveyorBelt")
        {
            print("Stay盘子出来了");
            onConveyorBelt = false;
        }
    }
}
