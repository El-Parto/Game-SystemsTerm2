using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("RPG/Player/Movement")]
[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    public bool creatingChar;
    #region Stats
    [Header("HP and MP")]
    public float health = 100;
    public float mana = 75;
    public float hpRegen = 0.01f;
    public float manaRegen = 0.005f;

    [Header("Speed Vars")]
    public float moveSpeed;    
    public float walkSpeed, runSpeed, crouchSpeed, jumpSpeed;
    #endregion

    private float _gravity = 20.0f;
    private Vector3 _moveDir;
    public CharacterController _charC;
    private Animator characterAnimator;


    private void Start()
    {
        _charC = GetComponent<CharacterController>();
        //can't get animator component because it's not in the GO.
        //so we need get component in children.
        characterAnimator = GetComponentInChildren<Animator>();// this will specifically tell the GO to get the component queried in < >

    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        //this assumes character is moving all the time
        

        // not using vector two for movement, but using it as a way to store two floats this way you only have one variable(?)
        Vector2 controlVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //maginitude = how long the line is. it's a float.
        //if it's not 0, then it's being pushed somewhere. Keyboard, 0 is fine,
        // but controller you need a deadzone.
        /*  Slow way
                if (controlVector.magnitude >= 0.05f)
                {
                    characterAnimator.SetBool("Moving", true);
                }
                else
                {
                    characterAnimator.SetBool("Moving", false);
                }
        */
        //fast way
        //This is a terniary operator The downside is that you can't use else statment
        //bip = bipedal
        characterAnimator.SetBool("Moving", controlVector.magnitude >= 0.05);

        if (_charC.isGrounded)
        {
            if (Input.GetButton("Sprint"))
            {
                moveSpeed = runSpeed;
                characterAnimator.SetFloat("Speed", 2.0f);// set float here refers to our parameter that we set for speed
            }
            else if (Input.GetButton("Crouch"))
            {
                moveSpeed = crouchSpeed;
                characterAnimator.SetFloat("Speed", 0.5f);
            }
            else
            {
                moveSpeed = walkSpeed;
                characterAnimator.SetFloat("Speed", 1.0f);
            }
            _moveDir = transform.TransformDirection(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * moveSpeed); 
            if (Input.GetButton("Jump"))
            {
                _moveDir.y = jumpSpeed;
            }
        }
        _moveDir.y -= _gravity * Time.deltaTime;
        _charC.Move(_moveDir * Time.deltaTime);
    }


    public void RaiseStat(int value)
    { //this button will increase movement speed give interger of 1
        switch (value)
        {
            case 1:
                health += 1; //in the first case, it'll increment by 1 the value inside the variable "moveSpeed"
                break;
            case 2:
                mana += 1; //in the second case, it'll increment by 1 the value inside the variable "jumpHeight"
                break;
            case 3:
                moveSpeed += 1; //in the sprintMulti case, it'll increment by 1 the value inside the variable "sprintMulti"
                break;
            case 4:
                hpRegen += 1; //in the maxFuel case, it'll increment by 1 the value inside the variable "maxFuel"
                break;
            case 5:
                manaRegen += 1; //adds one to the value inside the variable "jetpackForce"
                break;
            case 6:
                jumpSpeed += 1;
                break;


        }
    }
    public void LowerStat(int value)
    { //this button will increase movement speed give interger of 1
        switch (value)
        {
            case 1:
                moveSpeed -= 1; //in the first case, it'll decrease by 1 the value inside the variable "moveSpeed"
                break;
            case 2:
                jumpSpeed -= 1; //in the second case, it'll decrease by 1 the value inside the variable "jumpHeight"
                break;
            case 3:
                moveSpeed -= 1; //in the sprintMulti case, it'll decrease by 1 the value inside the variable "sprintMulti"
                break;
            case 4:
                hpRegen -= 1; //in the maxFuel case, it'll decrease by 1 the value inside the variable "maxFuel"
                break;
            case 5:
                manaRegen -= 1; //subtracts one to the value inside the variable "jetpackForce"
                break;
            case 6:
                jumpSpeed += 1;
                break;

        }
    }

}