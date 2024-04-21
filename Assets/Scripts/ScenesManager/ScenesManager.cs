using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : Singleton<ScenesManager>
{
    
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;
    private bool isFade;

    public void Transition(string from, string to)
    {
        if (!isFade)
        {
            StartCoroutine(TransitionToScene(from, to));
        }
    }

    private IEnumerator TransitionToScene(string from, string to)
    {
        yield return Fade(1);//等待执行完后才执行别的，如果同时可以使用StartCoroutine(Fade(1));
        yield return SceneManager.UnloadSceneAsync(from);
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        

        //设置新场景为激活场景
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1); //找到最后加载的场景
        SceneManager.SetActiveScene(newScene);

        yield return Fade(0);
    }

    /// <summary>
    /// 淡入淡出场景
    /// </summary>
    /// <param name="targerAlpha">1是黑，0是透明</param>
    /// <returns></returns>
    private IEnumerator Fade(float targerAlpha)
    {
        isFade = true;

        fadeCanvasGroup.blocksRaycasts = true;

        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targerAlpha) / fadeDuration;

        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targerAlpha))
        {
            // 也可以用DoTween方法
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targerAlpha, speed * Time.deltaTime);
            yield return null;
        }

        fadeCanvasGroup.blocksRaycasts = false;

        isFade = false;
    }
}
