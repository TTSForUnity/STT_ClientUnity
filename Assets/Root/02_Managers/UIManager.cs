using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public List<UIPanel> allActivePanel = new List<UIPanel>();
    public List<UIPanel> curActivePanel = new List<UIPanel>();

    public T SpawnAnyUI<T>() where T : BaseUI
    {
        var panel = allActivePanel.Find(x => x._instance is T);

        if(panel._instance != null)
        {
            Debug.LogError("해당 패널이 존재하지 않습니다.");
            return null;
        }
        else
        {
            GameObject panelObject = Instantiate(panel._prefab, null);
            return panelObject?.GetComponent<T>();
        }
    }
}
