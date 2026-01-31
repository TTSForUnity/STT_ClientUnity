using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public List<UIPanel> allActivePanel = new List<UIPanel>();
    public List<UIPanel> curActivePanel = new List<UIPanel>();

    /// <summary>
    /// UI 인스턴스 및 GameObject를 생성합니다.
    /// </summary>
    public T SpawnAnyUI<T>() where T : BaseUI
    {
        var panel = allActivePanel.Find(x => x._instance is T);

        if(panel._instance == null)
        {
            Debug.LogError("해당 패널이 존재하지 않습니다.");
            return null;
        }
        else
        {
            curActivePanel.Add(panel);
            GameObject panelObject = Instantiate(panel._prefab, null);
            return panelObject.GetComponent<T>();
        }
    }
    
    /// <summary>
    /// 현재 활성화 되어 있는 UI중, 원하는 UI요소를 삭제할 수 있습니다.
    /// </summary>
    public T DestroyAnyUI<T>(GameObject curUI) where T : BaseUI
    {
        var panel = curActivePanel.FindLast(x => x._instance is T);
        curActivePanel.Remove(panel);

        Destroy(curUI);

        return panel._instance as T;
    }

    /// <summary>
    /// 현재 활성화 되어 있는 UI중에서 원하는 인스턴스를 얻을 수 있습니다.
    /// </summary>
    public T GetAnyUiInstance<T>() where T : BaseUI
    {
        var panel = curActivePanel.FindLast(x => x._instance is T);
        return panel._instance as T;
    }
}
