using System;
using WebSocketSharp;
using UnityEngine;
using UnityEngine.UI;

public class ElectricityServiceClient : MonoBehaviour
{
  public Text kakka;
  public string wsData;

  private void Start()
  {
    Debug.Log("Creating websocket");
    int clientId = 999;
    string serverAddress = "ws://192.168.43.139:8080/websocket";
    var ws = new WebSocket(serverAddress);
    ws.OnOpen += (sender, e) =>
    {
        Debug.Log("Spring says: open");
        StompMessageSerializer serializer = new StompMessageSerializer();

        Debug.Log("Spring says: sent");
        var connect = new StompMessage("CONNECT");
        connect["accept-version"] = "1.1";
        connect["heart-beat"] = "10000,10000";
        ws.Send(serializer.Serialize(connect));
        
        
        var sub = new StompMessage("SUBSCRIBE");
        sub["id"] = "sub-" + clientId;
        sub["destination"] = "/topic/readings";
        ws.Send(serializer.Serialize(sub));
        
    };
                
    ws.OnError += (sender, e) =>
    Debug.Log("Error: " + e.Message);
    ws.OnMessage += (sender, e) =>
    Debug.Log("Spring says: " + e.Data);

    ws.Connect();
  }

  private void Update()
  {
    kakka.text = wsData;
  }
}