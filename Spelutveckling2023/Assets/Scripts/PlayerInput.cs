using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //Constants
    const float GROUNDEDDOWNFORCE = -1f; //Constant force to apply when grounded to make sure character sticks to the ground

    //Components
    CharacterController _characterController;

    //Settings
    public float walkSpeed = 5f;
    public float gravityForce = 9.82f;
    public float jumpStrength = 7f;

    //Other variables
    float storedVelocityY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        //Basic walk input
        moveDirection.x = Input.GetAxis("Horizontal") * walkSpeed;
        moveDirection.z = Input.GetAxis("Vertical") * walkSpeed;

        //Jump or gravity
        if (_characterController.isGrounded)
            storedVelocityY = Input.GetButtonDown("Jump") ? jumpStrength : GROUNDEDDOWNFORCE;
        else
            storedVelocityY -= gravityForce * Time.deltaTime;

        moveDirection.y = storedVelocityY;

        //Apply movement
        _characterController.Move(moveDirection * Time.deltaTime);

        if (Input.GetButtonDown("Cancel"))
            Application.Quit();
    }
}
