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
        offset = Random.Range(0f, 1f); // �������һ����ʼƫ��ֵ
    }

    void Update()
    {
        offset += Time.deltaTime * noiseSpeed; // �����ٶȸ���ƫ��ֵ
        tvMaterial.SetFloat("_NoiseStrength", noiseStrength);
        tvMaterial.SetFloat("_Offset", offset);
    }
}

