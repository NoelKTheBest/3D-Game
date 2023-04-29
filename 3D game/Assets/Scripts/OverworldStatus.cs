using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum BattleStatus { STARTED, FINISHED }

public class OverworldStatus : MonoBehaviour
{
    public static bool battleInProgress;
    //public GameObject battleOverlay; //might need later(?)
    public UnityEvent OnBattleStart;
    public UnityEvent OnBattleEnd;

    void Update()
    {
        if (battleInProgress)
        {
            if (OnBattleStart != null) OnBattleStart.Invoke();
        }
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
