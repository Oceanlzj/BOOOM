using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public float speed = 0.1f;

    private bool _inArea = false;

    private bool _vector = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_inArea && Input.GetMouseButtonDown(0))
        {
            _vector = true;
        }
    }

    private void FixedUpdate()
    {
        if (_vector)
        {
            print("Vector");
        }
        else
        {
            transform.RotateAround(transform.position, -transform.forward, speed+Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DropPort")
        {
            _inArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "DropPort")
        {
            _inArea = false;
        }
    }
}
