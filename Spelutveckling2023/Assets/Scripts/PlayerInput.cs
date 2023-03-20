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

    // värden för att spara data mellan varje uppdatering, för jämförelser och gradvisa förändringar
    bool wasMoving = false;
    bool wasGrounded = false;
    float targetAngle = 0;

    // Update is called once per frame
    void Update()
    {
        // tryck e för att atttackera
        if (Input.GetKeyDown("e"))
        {
            Debug.Log("e");
            OnAttack.Invoke();
        }

        Vector3 moveDirection = Vector3.zero;

        //Basic walk input
        moveDirection.x = Input.GetAxis("Horizontal") * walkSpeed;
        moveDirection.z = Input.GetAxis("Vertical") * walkSpeed;

        bool isMoving = moveDirection.magnitude > 0;

        // rotera spelaren mot rörelseriktning, bara om den rör sig
        if (isMoving)
        {
            // beräkna vinkel från X/Y riktning
            float signedAngle = Vector3.SignedAngle(Vector3.forward, moveDirection, Vector3.up);
            // vänd spelaren gradvis över tid mot den riktningen
            targetAngle = Mathf.MoveTowards(targetAngle, signedAngle, TurnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.AngleAxis(targetAngle, Vector3.up);
        }

        //Jump or gravity
        if (_characterController.isGrounded)
        {
            if (isMoving != wasMoving)
            {
                // sker vid start och stopp

                wasMoving = isMoving;

                if (isMoving)
                    OnMove.Invoke();
                else
                    OnStop.Invoke();
            }

            bool jumped = Input.GetButtonDown("Jump");
            storedVelocityY = jumped ? jumpStrength : GROUNDEDDOWNFORCE;

            if (!wasGrounded)
            {
                // sker vid landning
                wasGrounded = true;
                OnLand.Invoke();
            }

            if (jumped)
            {
                // sker vid hopp
                if (wasMoving) OnStop.Invoke();
                wasGrounded = false;
                OnJump.Invoke();
            }
        }
        else
        {
            // uppdatera gravitation
            storedVelocityY -= gravityForce * Time.deltaTime;
        }

        // applicera gravitation
        moveDirection.y = storedVelocityY;

        //Apply movement
        _characterController.Move(moveDirection * Time.deltaTime);

        // stäng av spelet med esc
        if (Input.GetButtonDown("Cancel"))
            Application.Quit();
    }
}
