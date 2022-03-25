using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] GameObject shatteredObstaclee;
    [SerializeField] GameObject unbreakableObstacle;
    [SerializeField] float downSpeed;
    [SerializeField] GameObject text;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (LevelController.Current == null || !LevelController.Current.gameActive)
        {
            return;
        }
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trigger")
        {
            Debug.Log("Player geldi");
            PlayerController.current.runningSpeed -= downSpeed;
            Destroy(unbreakableObstacle.gameObject);
            shatteredObstaclee.SetActive(true);
            Destroy(text.gameObject);
            StartCoroutine(ObstacleDestroy(shatteredObstaclee));

        }
    }
    

    IEnumerator ObstacleDestroy(GameObject shattered)
    {
        yield return new WaitForSeconds(1.8f);
        Destroy(shattered);
    }



}
