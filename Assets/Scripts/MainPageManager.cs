using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPageManager : MonoBehaviour
{
    public TVSnowEffect TVSnowEffect;
    public GameObject screen;
    private bool isStop=false;
    void Start()
    {
        StartCoroutine(stopTVSnow());
    }

    // Update is called once per frame
    void Update()
    {
        if(isStop==false)
            TVSnowEffect.noiseSpeed += 10000*Time.deltaTime;
    }
    public IEnumerator stopTVSnow()
    {
        screen.SetActive(false);
        yield return new WaitForSeconds(3f);
        isStop = true;
        TVSnowEffect.noiseSpeed = 0;
        yield return new WaitForSeconds(0.5f);
        screen.SetActive(true);
        Destroy(TVSnowEffect.gameObject);
    }
}
