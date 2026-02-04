using UnityEngine;

[CreateAssetMenu(fileName = "ApiConfig", menuName = "Scriptable Objects/ApiConfig")]
public class ApiConfig : ScriptableObject
{
    public string BaseURL;
    public string Port;
}

/// <summary>
/// API들을 정의한다.
/// </summary>
public enum ApiType
{
    STT
}

public enum ConnectionType
{
    Http,
    Ws
}
