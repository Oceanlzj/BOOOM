using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void LoadMainPage()//���ص���ҳ��
    {
        SceneManager.LoadScene(0);
    }
    public void LoadProcessPage()//���ص��ӹ�ҳ��
    {
        SceneManager.LoadScene(1);
    }
}
