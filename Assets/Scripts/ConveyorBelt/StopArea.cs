using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopArea : MonoBehaviour
{
    public ConveyorBelt conveyorBelt;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dish")
        {
            conveyorBelt.isFull = true;
        }

    }
    void Start()
    {

    }
    void Update()
    {

    }

}
