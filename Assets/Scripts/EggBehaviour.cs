using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBehaviour : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += transform.up * (kEggSpeed * Time.smoothDeltaTime);
       // mLifeCount--;
    }

    private void OnBecameInvisible()
    {
        HeroMovement heroMovement = FindObjectOfType<HeroMovement>();
        Destroy(gameObject);
    }
}
