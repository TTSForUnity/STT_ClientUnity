using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

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

    public bool IsInitialized { get; private set; } = false;

    public CancellationTokenSource SoundCts { get { return _cts; } }
    private CancellationTokenSource _cts = new CancellationTokenSource();

    protected override void Awake()
    {
        base.Awake();
    }

    public async override UniTask InitializeAsync()
    {
        if(IsInitialized) return;

        Debug.Log("RecordingManager 초기화 시작...");
        
        // 여기에 초기화 로직 추가
        await ResetCts();

        IsInitialized = true;
        Debug.Log("RecordingManager 초기화 완료.");
    }

    private async UniTask ResetCts()
    {
        _cts?.Cancel();
        _cts.Dispose();
        _cts = new CancellationTokenSource();

        await UniTask.Yield();
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
}
