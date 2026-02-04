using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;

public static class ApiUrlBuilder
{
    [SerializeField] private readonly static string BaseURL;
    [SerializeField] private readonly static string Port;

    /// <summary>
    /// Api타입으로 엔드포인트를 얻습니다.
    /// </summary>
    public static string GetUrl(ConnectionType connectionType, ApiType apiType)
    {
        string type = string.Empty;

        switch(connectionType)
        {
            case ConnectionType.Http:
                type = "http";
                break;
            case ConnectionType.Ws:
                type = "ws";
                break;
        }
        switch(apiType)
        {
            case ApiType.STT:
                return $"{type}://{BaseURL}:{Port}/ws/stt";
            default:
                return null;
        }
    }
}
