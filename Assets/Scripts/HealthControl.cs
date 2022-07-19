using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthControl : MonoBehaviour
{
    public GameObject heart1, heart2, heart3;
    private static int health;
    public static int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (health > 3)
            {
                health = 3;
            }
            else if (health < 0)
            {
                health = 0;
            }

        }
    }
    void Start()
    {
        health = 3;
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        switch (health)
        {
            case 3:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                break;
            case 2:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(false);
                break;
            case 1:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
            case 0:
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                // game over active;
                break;
        }
    }

    // Update not good method should move switch here
    public void CheckHealtLevel()
    {

    }
}
