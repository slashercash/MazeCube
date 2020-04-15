using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
   
    public float speed = 5f, sensitivity = 2f, jumpForce = 4f;
    public GameObject eyes;  
    private bool hasJumped, isCrouched;
    float moveFB, moveLR, rotX, rotY, vertVelocity;
    CharacterController player;

    void Start ()
    {

        player = GetComponent<CharacterController>();
        Cursor.visible = false;

    }

    void Update()
    {

        Movement();

        if (Input.GetButtonDown("Jump"))
        {
            hasJumped = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            if (isCrouched == false)
            {
                player.height = player.height = 1;
                isCrouched = true;
            }
            else
            {
                player.height = player.height = 2;
                isCrouched = false;
            }
        }

        ApplyGravity();

    }

    void Movement()
    {
        moveFB = Input.GetAxis("Vertical") * speed;
        moveLR = Input.GetAxis("Horizontal") * speed;

        rotX = Input.GetAxis("Mouse X") * sensitivity;
        rotY = Input.GetAxis("Mouse Y") * sensitivity;

        Vector3 movement = new Vector3(moveLR, vertVelocity, moveFB);
        transform.Rotate(0, rotX, 0);
        eyes.transform.Rotate(-rotY, 0, 0);

        movement = transform.rotation * movement;
        player.Move(movement * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if(player.isGrounded == true)
        {
            if (hasJumped == false)
            {
                // konsistente gravitation erzeugen
                vertVelocity = Physics.gravity.y;
            }
            else
            {
                vertVelocity = jumpForce;
            }
        }
        else
        {
            // geschwindigkeit de Fallens konsistent erhöhen
            vertVelocity += Physics.gravity.y * Time.deltaTime;
            // Geschwindigkeitsgrenze bei -50f, 
            vertVelocity = Mathf.Clamp(vertVelocity, -50f, jumpForce);
            hasJumped = false;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Ladder")
        {
            hasJumped = true;
            vertVelocity = jumpForce;
        }
    }
}
