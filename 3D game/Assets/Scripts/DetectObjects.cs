using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObjects : MonoBehaviour
{
    public string detectedObjectType;
    public GameObject detectedGameObject;
    public OverworldEnemy enemy;
    
    private void OnTriggerEnter(Collider other)
    {
        detectedGameObject = other.gameObject;
        //enemy = detectedGameObject.GetComponent<OverworldEnemy>();
        //enemy.isPlayerNearMe = true;

        if (other.gameObject.tag == "Enemy")
        {
            detectedObjectType = "Enemy";
        }
        else if (other.gameObject.tag == "Item")
        {
            //In case we add items
            detectedObjectType = "Item";
        }
        else
        {
            //For any unrecognized or irrelevant object
            detectedObjectType = "None";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //enemy.isPlayerNearMe = false;
        detectedGameObject = null;

        if (other.gameObject.tag == "Enemy")
        {
            detectedObjectType = "None";
        }
        else if (other.gameObject.tag == "Item")
        {
            detectedObjectType = "None";
        }
        else
        {
            //For any unrecognized or irrelevant object
            detectedObjectType = "None";
        }
    }
}
