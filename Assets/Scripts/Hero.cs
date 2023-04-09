using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public float initialSpeed = 20.0f;
    public float maxSpeed = 50.0f;
    public float minSpeed = 0f;
    private float mouseSpeed = 1000f;
    public float speedIncreaseAmount = 0.1f;
    public float speedDecreaseAmount = 0.1f;
    public float rotationRate = 45.0f;
    public GameObject eggPrefab;
    public float eggSpawnRate = 0.2f;
    public int maxNumberOfEggsInWorld = 10;
    
    private bool isUsingMouseControl = true;
    private float currentSpeed;
    private float timeSinceLastEggSpawned = 0.0f;
    public int numberOfEggsInWorld = 0;

    public GameObject enemyPrefab;  // the prefab for the Enemy game object
    public float enemyspawnDelay = 0.1f;  // delay between spawns
    public int maxEnemies = 10;  // maximum number of enemies allowed on screen at once
    public int numberOfEnemiesInWorld = 0;
    public int numberOfEnemiesTouched = 0;
    public int numberOfEnemiesDestroyed = 0;

    private float timeSinceLastEnemySpawned = 0.0f;  // time since the last enemy was spawned


    private void Start()
    {
        currentSpeed = initialSpeed;
    }

    private void Update()
    {
        HandleInput();
        Move();
        CheckBounds();
        SpawnEgg();
        SpawnEnemy();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isUsingMouseControl = !isUsingMouseControl;
        }

        if (!isUsingMouseControl)
        {
            if (Input.GetKey(KeyCode.W) && currentSpeed < maxSpeed)
            {
                currentSpeed += speedIncreaseAmount;
            }
            else if (Input.GetKey(KeyCode.S) && currentSpeed > minSpeed)
            {
                currentSpeed -= speedDecreaseAmount;
            }

            float rotation = Input.GetAxis("Horizontal") * rotationRate * Time.deltaTime;
            transform.Rotate(0, 0, -rotation);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
    }

    private void Move()
    {
        if (isUsingMouseControl)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = Vector3.MoveTowards(transform.position, mousePosition, mouseSpeed * Time.deltaTime);

            // Make the hero always face in the positive y direction
            Vector3 direction = (mousePosition - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
        else
        {
            transform.Translate(Vector3.up * currentSpeed * Time.deltaTime);
        }
    }

    private void CheckBounds()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, -90f, 90f);
        currentPosition.y = Mathf.Clamp(currentPosition.y, -90f, 90f);
        transform.position = currentPosition;
    }

    private void SpawnEgg()
    {
        timeSinceLastEggSpawned += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && timeSinceLastEggSpawned >= eggSpawnRate && numberOfEggsInWorld < maxNumberOfEggsInWorld)
        {
            GameObject egg = Instantiate(eggPrefab, transform.position, Quaternion.identity);
            egg.transform.up = transform.up;
            Rigidbody2D eggRigidbody = egg.GetComponent<Rigidbody2D>();
            eggRigidbody.velocity =  transform.up * (currentSpeed + 40f);
            numberOfEggsInWorld++;
            timeSinceLastEggSpawned = 0.0f;
        }
    }

    void SpawnEnemy()
    {
        timeSinceLastEnemySpawned += Time.deltaTime;
        float x = Random.Range(-90.0f, 90.0f);
        float y = Random.Range(-90f, 90f);
        Vector3 position = new Vector3(x, y, 0);
        if (timeSinceLastEnemySpawned >= enemyspawnDelay && numberOfEnemiesInWorld < maxEnemies)
        {
            GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            numberOfEnemiesInWorld++;
            Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();
            timeSinceLastEnemySpawned = 0.0f;
        } 
    }

    public void EggDestroyed()
    {
        numberOfEggsInWorld--;
    }

    public bool IsUsingMouseControl()
    {
        return isUsingMouseControl;
    }

    public void DestroyEnemy()
    {
        numberOfEnemiesInWorld--;
        numberOfEnemiesDestroyed++;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            numberOfEnemiesTouched++;
            DestroyEnemy();
        }
    }
}