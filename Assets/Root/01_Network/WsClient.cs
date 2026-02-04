using UnityEngine;
using NativeWebSocket;
using Cysharp.Threading.Tasks;

public class WsClient : MonoBehaviour
{
    private WebSocket webSocket;

    public async UniTask WebSocketConnected()
    {
        webSocket = new WebSocket(ApiUrlBuilder.GetUrl(ConnectionType.Ws, ApiType.STT));

        webSocket.OnOpen += () =>
        {
            // Debug.Log("웹소켓 ")
        };
    }
}
