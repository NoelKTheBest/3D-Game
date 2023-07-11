using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public enum BattleStatus { STARTED, FINISHED }

public class OverworldStatus : MonoBehaviour
{
    public static bool battleInProgress;
    //public GameObject battleOverlay; //might need later(?)
    public UnityEvent OnBattleStart;
    public UnityEvent OnBattleEnd;
    public CinemachineFreeLook cinemaCam;
    public GameObject boss;

    public OverworldEnemy[] enemies;

    int deathCount = 0;
    int enemyCount;

    bool hasBeenCalled = false;

    void Awake()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    void Start()
    {
        //boss.SetActive(false);
    }

    void Update()
    {
        if (battleInProgress && !hasBeenCalled)
        {
            if (OnBattleStart != null) OnBattleStart.Invoke();
            StopCamera();

            hasBeenCalled = true;
        }
        else if (!battleInProgress && hasBeenCalled)
        {
            if (OnBattleEnd != null) OnBattleEnd.Invoke();
            RestoreCamera();

            hasBeenCalled = false;
        }

        if (battleInProgress)
        {
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].dead) deathCount++;
        }

        if (deathCount == enemyCount)
        {
            boss.SetActive(true);
        }
    }

    void RestoreCamera()
    {
        cinemaCam.m_YAxis.m_MaxSpeed = 4f;
        cinemaCam.m_XAxis.m_MaxSpeed = 450f;
    }

    void StopCamera()
    {
        cinemaCam.m_YAxis.m_MaxSpeed = 0f;
        cinemaCam.m_XAxis.m_MaxSpeed = 0f;
    }

    /*
    public void OpenOverlay()
    {
        //For later:

        //  Add animations

        battleOverlay.gameObject.SetActive(true);
    }

    public void CloseOverlay()
    {
        //For later:

        //  Add animations

        battleOverlay.gameObject.SetActive(false);
    }
    */
}
