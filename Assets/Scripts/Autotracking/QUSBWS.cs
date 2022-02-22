using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using NativeWebSocket;

public class QUSBWS : MonoBehaviour
{
    public static WebSocket ws;
    public static int opcode;
    public static bool messageReceived;
    public static byte[] data;  // Data from RAM is accessed here. Because of the weirdness of this websocket implementation,
                                // I can't simply create a function that returns the bytes.
    async void OnEnable()
    {
        ws = new WebSocket("ws://localhost:8080");

        ws.OnOpen += () =>
        {
        Debug.Log("Connection open!");
        DeviceList();
        Autotracker.autotrack = true;
        };

        ws.OnError += (e) =>
        {
        Debug.Log("Error! " + e);
        Autotracker.autotrack = false;
        this.gameObject.SetActive(false);
        };

        ws.OnClose += (e) =>
        {
        Debug.Log("Connection closed!");
        Autotracker.autotrack = false;
        this.gameObject.SetActive(false);
        };

        ws.OnMessage += (bytes) =>
        {
            messageReceived = true;
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            //Debug.Log("Received Message! (" + bytes.Length + " bytes) " + message);
            data = MessageHandler(bytes);
        };

        await ws.Connect();
    }

    void Update()
    {
        #if !UNITY_WEBGL || UNITY_EDITOR
        ws.DispatchMessageQueue();
        #endif
    }

    byte[] MessageHandler(byte[] bytes) {
        // Opcode 0 just returns bytes. Which is what should be happening most of the time.
        if (opcode == 1) { // Attach device.
            string message = System.Text.Encoding.UTF8.GetString(bytes);
            message = GetBetween(message, "[", "]");
            message = GetBetween(message, "\"");
            AttachDevice(message);
        }
        return bytes;
    }

    string GetBetween(string str, string from, string to = "") { // Util
        if (to.Length == 0) {to = from;}
        return str.Substring(str.IndexOf(from)+from.Length, (str.LastIndexOf(to) - (str.IndexOf(from)+from.Length)));
    }

    public static void DeviceList() // From what I can tell, Unity and JSON just don't get along very well. So we do string operations.
    {
        if (ws.State != WebSocketState.Open) {return;}
        opcode = 1;
        ws.SendText("{\"Opcode\":\"DeviceList\",\"Space\":\"SNES\"}");
    }

    public static void GetAddress(string offset, string size) { // Try Hex
        opcode = 0;
        ws.SendText("{\"Opcode\":\"GetAddress\",\"Space\":\"SNES\",\"Operands\":[\"" + offset + "\",\"" + size + "\"]}");
    }

    void AttachDevice(string deviceName) {
        if (ws.State != WebSocketState.Open) {return;}
        ws.SendText("{\"Opcode\":\"Attach\",\"Space\":\"SNES\",\"Operands\":[\"" + deviceName + "\"]}");
        Debug.Log("Device Attached!");
    }

    private async void OnApplicationQuit()
    {
        await ws.Close();
    }
}
