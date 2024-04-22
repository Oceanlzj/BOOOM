using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public float speed = 0.1f;

    private bool _inArea = false;

    private bool _vector = false;

    private bool _clockwise = true;
    
    
    
    //private float origionZ;
    //private Quaternion targetRotation;
    //public float RotateAngle = 60;
    //public int count = 0;
    //private bool i;
    
    //private Transform from;
    //private Transform to;
//
    //private float timeCount = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        //from.rotation = new Quaternion(0,0,75,0);
        //to.rotation = new Quaternion(0,0,-150,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_inArea && Input.GetMouseButtonDown(0))
        {
            _vector = true;
        }
        
        //transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, timeCount);
        //timeCount = timeCount + Time.deltaTime;
        //transform.Rotate();
    }

    private void FixedUpdate()
    {
        if (_vector)
        {
            print("Vector");
        }
        else
        {
            if (transform.rotation.z > 0.608f)
            {
                _clockwise = true;
            }
            else if (transform.rotation.z < -0.966f)
            {
                _clockwise = false;
            }
            
            Vector3 direction = transform.forward; 
            if (_clockwise)
            {
                direction = -direction;
            }
            
            transform.RotateAround(transform.position, direction, speed+Time.deltaTime);
            
            
            
            
            //z轴 75,-150
            //targetRotation = Quaternion.Euler(0,0,RotateAngle*count+origionZ) * Quaternion.identity;
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2);
            ////避免误差
            //if (Quaternion.Angle(targetRotation, transform.rotation) < 1)
            //    transform.rotation = targetRotation;
            
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DropPort")
        {
            _inArea = true;
            print("enter");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "DropPort")
        {
            _inArea = false;
            print("Exit");
        }
    }
    
}
