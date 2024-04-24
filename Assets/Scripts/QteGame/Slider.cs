using System;
using UnityEngine;

public class Slider : MonoBehaviour
{
    public float speed = 3f;
    
    private Vector3 _minXPos;
    private Vector3 _maxXPos;
    private bool _inArea = false;
    private bool _isRight = false;
    private bool _stop = false;
    private bool _isActive = false;
    private float _timeStart;
    private CookQte _parentGameObject;
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
            _parentGameObject = parentTransform.gameObject.GetComponent<CookQte>();
        }
    }

    
    void Update()
    {
        if (!_isActive)
        {
            return;
        }
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
                _parentGameObject.CheckVector();
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

    public void StartGame()
    {
        transform.position = _minXPos;
        _isActive = true;
    }

    public void StopGame()
    {
        _isActive = false;
        _inArea = false;
        transform.position = _minXPos;
    }

    public float GetHoldTime()
    {
        return _timeStart;
    }
}
