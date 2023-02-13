using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    [Space]
    public float TurnSpeed = 1f;

    //Other variables
    float storedVelocityY = 0f;

    // events
    [Space]
    public UnityEvent OnMove;
    public UnityEvent OnStop;
    public UnityEvent OnJump;
    public UnityEvent OnLand;

    public UnityEvent OnAttack;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    bool wasMoving = false;
    bool wasGrounded = false;
    float targetAngle = 0;

    // Update is called once per frame
    void Update()
    {
        // lägg till under
        if (Input.GetKeyDown("e"))
        {
            Debug.Log("e");
            OnAttack.Invoke();
        }
		// lägg till ovan

		Vector3 moveDirection = Vector3.zero;

        //Basic walk input
        moveDirection.x = Input.GetAxis("Horizontal") * walkSpeed;
        moveDirection.z = Input.GetAxis("Vertical") * walkSpeed;

        if (moveDirection.sqrMagnitude > 0)
        {
            float signedAngle = Vector3.SignedAngle(Vector3.forward, moveDirection, Vector3.up);
            targetAngle = Mathf.MoveTowards(targetAngle, signedAngle, TurnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.AngleAxis(targetAngle, Vector3.up);
        }

        bool isMoving = moveDirection.sqrMagnitude > float.Epsilon;

        //Jump or gravity
        if (_characterController.isGrounded)
        {
            if (isMoving != wasMoving)
            {
                wasMoving = isMoving;

                if (isMoving) OnMove.Invoke();
                else OnStop.Invoke();
            }

            bool jumped = Input.GetButtonDown("Jump");
            storedVelocityY = jumped ? jumpStrength : GROUNDEDDOWNFORCE;

            if (!wasGrounded)
            {
                wasGrounded = true;
                OnLand.Invoke();
            }

            if (jumped)
            {
                if (wasMoving) OnStop.Invoke();
                wasGrounded = false;
                OnJump.Invoke();
            }
        }
        else
        {
            storedVelocityY -= gravityForce * Time.deltaTime;
        }

        moveDirection.y = storedVelocityY;

        //Apply movement
        _characterController.Move(moveDirection * Time.deltaTime);


        if (Input.GetButtonDown("Cancel"))
            Application.Quit();
    }
}
