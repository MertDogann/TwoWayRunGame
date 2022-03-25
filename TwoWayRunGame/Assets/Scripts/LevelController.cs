using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController Current;
    public bool gameActive = false;

    public GameObject startMenu, gameMenu, gameOverMenu, finishMenu;
    public Text scoreText, finishScoreText, currentLevelText, nextLevelText ,currentSpeed , currentspeedText;
    public Slider levelProgressBar;
    public Slider energyBar;
    public float maxDistance;
    public float maxDistanceEnergy;
    public GameObject finishLine;
    [SerializeField] GameObject animationRotation;
    [SerializeField] GameObject players;
    [SerializeField] GameObject ball;
    [SerializeField] GameObject shattaredBall;
    
    public int currentLevel =0;
    int currentLevell=0 ;
    float score;

    
    void Start()
    {
        Current = this;
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        currentLevell = PlayerPrefs.GetInt("currentLevell");

        if (SceneManager.GetActiveScene().name != "Level " + currentLevel)
        {
           

            SceneManager.LoadScene("Level " + currentLevel);
            
        }
        else
        {
            currentLevelText.text = (currentLevell + 1).ToString();
            nextLevelText.text = (currentLevell + 2).ToString();
        }
        
    }

    void Update()
    {
        
        if (gameActive)
        {
            PlayerController player = PlayerController.current;
            float distance = finishLine.transform.position.z - PlayerController.current.transform.position.z;
            levelProgressBar.value = 1 - (distance / maxDistance);
            int runningSpeed = (int)PlayerController.current.runningSpeed;
            currentSpeed.text = runningSpeed.ToString();
            float distanceEnergyBar = PlayerController.current.accumulatingSpeed - 40;
            energyBar.value = 1 - (distanceEnergyBar / maxDistanceEnergy);
            
        }
        
    }
    public void StartLevel()
    {
        maxDistanceEnergy = PlayerController.current.accumulatingSpeed - 40;
        maxDistance = finishLine.transform.position.z - PlayerController.current.transform.position.z;
        PlayerController.current.ChangeSpeed(PlayerController.current.runningSpeed);
        startMenu.SetActive(false);
        gameMenu.SetActive(true);
        PlayerController.current.animator.SetBool("running", true);
        LeftPlatformContoller leftPlatform = FindObjectOfType<LeftPlatformContoller>();
        leftPlatform.animator.SetBool("GameStarted" , true);
        RightPlatformContoller rightPlatform = FindObjectOfType<RightPlatformContoller>();
        rightPlatform.animator.SetBool("GameStarted", true);
        Debug.Log("Start Baþladý");
        gameActive = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        

    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Level " + (currentLevel +1));
        
        if (currentLevel == 2)
        {

            PlayerPrefs.SetInt("currentLevel", currentLevel - 2);
            SceneManager.LoadScene("Level 0");
        }
    }
    public void GameOver()
    {
        gameMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        PlayerController.current.animator.SetBool("gameover", true);
        PlayerController.current.animator.SetBool("running", false);
        ball.SetActive(false);
        shattaredBall.SetActive(true);
        Destroy(shattaredBall, 2f);
        gameActive = false;
    }
    public void FinishGame()
    {
        PlayerPrefs.SetInt("currentLevel", currentLevel + 1);
        PlayerPrefs.SetInt("currentLevell", currentLevell + 1);
        finishScoreText.text = score.ToString();
        gameMenu.SetActive(false);
        finishMenu.SetActive(true);
        PlayerController.current.animator.SetBool("win", true);
        PlayerController.current.animator.SetBool("running", false);
        LeftPlatformContoller leftPlatform = FindObjectOfType<LeftPlatformContoller>();
        leftPlatform.animator.SetBool("GameStarted", false);
        RightPlatformContoller rightPlatform = FindObjectOfType<RightPlatformContoller>();
        rightPlatform.animator.SetBool("GameStarted", false);
        ball.SetActive(false);
        shattaredBall.SetActive(true);
        gameActive = false;
        players.transform.Rotate(0, -180, 0);
        

    }
    public void ChangeScore(int incremenet)
    {
        score += incremenet;
        scoreText.text = score.ToString();
    }
    public void ChangeMultiplicationScore(float increment)
    {
        
        score *= increment;
        score = (int)score;
        scoreText.text = score.ToString();
        
        
    }
}
