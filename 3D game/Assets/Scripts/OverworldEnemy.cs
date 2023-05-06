using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldEnemy : MonoBehaviour
{
    private Unit emptyUnit;
    private Unit myUnit;

    public bool dead;
    public Animator anim;

    void Awake()
    {
        emptyUnit = GameObject.FindGameObjectWithTag("Unit")
            .GetComponent<Unit>();
        myUnit = GetComponent<Unit>();
    }
    
    void Update()
    {
        if (dead)
        {
            anim.SetBool("isDead", true);
        }
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
