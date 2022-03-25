using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    float totalTime = 0;
    float passingTime = 0;

    bool working = false;
    bool started = false;


    /// <summary>
    /// Geri sayým sayacýnýn toplam süresini ayarlar
    /// </summary>
    public float TotalTime
    {
        set
        {
            if (!working)
            {
                totalTime = value;
            }
        }
    }


    /// <summary>
    /// Geri sayýmýn bitip bitmediðini söyler
    /// </summary>
    public bool End
    {
        get
        {
            return started && !working;
        }
    }

    /// <summary>
    /// Sayacý çalýþtýrýr
    /// </summary>
    public void Run()
    {
        if (totalTime > 0)
        {
            working = true;

            started = true;
            passingTime = 0;
        }
    }


    void Update()
    {
        if (working)
        {
            passingTime += Time.deltaTime;
            if (passingTime >= totalTime)
            {
                working = false;
            }
        }
    }
}
