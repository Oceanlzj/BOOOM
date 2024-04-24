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
    public void LoadMainPage()//加载到主页面
    {
        SceneManager.LoadScene(0);
    }
    public void LoadProcessPage()//加载到加工页面
    {
        SceneManager.LoadScene(1);
    }
}
