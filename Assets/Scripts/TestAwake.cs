using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestAwake : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("Awake called!"); // Sẽ được gọi ngay khi GameObject khởi tạo.
    }

    void Start()
    {
        Debug.Log("Start called!"); // Chỉ gọi khi GameObject được kích hoạt.
    }
}

