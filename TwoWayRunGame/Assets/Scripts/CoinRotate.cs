using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    private float rotateSpeed = 3;
    [SerializeField] float Speed ;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (LevelController.Current == null || !LevelController.Current.gameActive)
        {
            return;
        }
        transform.Translate(Vector3.back * Time.deltaTime *Speed) ;
        //transform.Rotate(transform.rotation.x, rotateSpeed, transform.rotation.z) ;
    }
    
}
