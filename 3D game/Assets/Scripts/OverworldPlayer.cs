using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldPlayer : MonoBehaviour
{
    private DetectObjects obj;
    
    void Awake()
    {
        obj = GetComponentInChildren<DetectObjects>();
        Debug.Log(obj);
    }

    // Update is called once per frame
    void Update()
    {
        if (obj.detectedObject == "Enemy")
        {
            Debug.Log("let's go!");
            if (Input.GetButtonDown("Engage"))
            {
                Debug.Log("ATTACK!!");
                StartBattle();
            }
        }
    }

    void StartBattle()
    {
        OverworldStatus.battleInProgress = true;
    }

    void BattleEnded()
    {
        OverworldStatus.battleInProgress = false;
    }
}
