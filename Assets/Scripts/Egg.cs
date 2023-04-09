using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            HeroMovement hero = GameObject.FindWithTag("Hero").GetComponent<HeroMovement>();
            EnemyBehavior enemy = GameObject.FindWithTag("Enemy").GetComponent<EnemyBehavior>();
            hero.EggDestroyed();
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        HeroMovement heroMovement = FindObjectOfType<HeroMovement>();
        Destroy(gameObject);
        heroMovement.EggDestroyed();
    }
}