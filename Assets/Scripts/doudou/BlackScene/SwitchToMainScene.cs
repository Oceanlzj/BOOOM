using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchToMainScene : MonoBehaviour
{
    public GameObject Text;
    private SpriteRenderer spriteRenderer;
    private float alphaValue = 0f;
    private bool isFadingIn = true;
    private bool isFadingOut = false;
    private float fadeSpeed = 0.5f; // ���������ٶ�
    void Start()
    {
        Text.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }



    
    // Update is called once per frame
    void Update()
    {
        float alphaChange = fadeSpeed * Time.deltaTime;
        if (isFadingIn)
        {
            alphaValue += alphaChange;
            if (alphaValue >=1)
            {
                alphaValue = 1;
                isFadingIn = false;
                Text.SetActive(true);
            }
        }
        

        if (Input.anyKeyDown)
        {
            
            Text.SetActive(false);
            isFadingOut = true;
        }
        if(isFadingOut==true)
        {
            alphaValue -= alphaChange;
            if (alphaValue <= 0)
            {
                alphaValue = 0;
                SceneManager.LoadScene(1);
            }
        }
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alphaValue);
    }
    private IEnumerator TextVisible()
    {
        yield return new WaitForSeconds(2f);
        Text.SetActive(true);
    }
}