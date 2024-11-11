using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class sphereverte : MonoBehaviour
{


    [SerializeField] private TMP_Text victoire;
    public static float johnsonrestant;


    void Start()
    {
        johnsonrestant = 50;
    }

    
    void Update()
    {
        if (johnsonrestant == 0)
        {
            victoire.text = "VOUS AVEZ GAGNÉ";
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("johnson"))
        {
            johnsonrestant -= 1;
            Destroy(gameObject);


        }
        else
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                Destroy(gameObject);
            }
        }

    }


}
