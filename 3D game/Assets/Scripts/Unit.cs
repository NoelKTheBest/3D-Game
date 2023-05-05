using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int currentHP;

    public void SetUnit(Unit unit)
    {
        unitName = unit.unitName;
        unitLevel = unit.unitLevel;
        damage = unit.damage;
        maxHP = unit.maxHP;
        currentHP = unit.currentHP;
    }

    public void ResetUnit()
    {
        unitName = "";
        unitLevel = 0;
        damage = 0;
        maxHP = 0;
        currentHP = 0;
    }

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }
}
