using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatAIMovement : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public float speed = 5f;
    public int scene;
    private bool movingTowardsPoint2 = true;
    private bool inCombat = false;
    
    private void FixedUpdate()
    {
        if (!inCombat)
        {
            Vector3 currentPos = transform.position;
            Vector3 point1Pos = new Vector3(point1.position.x, currentPos.y, point1.position.z);
            Vector3 point2Pos = new Vector3(point2.position.x, currentPos.y, point2.position.z);

            if (movingTowardsPoint2)
            {
                transform.position = Vector3.MoveTowards(currentPos, point2Pos, speed * Time.fixedDeltaTime);

                if (transform.position == point2Pos)
                {
                    movingTowardsPoint2 = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(currentPos, point1Pos, speed * Time.fixedDeltaTime);

                if (transform.position == point1Pos)
                {
                    movingTowardsPoint2 = true;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !inCombat)
        {
            inCombat = true;
            SceneManager.LoadScene(scene);
        }
    }
}