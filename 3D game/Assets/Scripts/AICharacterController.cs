using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterController : MonoBehaviour
{
    public CharacterController controller;
    public Animator anim;
    public OverworldEnemy myStatus;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float gravityAccel = 10f;
    public float jumpForce = 15f;
    float turnSmoothVelocity;
    float xDirection;
    float zDirection;
    float yVelocity = 0f;

    bool isFalling = false;
    
    public float timeTillNextDirection;
    float timer;

    void Start()
    {
        timer = timeTillNextDirection;
    }

    // Update is called once per frame
    void Update()
    {
        if (!OverworldStatus.battleInProgress)
        {
            #region Rotation Code
            if (timer == timeTillNextDirection)
            {
                xDirection = Random.Range(-1f, 1f);
                zDirection = Random.Range(-1f, 1f);
                timer -= Time.deltaTime;
            }
            else if (timer < timeTillNextDirection && timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else if (timer < 0)
            {
                timer = timeTillNextDirection;
            }
            
            Vector3 direction = new Vector3(xDirection, 0f, zDirection).normalized;
            Vector3 moveDirection = new Vector3(0f, yVelocity, 0f);

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,
                    ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            }
            #endregion

            #region Jump and Gravity Code
            if (controller.isGrounded)
            {
                isFalling = false;
                yVelocity = 0f;
            }
            else
            {
                yVelocity += -1 * gravityAccel * 3f * Time.deltaTime;

                //Debug.Log("Airborne velocity:" + yVelocity);

                /* Because CharacterController can't seem to tell when it's on the ground vs when it's airborne
                 *  I've made it so that when the program chooses the else block, the player still is able to jump.
                 *  I'm checking for VERY low velocity as that indicates that the model is barely moving if at all.
                 *  I also have a boolean variable to make sure that you can't spam into a double jump.
                 *      The conditions that allow for jumping in this code block also appear at the height of the jump.
                 */
                 /*
                if (yVelocity > -1f && yVelocity < 1f && !isFalling && Input.GetButtonDown("Jump"))
                {
                    yVelocity = jumpForce;
                    anim.SetTrigger("Jump");
                    isFalling = true;
                }
                */
            }

            moveDirection.y = yVelocity;
            if (!myStatus.isPlayerNearMe)
            {
                controller.Move(moveDirection * speed * Time.deltaTime);
            }
            else
            {
                transform.LookAt(myStatus.playerTransform);
            }
            #endregion

            #region Movement Animation
            //Debug.Log(moveDirection);

            if (moveDirection.x != 0f || moveDirection.z != 0f)
            {
                anim.SetBool("isWalking", true);
            }
            else if (moveDirection.x == 0f && moveDirection.z == 0f)
            {
                anim.SetBool("isWalking", false);
            }
            #endregion
        }
    }
}
