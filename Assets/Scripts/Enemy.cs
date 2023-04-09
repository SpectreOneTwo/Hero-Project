using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHits = 4; // maximum number of hits before the enemy is destroyed
    private int currentHits = 0; // current number of hits on the enemy
    private SpriteRenderer spriteRenderer; // reference to the sprite renderer component

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // get the sprite renderer component of the enemy object
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Hero hero = FindObjectOfType<Hero>();
        if (other.gameObject.CompareTag("Egg"))
        {
            currentHits++;

            // Reduce alpha of the enemy sprite by 20% after each hit by egg
            Color spriteColor = spriteRenderer.color;
            spriteColor.a *= 0.8f;
            spriteRenderer.color = spriteColor;

            if (currentHits == maxHits)
            {
                Destroy(gameObject);
                hero.DestroyEnemy();
            }
        }
    }
}