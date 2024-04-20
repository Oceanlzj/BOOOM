using UnityEngine;

public class Slider : MonoBehaviour
{
    public float force = 200f;
    public float speed = 3f;
    public float interval = 0f;
    
    private Vector3 _minXPos;
    private Vector3 _maxXPos;
    private float _sizeX;

    private Vector3 _areaPos;
    private float _areaSizeX;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckVector())
        {
            print("Vector");
            return;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            float step = force * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _maxXPos, step);
        }
        else
        {
            if (transform.position.x <= _minXPos.x)
            {
                transform.position = _minXPos;
            }
            else if (transform.position.x > _maxXPos.x)
            {
                transform.position = _maxXPos;
            }
            else
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, _minXPos, step);
            }
        }
    }

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.tag == "DropPort")
    //    {
    //        print("OnTriggerStay2D");
    //        _vector = true;
    //    }
    //}

    public void SetPos(Vector3 minX, Vector3 maxX, float sizeX)
    {
        _minXPos = minX;
        _maxXPos = maxX;
        _sizeX = sizeX;

    }

    public void SetAreaPos(Vector3 pos, float sizeX)
    {
        _areaPos = pos;
        _areaSizeX = sizeX;
    }

    private bool CheckVector()
    {
        if (transform.position.x-_sizeX/2 < _areaPos.x-_areaSizeX/2-interval && transform.position.x+_sizeX/2 > _areaPos.x+_areaSizeX/2+interval)
        {
            return true;
        }

        return false;
    }
}
