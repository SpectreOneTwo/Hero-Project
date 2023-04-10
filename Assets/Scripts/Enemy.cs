using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHits = 4; // maximum number of hits before the enemy is destroyed
    private int currentHits = 0; // current number of hits on the enemy
    private SpriteRenderer spriteRenderer; // reference to the sprite renderer component

    public float speed = 10.0f;
    public bool moveAroundBoundary = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // get the sprite renderer component of the enemy object
    }


    void Update()
    {
        HandleInput();
        Move();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            moveAroundBoundary = !moveAroundBoundary;
        }
    }

    void Move()
    {
        if (moveAroundBoundary == true)
        {
            // move the enemy up until it reaches the world bounds
            transform.Translate(Vector3.up * speed * Time.deltaTime);

            // check if the enemy is outside of the world bounds
            if (!IsInsideWorldBounds(transform.position))
            {
                // find a new direction within the world bounds
                Vector3 newDirection = GetRandomDirectionWithinWorldBounds();
                transform.up = newDirection;
            }
        }
    }

    bool IsInsideWorldBounds(Vector3 position)
    {
        return position.x >= -90f && position.x <= 90f && position.y >= -90f && position.y <= 90f;
    }

    Vector3 GetRandomDirectionWithinWorldBounds()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        Vector3 direction = new Vector3(x, y, 0).normalized;
        return direction;
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