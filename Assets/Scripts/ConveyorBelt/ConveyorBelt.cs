using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    private Vector3 direction;//���ʹ��ƶ�����
    public float speed;//�Ĵ��ƶ��ٶ�
    public bool isFull=false;//�Ĵ���ʳ���Ƿ�����
    void Start()
    {
        direction = new Vector2(1, 0);
    }

    void Update()
    {
        if(isFull==false)
            Move();
        
    }
    private void Move()//�Ĵ��ƶ�
    {
        transform.position += direction * Time.deltaTime * speed;
    }
}
