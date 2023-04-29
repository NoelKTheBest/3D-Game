using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObjects : MonoBehaviour
{
    public string detectedObject;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            detectedObject = "Enemy";
        }
        else if (other.gameObject.tag == "Item")
        {
            //In case we add items
            detectedObject = "Item";
        }
        else
        {
            //For any unrecognized or irrelevant object
            detectedObject = "None";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            detectedObject = "None";
        }
        else if (other.gameObject.tag == "Item")
        {
            detectedObject = "None";
        }
        else
        {
            //For any unrecognized or irrelevant object
            detectedObject = "None";
        }
    }
}
