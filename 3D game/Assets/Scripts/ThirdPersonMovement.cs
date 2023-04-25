using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float gravityAccel = 10f;
    public float timeIncrememt = 1f;
    public float jumpForce = 15f;
    float turnSmoothVelocity;
    float yVelocity = 0f;
    float timeNotGrounded = 0f;

    bool isFalling;
    // Update is called once per frame
    void Update()
    {
        #region Rotation Code
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 moveDirection = new Vector3(0f, yVelocity, 0f);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z)* Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,
                ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        #endregion

        if (Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpForce;
        }
        
        if (controller.isGrounded)
        {
            yVelocity = 0f;
            timeNotGrounded += 0f;
        }
        else
        {
            yVelocity += -1 * gravityAccel * 3f * Time.deltaTime * timeNotGrounded;
            timeNotGrounded += timeIncrememt;
        }

        moveDirection.y = yVelocity;
        controller.Move(moveDirection.normalized * speed * Time.deltaTime);
    }
}
