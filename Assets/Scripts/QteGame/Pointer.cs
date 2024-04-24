using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    
    public float speed = 0.1f;

    private bool _isActive = false;
    
    private bool _inArea = false;

    private bool _stop = false;

    private bool _clockwise = true;

    private CutQte _parentGameObject;
    
    // Start is called before the first frame update
    void Start()
    {
        // 获取父物体
        Transform parentTransform = transform.parent;
 
        // 如果父物体存在
        if (parentTransform == null)
        {
           
            Debug.Log("这是一个根物体，没有父物体。");
        }
        else
        {
            _parentGameObject = parentTransform.gameObject.GetComponent<CutQte>();
        }

        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isActive && Input.GetMouseButtonDown(0))
        {
            _stop = true;
           // _parentGameObject.CheckVector(_inArea);
            
        }
    }

    private void FixedUpdate()
    {
        if (!_stop && _isActive)
        {
            // 实际测试获得 左侧0.608f，右侧-0.966f
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
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DropPort")
        {
            _inArea = true;
            //print("enter");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "DropPort")
        {
            _inArea = false;
            //print("Exit");
        }
    }

    public void Reset()
    {
        _stop = false;
    }

    public void StartGame()
    {
        _isActive = true;
    }

    public void StopGame()
    {
        _isActive = false;
    }
    
}
