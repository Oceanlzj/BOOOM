using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    public GameObject DialogPage;
    public GameObject OrderPage;
    public bool isClickUp;//�Ƿ������ϻ���ť
    public bool isClickDown;//�Ƿ������»���ť
    public float speed;//ҳ���ƶ��ٶ�
    void Start()
    {
        
    }

    void Update()
    {
        TurnToDialogPage();
        TurnToAccountPage();
    }
    public void TurnToDialogPage()
    {
        if(isClickUp==true&&OrderPage.transform.position.y<12)
        {
            OrderPage.transform.position += new Vector3(0, 1) * speed * Time.deltaTime;
        }
    }
    public void TurnToAccountPage()
    {
        if (isClickDown == true && DialogPage.transform.position.y>-12)
        {
            DialogPage.transform.position -= new Vector3(0, 1) * speed * Time.deltaTime;
        }
    }
}
