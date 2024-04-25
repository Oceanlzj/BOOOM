using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountPageManagement : MonoBehaviour
{
    public TVSnowEffect TVSnowEffect;
    public GameObject screen;
    private bool isStop = false;
    private Coroutine tvSnow;
    void Start()
    {


    }
    private void OnEnable()
    {
        if (tvSnow == null)
        {
            tvSnow = StartCoroutine(stopTVSnow());
        }
        else
        {
            StopCoroutine(stopTVSnow());
            TVSnowEffect.noiseSpeed = 0.00011f;
            isStop = false;
            tvSnow = StartCoroutine(stopTVSnow());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isStop == false)
            TVSnowEffect.noiseSpeed += 10000 * Time.deltaTime;

    }
    public IEnumerator stopTVSnow()
    {
        screen.SetActive(false);
        yield return new WaitForSeconds(3f);
        isStop = true;
        TVSnowEffect.noiseSpeed = 0;
        yield return new WaitForSeconds(0.5f);
        screen.SetActive(true);
        TVSnowEffect.gameObject.SetActive(false);
    }
}