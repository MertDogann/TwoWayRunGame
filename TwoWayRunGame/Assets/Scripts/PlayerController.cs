using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //un

public class PlayerController : MonoBehaviour
{
    CountdownTimer countDownTimer;
    public static PlayerController current;
    [SerializeField] float limitXRight;
    [SerializeField] float limitXLeft;
    public float xSpeed;
    public float runningSpeed;
    private float lastTouchedX;
    private float currentSpeed;
    private bool finished;
    private float scoreTimer;
    public float accumulatingSpeed;
    [SerializeField] Transform ballTransform;
    public Animator animator;
    [SerializeField] float obstacleDownloadSpeed;
    [SerializeField] private GameObject shatteredObstacle;
    Rigidbody myRb;
    Vector3 freezePosition;
    public float strikingPoints = 0;
    [SerializeField] ParticleSystem runParticle1;
    [SerializeField] ParticleSystem runParticle2;

    //Un
    
    private Touch theTouch;
    private Vector2 touchStartPosition, touchEndPosition;
    




    

    void Start()
    {
        current = this;

        //PlayerPrefs.DeleteAll();
        


    }
    void Update()
    {
        if(LevelController.Current == null || !LevelController.Current.gameActive)
        {
            runParticle1.Stop();
            return;
        }
        runParticle1.transform.position = transform.position + new Vector3(-0.13f, -2f, -1f);
        SwerveMobil();
        RotationBall();


        if (runningSpeed < 0)
        {
            LevelController.Current.GameOver();
        }
        if (transform.position.x <= -2f && transform.position.z <275f)
        {
            LeftPlatformSpeed();
            
        }if (transform.position.x > -2f && transform.position.z < 275f)
        {
            RightPlatformSpeed();
            
        }else if(transform.position.z > 275f)
        {
            runParticle1.Stop();
            runParticle2.Stop();
        }
        if (currentSpeed <= 20f)
        {
            LevelController.Current.currentspeedText.color = Color.red;
            LevelController.Current.currentSpeed.color = Color.red;
        }
        else
        {
            LevelController.Current.currentspeedText.color =new Color32(49, 60, 222, 255);
            LevelController.Current.currentSpeed.color = new Color32(49, 60, 222, 255);
        }

    }

    private void RightPlatformSpeed()
    {
        runningSpeed += accumulatingSpeed;
        accumulatingSpeed = 0;
        runningSpeed -= 8f * Time.deltaTime;
        if (runningSpeed <= 10)
        {
            runningSpeed = runningSpeed = -1f;
        }

        else if (runningSpeed<= 15 )
        {
            runningSpeed = 15;
        }

    }

    private void LeftPlatformSpeed()
    {
        runningSpeed -= 6.5f * Time.deltaTime;
        accumulatingSpeed += 16f * Time.deltaTime;
    }

    public void ChangeSpeed(float value) // Oyunun baþlamadan önce karakterinin hýzýnýn sýfýrlandýðý kýsým.
    {
        currentSpeed= value;    
    }
    public void Die()//Playerin ölüm animasyonunun kamera takipinin sonlandýðý game over canvasýnýn ortaya çýktýðý yer.
    {
        Camera.main.transform.SetParent(null);
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (LevelController.Current == null || !LevelController.Current.gameActive)
        {
            return;
        }
        runParticle1.transform.position = transform.position - new Vector3(0, 2, 1);
        if (other.tag == "Finish")
        {
            finished = true;
        }
        if (other.tag == "FinishEnd")
        {
            LevelController.Current.FinishGame();
        }
        else if (other.tag == "FinishPlatform")
        {
            runningSpeed -= 1.5f * Time.deltaTime;
            if(runningSpeed < 3)
            {
                myRb = GetComponent<Rigidbody>();
                freezePosition = new Vector3(0, 0, 0);
                myRb.constraints = RigidbodyConstraints.FreezeAll;
                LevelController.Current.FinishGame();
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            
            other.gameObject.tag = "Untagged";
            LevelController.Current.ChangeScore(10);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("LeftPlatform"))
        {
            runParticle2.Stop();
            Debug.Log("Durdu");
            runParticle1.Play();
        }
        if (other.gameObject.CompareTag("RightPlatform"))
        {
            runParticle2.Play();
            Debug.Log("Devam");
            runParticle1.Stop();
        }
        
        
        
    }


    public void RotationBall()
    {
        ballTransform.Rotate(new Vector3(0, 0, -runningSpeed));
        
    }

    public void SwerveMobil()
    {
        currentSpeed = runningSpeed;
        //Un
        float touchXDelta = 0;
        if (Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);

            if (theTouch.phase == TouchPhase.Began)
            {
                touchXDelta = Input.GetTouch(0).deltaPosition.x / Screen.width;
                touchStartPosition = theTouch.position;
            }

            else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended)
            {
                touchXDelta = Input.GetTouch(0).deltaPosition.x / Screen.width;
                touchEndPosition = theTouch.position;

                float x = touchEndPosition.x - touchStartPosition.x;
                float y = touchEndPosition.y - touchStartPosition.y;


            }
        }

        float newX = 0;

        newX = transform.position.x + xSpeed * touchXDelta * Time.deltaTime;
        newX = Mathf.Clamp(newX, -limitXLeft, limitXRight);
        Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z + currentSpeed * Time.deltaTime);

        transform.position = newPosition;

    }


}
