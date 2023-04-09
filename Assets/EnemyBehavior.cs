using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int maxHits = 4; // maximum number of hits before the enemy is destroyed
    private int currentHits = 0; // current number of hits on the enemy

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Egg"))
        {
            currentHits++;
            if (currentHits >= maxHits)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}