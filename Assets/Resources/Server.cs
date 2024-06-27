using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class Server : MonoBehaviour
{
    private TcpListener server;
    private Thread serverThread;

    public UnityEvent i, c, e, thankyou;

    void Start()
    {
        serverThread = new Thread(StartServer);
        serverThread.Start();
    }



    void StartServer()
    {
        server = new TcpListener(IPAddress.Any, 8080);
        server.Start();
        Debug.Log("Server started on port 8080");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string command = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            Debug.Log("Received command: " + command);

            ExecuteCommand(command);

            client.Close();
        }
    }

    void ExecuteCommand(string command)
    {
        // Define your actions based on the command
        if (command == "I")
        {
            i.Invoke();
        }
        else if (command == "E")
        {
            e.Invoke();
        }
        else if (command == "C")
        {
            c.Invoke();
        }
        else if (command == "thankyou")
        {
            //thankyou.Invoke();
            Debug.Log("Thank you");
        }
        // Add more commands as needed
    }

    void OnApplicationQuit()
    {
        server.Stop();
        serverThread.Abort();
    }
}
