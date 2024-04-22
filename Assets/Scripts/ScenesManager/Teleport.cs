using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SceneName] public string sceneFrom;
    [SceneName] public string sceneToGO;

    public void TeleportToScene()
    {
        ScenesManager.Instance.Transition(sceneFrom, sceneToGO);
    }
}