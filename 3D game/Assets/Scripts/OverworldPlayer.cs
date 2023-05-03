using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldPlayer : MonoBehaviour
{
    private DetectObjects obj;
    //private BattleSystem bs;

    public OverworldStatus overworld;

    void Awake()
    {
        obj = GetComponentInChildren<DetectObjects>();
    }

    // Update is called once per frame
    void Update()
    {
        if (obj.detectedObjectType == "Enemy")
        {
            if (Input.GetButtonDown("Engage"))
            {
                //overworld.OnBattleStart.AddListener(obj.detectedGameObject.GetComponent<OverworldEnemy>().UseEmptyUnit); - Too slow
                OverworldEnemy enemy = obj.detectedGameObject.GetComponent<OverworldEnemy>();

                if (enemy.dead) return;

                enemy.UseEmptyUnit();
                StartBattle();
            }
        }
    }

    void StartBattle()
    {
        OverworldStatus.battleInProgress = true;
    }

    public void BattleEnded()
    {
        obj.detectedGameObject.GetComponent<OverworldEnemy>().UpdateMyUnit();
    }
}
