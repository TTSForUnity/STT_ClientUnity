using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public enum CallbackType
{
    StartRecording,
    StopRecording,
    Count
}

public class RecordingManager : Singleton<RecordingManager>
{
    public Dictionary<CallbackType, List<Action>> Callbacks = new Dictionary<CallbackType, List<Action>>()
    {
        { CallbackType.StartRecording, new List<Action>() },
        { CallbackType.StopRecording, new List<Action>() }
    };

    protected override void Awake()
    {
        base.Awake();
    }

    public async UniTask InitializeAsync()
    {
        
    }

    /// <summary>
    /// 콜백을 등록합니다.
    /// </summary>
    public void RegisterCallback(CallbackType type, Action Callback)
    {
        if(!Callbacks[type].Contains(Callback))
        {
            Callbacks[type].Add(Callback);
        }
    }

    /// <summary>
    /// 등록된 콜백을 실행합니다.
    /// </summary>
    public void TriggerCallbacks(CallbackType type)
    {
        foreach(var callback in Callbacks[type])
        {
            callback?.Invoke();
        }
    }

    public static void CheckMicDevice()
    {
        
    }
}
