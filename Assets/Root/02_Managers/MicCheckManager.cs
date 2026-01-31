using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;

using System;

public class MicCheckManager : Singleton<MicCheckManager>
{
    public bool IsInitialized { get; private set; } = false;

    public CancellationTokenSource Cts { get { return _cts; } }
    private CancellationTokenSource _cts = new CancellationTokenSource();

    // 유니티에 연결된 모든 마이크 리스트
    public static List<string> MicDevices = new List<string>();
    // 현재 선택된 마이크 장치
    public static string curMicDevice = string.Empty;

    public async override UniTask InitializeAsync()
    {
        if(IsInitialized) return;

        Debug.Log("RecordingManager 초기화 시작...");
        
        // 여기에 초기화 로직 추가
        await ResetCts();
        await CheckMicDevice();

        if(!string.IsNullOrEmpty(curMicDevice))
            CheckMicDeviceActivate(_cts.Token).Forget();

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

    public async UniTask CheckMicDevice()
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

    public void SelectMicDeivce(int index)
    {
        if(MicDevices.Count >= 1)
        {
            curMicDevice = MicDevices[index];
            Debug.Log($"마이크 {curMicDevice}가 선택 되었습니다.");
        }
    }

    /// <summary>
    /// 마이크가 연결되어 있는지 계속 확인합니다.
    /// </summary>
    public async UniTask CheckMicDeviceActivate(CancellationToken ct)
    {
        if(ct.IsCancellationRequested) return;

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
