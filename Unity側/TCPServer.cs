using System;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class TCPServer : MonoBehaviour
{
    private TcpListener tcpListener;
    private Thread tcpListenerThread;
    private TcpClient connectedTcpClient;

    // Start is called before the first frame update
    void Start()
    {
        // TCPリスナーのスレッドをセットアップ
        tcpListenerThread = new Thread(new ThreadStart(ListenForIncomingRequests));
        tcpListenerThread.IsBackground = true;
        tcpListenerThread.Start();
    }

    private void ListenForIncomingRequests()
    {
        try
        {
            // ローカルホストの特定のポートでリスナーを作成
            tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8052);
            tcpListener.Start();
            UnityEngine.Debug.Log("Server is listening");

            Byte[] bytes = new Byte[1024];
            while (true)
            {
                using (connectedTcpClient = tcpListener.AcceptTcpClient())
                {
                    using (NetworkStream stream = connectedTcpClient.GetStream())
                    {
                        int length;
                        // 受信したデータを読み取る
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incomingData = new byte[length];
                            Array.Copy(bytes, 0, incomingData, 0, length);
                            // 受信したデータを文字列に変換
                            string clientMessage = Encoding.ASCII.GetString(incomingData);
                            UnityEngine.Debug.Log("client message received as: " + clientMessage);
                            // ここでUnityのシステムを起動
                        }
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            UnityEngine.Debug.Log("SocketException " + socketException.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ここでUnityのシステムを更新するコードを書く
    }
}
