using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class obj_chk_collision : MonoBehaviour
{
    public Transform staticObject; // �Î~���Ă��镨��
    public Transform movingObject; // ����������
    public float moveSpeed = 5f; // ���̂̈ړ����x

    private bool hasCollided = false; // �ڐG�������ǂ����̃t���O

    void Update()
    {
        // ���������̂����̑��x�ňړ�������
        movingObject.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // ���̂��ڐG������t���O��True�ɐݒ�
        if (!hasCollided && IsColliding())
        {
            hasCollided = true;
            // Unity����Python�ɒʐM
            //Process.Start("python", "C:\Users\kawanshi\a.py")
            SendMessageToPython(hasCollided.ToString());
        }
    }

    // ���̂��ڐG���Ă��邩�ǂ����𔻒肷�郁�\�b�h
    bool IsColliding()
    {
        return staticObject.GetComponent<Collider>().bounds.Intersects(movingObject.GetComponent<Collider>().bounds);
    }

    void SendMessageToPython(string message)
    {
        try
        {
            TcpClient client = new TcpClient("localhost", 5005);
            StreamWriter sw = new StreamWriter(client.GetStream());
            sw.WriteLine(message);
            sw.Flush();
            sw.Close();
            client.Close();
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError($"Failed to send message: {e.Message}");
        }
    }
}