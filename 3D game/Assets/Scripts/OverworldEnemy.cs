using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldEnemy : MonoBehaviour
{
    private Unit emptyUnit;
    private Unit myUnit;

    public bool dead;
    public Animator anim;
    public bool isPlayerNearMe;
    public Transform playerTransform;
    public float detectionDistanceX;
    public float detectionDistanceZ;

    bool inRangeX;
    bool inRangeZ;

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

        if (Mathf.Abs(playerTransform.position.x - transform.position.x) <= detectionDistanceX)
        {
            inRangeX = true;
        }
        else if (Mathf.Abs(playerTransform.position.x - transform.position.x) > detectionDistanceX)
        {
            inRangeX = false;
        }

        if (Mathf.Abs(playerTransform.position.z - transform.position.z) <= detectionDistanceZ)
        {
            inRangeZ = true;
        }
        else if (Mathf.Abs(playerTransform.position.z - transform.position.z) > detectionDistanceZ)
        {
            inRangeZ = false;
        }

        //if (Mathf.Abs(playerTransform.position.z - transform.position.z) <= detectionDistanceZ) inRangeZ = true;
        if (inRangeX && inRangeZ) isPlayerNearMe = true;//Debug.Log("in range");
        if (!inRangeX && !inRangeZ) isPlayerNearMe = false;// Debug.Log("out of range");
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
            gameObject.SetActive(false);
        }
    }
}
