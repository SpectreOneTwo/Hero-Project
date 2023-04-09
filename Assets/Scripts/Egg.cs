using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Hero hero = GameObject.FindWithTag("Hero").GetComponent<Hero>();
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Hero hero = FindObjectOfType<Hero>();
        Destroy(gameObject);
        hero.EggDestroyed();
    }
}