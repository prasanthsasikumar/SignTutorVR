using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class Client : MonoBehaviour
{
    private TcpClient client;
    public string serverAddress = "127.0.0.1";

    void Start()
    {
        //Connect();
    }

    public void Connect()
    {
        client = new TcpClient(serverAddress, 8080);
        Debug.Log("Connected to server");
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    SendCommand("MOVE");
        //}
        //else if (Input.GetKeyDown(KeyCode.J))
        //{
        //    SendCommand("JUMP");
        //}
    }

    public void SendCommand(string command)
    {
        NetworkStream stream = client.GetStream();

        byte[] data = Encoding.ASCII.GetBytes(command);
        stream.Write(data, 0, data.Length);

        Debug.Log("Sent command: " + command);
    }

    void OnApplicationQuit()
    {
        client.Close();
    }
}
