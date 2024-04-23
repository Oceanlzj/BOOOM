using System;
using UnityEngine;

public class Slider : MonoBehaviour
{
    //public float force = 200f;
    public float speed = 3f;
    
    private Vector3 _minXPos;
    private Vector3 _maxXPos;
    //private float _sizeX;
    //private Vector3 _areaPos;
    //private float _areaSizeX;
    
    private bool _inArea = false;

    private bool _isRight = false;
    
    private bool _stop = false;

    private float _timeStart;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       // CheckVector();
        if (Input.GetMouseButtonDown(0))
        {
            _isRight = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isRight = false;
        }

        if (_inArea)
        {
            _timeStart += Time.deltaTime;
            if (_timeStart >= 3)
            {
                _stop = true;
            }
        }
        else
        {
            _timeStart = 0;
        }
    }

    private void FixedUpdate()
    {
        if (_stop)
        {
            return;
        }
        if (_isRight)
        {
            if (transform.position.x > _maxXPos.x)
            {
                transform.position = _maxXPos;
                return;
            }
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _maxXPos, step);
            
        }
        else
        {
            if (transform.position.x <= _minXPos.x)
            {
                transform.position = _minXPos;
                return;
            }
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _minXPos, step);
        }
    }

    public void SetPos(Vector3 minX, Vector3 maxX)
    {
        _minXPos = minX;
        _maxXPos = maxX;
        //_sizeX = sizeX;

    }

    //public void SetAreaPos(Vector3 pos, float sizeX)
    //{
    //    _areaPos = pos;
    //    _areaSizeX = sizeX;
    //}

    //private void CheckVector()
    //{
        //if (transform.position.x-_sizeX/2 < _areaPos.x-_areaSizeX/2 && transform.position.x+_sizeX/2 > _areaPos.x+_areaSizeX/2)
        //{
        //    _vector = true;
        //}
    //}


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
