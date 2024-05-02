using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
  public float fadeSpeed = 1.5f;
  public Image backImage;

  // Start is called before the first frame update
  void Start()
  {

  }
  // 渐现
  private void FadeToClear()
  {
    backImage.color = Color.Lerp(backImage.color, Color.clear, fadeSpeed * Time.deltaTime);
  }
  // 渐隐
  private void FadeToBlack()
  {
    backImage.color = Color.Lerp(backImage.color, Color.black, fadeSpeed * Time.deltaTime);
  }
  // 初始化时调用
  public void StartScene()
  {
    backImage.enabled = true;
    FadeToClear();
    if (backImage.color.a <= 0.05f)
    {
      backImage.color = Color.clear;
      backImage.enabled = false;
      //sceneStarting = false;
    }
  }
  // 结束时调用
  public void EndScene()
  {
    backImage.enabled = true;
    FadeToBlack();
    if (backImage.color.a >= 0.95f)
    {

    }
  }

  // Update is called once per frame
  void Update()
  {
  }
}
