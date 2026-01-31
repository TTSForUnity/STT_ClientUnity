using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 앱의 초기 부트스트랩 클래스
/// </summary>
public class AppBootStrap : Singleton<AppBootStrap>
{
    [SerializeField] private SceneSO sceneSO;

    protected override void Awake()
    {
        base.Awake();
        InitializeAsync().Forget();
    }

    public void RegisterCallback()
    {
        
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var scenecallback = sceneSO.sceneNameList.Find(x => x.sceneName.Equals(scene.name));
        scenecallback.callback.Invoke();
    }

    /// <summary>
    /// 매니저 순차적으로 초기화, 의존성 순서 고려
    /// </summary>
    public async override UniTask InitializeAsync()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        await RecordingManager.Instance.InitializeAsync();
    }
}
    