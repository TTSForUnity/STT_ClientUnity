using Cysharp.Threading.Tasks;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object _lock = new object();

    public static T Instance
    {
        get
        {
            lock (_lock) // 메인 쓰레드에서만 객체 생성, 참고 타입만 열쇠로 사용 (예 : Object), 여러 쓰레드에서 중복 생성하지 않도록.
            {
                if(_instance == null)
                {
                    _instance = (T)FindFirstObjectByType(typeof(T));

                    if(_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = typeof(T).ToString() + " (Singleton)";

                        DontDestroyOnLoad(singleton);
                    }
                }
                return _instance;
            }
        }
    }

    protected async virtual void Awake()
    {
        if(_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if(_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public async virtual UniTask InitializeAsync()
    {
        
    }
}
