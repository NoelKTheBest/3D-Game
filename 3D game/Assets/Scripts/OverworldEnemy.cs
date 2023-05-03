using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldEnemy : MonoBehaviour
{
    private Unit emptyUnit;
    private Unit myUnit;

    public bool dead;

    void Awake()
    {
        emptyUnit = GameObject.FindGameObjectWithTag("Unit")
            .GetComponent<Unit>();
        myUnit = GetComponent<Unit>();
    }
    
    public void UseEmptyUnit()
    {
        emptyUnit.SetUnit(myUnit);
    }

    public void UpdateMyUnit()
    {
        myUnit.currentHP = emptyUnit.currentHP;

        if (myUnit.currentHP <= 0)
        {
            dead = true;
        }
    }
}
