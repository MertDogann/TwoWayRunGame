using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScore : MonoBehaviour
{
    public float scoreX;
    [SerializeField] GameObject canvas;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trigger")
        {
            LevelController.Current.ChangeMultiplicationScore(scoreX);
            Debug.Log("Finis1");


        }
        if (other.gameObject.tag == "Trigger")
        {
            canvas.SetActive(false);
        }
    }
}
