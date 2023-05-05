using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObjects : MonoBehaviour
{
    public string detectedObjectType;
    public GameObject detectedGameObject;
    
    private void OnTriggerEnter(Collider other)
    {
        detectedGameObject = other.gameObject;

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
