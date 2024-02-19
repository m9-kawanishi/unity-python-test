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
        // TCP���X�i�[�̃X���b�h���Z�b�g�A�b�v
        tcpListenerThread = new Thread(new ThreadStart(ListenForIncomingRequests));
        tcpListenerThread.IsBackground = true;
        tcpListenerThread.Start();
    }

    private void ListenForIncomingRequests()
    {
        try
        {
            // ���[�J���z�X�g�̓���̃|�[�g�Ń��X�i�[���쐬
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
                        // ��M�����f�[�^��ǂݎ��
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incomingData = new byte[length];
                            Array.Copy(bytes, 0, incomingData, 0, length);
                            // ��M�����f�[�^�𕶎���ɕϊ�
                            string clientMessage = Encoding.ASCII.GetString(incomingData);
                            UnityEngine.Debug.Log("client message received as: " + clientMessage);
                            // ������Unity�̃V�X�e�����N��
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
        // ������Unity�̃V�X�e�����X�V����R�[�h������
    }
}
