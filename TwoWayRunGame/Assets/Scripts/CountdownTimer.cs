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
    /// Geri say�m sayac�n�n toplam s�resini ayarlar
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
    /// Geri say�m�n bitip bitmedi�ini s�yler
    /// </summary>
    public bool End
    {
        get
        {
            return started && !working;
        }
    }

    /// <summary>
    /// Sayac� �al��t�r�r
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
