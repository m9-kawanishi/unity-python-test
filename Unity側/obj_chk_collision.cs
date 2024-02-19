using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class obj_chk_collision : MonoBehaviour
{
    public Transform staticObject; // 静止している物体
    public Transform movingObject; // 動かす物体
    public float moveSpeed = 5f; // 物体の移動速度

    private bool hasCollided = false; // 接触したかどうかのフラグ

    void Update()
    {
        // 動かす物体を一定の速度で移動させる
        movingObject.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // 物体が接触したらフラグをTrueに設定
        if (!hasCollided && IsColliding())
        {
            hasCollided = true;
            // UnityからPythonに通信
            //Process.Start("python", "C:\Users\kawanshi\a.py")
            SendMessageToPython(hasCollided.ToString());
        }
    }

    // 物体が接触しているかどうかを判定するメソッド
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