using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenUpBehaviour : MonoBehaviour
{
    public Text mEnemyCountText = null;
    public float initialSpeed = 20.0f;
    public float maxSpeed = 50.0f;
    public float minSpeed = 0f;
    public float mHeroRotateSpeed = 100f / 2f; // 90-degrees in 2 seconds
    public bool mFollowMousePosition = true;
    // Start is called before the first frame update

    private int mPlanesTouched = 0;
    private float currentSpeed = 0f;

    private GameController mGameGameController = null;

    void Start()
    {
        currentSpeed = initialSpeed;
        mGameGameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame

    void Update()
    {

        HandleInput();

    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mFollowMousePosition = !mFollowMousePosition;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
        Vector3 pos = transform.position;

        if (mFollowMousePosition)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Debug.Log("Position is " + pos);
            pos.z = 0f;  // <-- this is VERY IMPORTANT!
            // Debug.Log("Screen Point:" + Input.mousePosition + "  World Point:" + p);
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                pos += ((initialSpeed * Time.smoothDeltaTime) * transform.up);
            }

            if (Input.GetKey(KeyCode.S))
            {
                pos -= ((initialSpeed * Time.smoothDeltaTime) * transform.up);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(transform.forward, -mHeroRotateSpeed * Time.smoothDeltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(transform.forward, mHeroRotateSpeed * Time.smoothDeltaTime);
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            // Prefab MUST BE locaed in Resources/Prefab folder!
            GameObject egg = Instantiate(Resources.Load("Prefabs/Egg") as GameObject);
            egg.transform.up = transform.up;
            egg.transform.rotation = transform.rotation;
            Rigidbody2D eggRigidbody = egg.GetComponent<Rigidbody2D>();
            eggRigidbody.velocity = transform.up * (currentSpeed + 40f);
            Debug.Log("Spawn Eggs:" + egg.transform.localPosition);
        }
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Here x Plane: OnTriggerEnter2D");
        mPlanesTouched = mPlanesTouched + 1;
        mEnemyCountText.text = "Planes touched = " + mPlanesTouched;
        Destroy(collision.gameObject);
        mGameGameController.EnemyDestroyed();
    }

    private void OnTriggerStay2D(Collider2D collision) 
    {
        Debug.Log("Here x Plane: OnTriggerStay2D");
    }
}
