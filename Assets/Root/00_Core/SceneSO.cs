using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SceneCallback
{
    public string sceneName;
    [HideInInspector] public Action callback;
}

[CreateAssetMenu(fileName = "SceneSO", menuName = "Scriptable Objects/SceneSO")]
public class SceneSO : ScriptableObject
{
    public List<SceneCallback> sceneNameList;
}