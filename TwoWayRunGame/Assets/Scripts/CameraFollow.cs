using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float cameraFollowTime;
    Vector3 offsetVector;
    [SerializeField] float cameraDistance;
    void Start()
    {
        DOTween.Init();
        offsetVector = CalculateVector(playerTransform);
    }

    void FixedUpdate()
    {
        if (playerTransform != null)
        {
            //InvokeRepeating("MoveTheCamera", 1.0f, 2.0f);
            MoveTheCamera();
            //transform.position = playerTransform.position + offsetVector;
            //transform.LookAt(playerTransform.position);
        }

    }
    private void MoveTheCamera()
    {
        

        PlayerController currentSpeed = FindObjectOfType<PlayerController>();
        Vector3 camera = new Vector3(0, 0, currentSpeed.runningSpeed * cameraDistance);

        //Tween myTween = transform.DOMove(new Vector3(transform.position.x, transform.position.y,camera.z ) + (playerTransform.position + offsetVector), 2);
        Vector3 targetToMove = (playerTransform.position + offsetVector) + camera;
        transform.position = Vector3.Lerp(transform.position, targetToMove, cameraFollowTime * Time.deltaTime);
        transform.LookAt(playerTransform.position);

        
    }
    private Vector3 CalculateVector(Transform target)
    {
        return transform.position - target.position;
    }
}
