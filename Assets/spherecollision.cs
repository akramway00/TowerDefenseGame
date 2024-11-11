using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spherecollision : MonoBehaviour
{
    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            Debug.Log("Tower collision");
            scoreManager.DecreaseNombre(1f);
            Destroy(gameObject);
        }
        
    }
}