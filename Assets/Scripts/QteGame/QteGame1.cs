using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class QteGame1 : MonoBehaviour
{
    public Transform progressScroll;    // 进度条
    public Transform slider;            // 滑块
    public Transform area;              // 识别区
    
    // Start is called before the first frame update
    void Start()
    {
        Bounds progressScrollBounds = progressScroll.GetComponent<Renderer>().bounds;
        //Bounds sliderBounds = slider.GetComponent<Renderer>().bounds;
        Bounds areaBounds = area.GetComponent<Renderer>().bounds;

        Vector3 minSliderPos = new Vector3(-progressScrollBounds.size.x / 2 + progressScroll.position.x, progressScroll.position.y);
        Vector3 maxSliderPos = new Vector3(progressScrollBounds.size.x / 2 + progressScroll.position.x, progressScroll.position.y);

        float minAreaX = -progressScrollBounds.size.x / 2 + areaBounds.size.x / 2;
        float maxAreaX = progressScrollBounds.size.x / 2 - areaBounds.size.x / 2;

        Vector3 areaPos =new Vector3(Random.Range(minAreaX, maxAreaX) + progressScroll.position.x, progressScroll.position.y);
        area.transform.position = areaPos;
        
        slider.GetComponent<Slider>().SetPos(minSliderPos, maxSliderPos);
        slider.transform.position = minSliderPos;
        //slider.GetComponent<Slider>().SetAreaPos(areaPos, areaBounds.size.x);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
