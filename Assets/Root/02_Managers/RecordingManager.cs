using System;
using System.Collections.Generic;
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

    // 유니티에 연결된 모든 마이크 리스트
    public static List<string> MicDevices = new List<string>();

    // 현재 선택된 마이크 장치
    public static string curMicDevice = string.Empty;

    protected override void Awake()
    {
        base.Awake();
    }

    public async override UniTask InitializeAsync()
    {
        if(IsInitialized) return;

        Debug.Log("RecordingManager 초기화 시작...");
        
        // 여기에 초기화 로직 추가
        await UniTask.Delay(TimeSpan.FromSeconds(1)); // 초기화 작업 시뮬레이션
        IsInitialized = true;

        Debug.Log("RecordingManager 초기화 완료.");
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

    /// <summary>
    /// 현재 존재하는 마이크 장치를 확인합니다.
    /// </summary>
    public static void CheckMicDevice()
    {
        if(Microphone.devices.Length <= 0)
        {
            Debug.LogWarning("마이크 장치가 감지되지 않았습니다.");
        }

        foreach(var device in Microphone.devices)
        {
            if(!MicDevices.Contains(device))
            {
                MicDevices.Add(device);
                Debug.Log($"감지된 마이크 장치: {device}");
            }
        }
    }

    /// <summary>
    /// 마이크가 연결되어 있는지 계속 확인합니다.
    /// </summary>
    private async UniTask CheckMicDeviceActivate()
    {
        int curPosition = 0;
        int _lastSamplePostion = 0;

        float _lastCheckTime = 0f;

        while(true)
        {
            curPosition = Microphone.GetPosition(curMicDevice);
            if(_lastSamplePostion != curPosition)
            {
                _lastSamplePostion = curPosition;
                _lastCheckTime = Time.time;
            }
            else
            {
                if(Time.time - _lastCheckTime > 0.5f)
                {
                    Debug.LogError("마이크 데이터가 멈췄습니다. 장치 연결을 확인하세요.");
                }
            }
            await UniTask.Delay(TimeSpan.FromSeconds(1)); // 1초마다 검사
        }
    }
}
