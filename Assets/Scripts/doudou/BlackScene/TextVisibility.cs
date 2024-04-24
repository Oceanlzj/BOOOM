using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextVisibility : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private float alphaValue = 1f;
    private bool isFadingOut = true;
    private float fadeSpeed = 0.8f; // 调整透明度变化速度

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        float alphaChange = fadeSpeed * Time.deltaTime;
        if (isFadingOut)
        {
            alphaValue -= alphaChange;
            if (alphaValue <= 0)
            {
                isFadingOut = false;
            }
        }
        else
        {
            alphaValue += alphaChange;
            if (alphaValue >= 1)
            {
                isFadingOut = true;
            }
        }

        textMeshPro.alpha = alphaValue;
    }
}
