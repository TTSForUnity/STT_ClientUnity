using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct UIPanel
{
    public BaseUI _instance;
    public GameObject _prefab;
}

[CreateAssetMenu(fileName = "UISO", menuName = "Scriptable Objects/UISO")]
public class UISO : ScriptableObject
{
    public List<UIPanel> allUiList;
}
