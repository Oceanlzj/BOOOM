using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVSnowEffect : MonoBehaviour
{
    public Material tvMaterial;
    public float noiseStrength = 1.0f;
    public float noiseSpeed = 1.0f;

    private float offset;

    void Start()
    {
        offset = Random.Range(0f, 1f); // 随机生成一个起始偏移值
    }

    void Update()
    {
        offset += Time.deltaTime * noiseSpeed; // 根据速度更新偏移值
        tvMaterial.SetFloat("_NoiseStrength", noiseStrength);
        tvMaterial.SetFloat("_Offset", offset);
    }
}

