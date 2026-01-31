using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

/// <summary>
/// 앱의 초기 부트스트랩 클래스
/// </summary>
public class AppBootStrap : Singleton<AppBootStrap>
{
    protected override void Awake()
    {
        base.Awake();
        InitializeAsync().Forget();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }

    /// <summary>
    /// 매니저 순차적으로 초기화, 의존성 순서 고려
    /// </summary>
    public async override UniTask InitializeAsync()
    {
        await RecordingManager.Instance.InitializeAsync();
    }
}
    