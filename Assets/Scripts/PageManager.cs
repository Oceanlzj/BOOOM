using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    public GameObject DialogPage;
    public GameObject OrderPage;
    public bool isClickUp;//是否点击向上划按钮
    public bool isClickDown;//是否点击向下划按钮
    public float speed;//页面移动速度
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
