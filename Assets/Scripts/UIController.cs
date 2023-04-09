using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Hero hero;

    private Text textComponent;
    private string control;

    private void Start()
    {
        textComponent = GetComponent<Text>();
    }

    private void Update()
    {
        bool isUsingMouseControl = hero.IsUsingMouseControl();
        if (isUsingMouseControl)
        {
            control = "Mouse";
        }
        else
        {
            control = "WASD";
        }
        int numberOfEnemiesTouched = hero.numberOfEnemiesTouched;
        int numberOfEggsInWorld = hero.numberOfEggsInWorld;
        int numberOfEnemiesInWorld = hero.numberOfEnemiesInWorld;
        int numberOfEnemiesDestroyed = hero.numberOfEnemiesDestroyed;

        textComponent.text = string.Format("Control: {0}  Enemies touched: {1}  Eggs in world: {2}  Enemies in world: {3} Enemies destroyed: {4}",
            control, numberOfEnemiesTouched, numberOfEggsInWorld, numberOfEnemiesInWorld, numberOfEnemiesDestroyed);
    }
}