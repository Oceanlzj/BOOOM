using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class CookQte : MonoBehaviour
{
    public Transform progressScroll;    // 进度条
    public Transform slider;            // 滑块
    public Transform area;              // 识别区
    
    // Start is called before the first frame update
    void Start()
    {
        area.gameObject.SetActive(false);
        
        Bounds progressScrollBounds = progressScroll.GetComponent<Renderer>().bounds;

        Vector3 minSliderPos = new Vector3(-progressScrollBounds.size.x / 2 + progressScroll.position.x, progressScroll.position.y);
        Vector3 maxSliderPos = new Vector3(progressScrollBounds.size.x / 2 + progressScroll.position.x, progressScroll.position.y);

        slider.GetComponent<Slider>().SetPos(minSliderPos, maxSliderPos);
        slider.transform.position = minSliderPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        Bounds progressScrollBounds = progressScroll.GetComponent<Renderer>().bounds;
        Bounds areaBounds = area.GetComponent<Renderer>().bounds;
        float minAreaX = -progressScrollBounds.size.x / 2 + areaBounds.size.x / 2;
        float maxAreaX = progressScrollBounds.size.x / 2 - areaBounds.size.x / 2;
        Vector3 areaPos =new Vector3(Random.Range(minAreaX, maxAreaX) + progressScroll.position.x, progressScroll.position.y);
        area.transform.position = areaPos;
        area.gameObject.SetActive(true);
        slider.GetComponent<Slider>().StartGame();
    }
    
    public void StopGame()
    {
        area.gameObject.SetActive(false);
        slider.GetComponent<Slider>().StopGame();
    }
    
    public void CheckVector()
    {
        StopGame();
        QteManager.Instance.FinishCookGame();
    }
    
    public float GetHoldTime()
    {
        return slider.GetComponent<Slider>().GetHoldTime();
    }
    
}
