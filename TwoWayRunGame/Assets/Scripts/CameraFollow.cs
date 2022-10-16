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
            MoveTheCamera();

        }

    }
    private void MoveTheCamera()
    {
        

        PlayerController currentSpeed = FindObjectOfType<PlayerController>();
        Vector3 camera = new Vector3(0, 0, currentSpeed.runningSpeed * cameraDistance);
        Vector3 targetToMove = (playerTransform.position + offsetVector) + camera;
        transform.position = Vector3.Lerp(transform.position, targetToMove, cameraFollowTime * Time.deltaTime);
        transform.LookAt(playerTransform.position);

        
    }
    private Vector3 CalculateVector(Transform target)
    {
        return transform.position - target.position;
    }
}
