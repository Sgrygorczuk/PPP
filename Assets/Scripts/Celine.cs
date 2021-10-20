using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celine : MonoBehaviour
{

    //========================= Horizontal Movement 
    public float xSpeed;        //Speed that player moves horizontally 
    
    //======================== Jumping 
    private bool isGrounded = false; //Tells us if we're touching the ground 
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround; 

    public float ySpeed;

    //================================== Internal References  
    Rigidbody2D _rigidbody;  //Used for physics 
    Animator animator; //Used to update the given animation  


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float xInput = horizontalMovement();
        float yInput = verticalMovement();
        _rigidbody.velocity = new Vector2(xInput * xSpeed, yInput);

    }

    /**
    * Purpose: Listen to player inputs and update the x movement and direction faced 
    * Return: Player's x velocity  
    */
    float horizontalMovement(){
        float xInput = 0;

        if(Input.GetKey(KeyCode.A)  || Input.GetKey(KeyCode.LeftArrow)){
            xInput = -1;
            animator.SetBool("isRunning", true);
        }
        else if(Input.GetKey(KeyCode.D) ||Input.GetKey(KeyCode.RightArrow)){
            xInput = 1;
            animator.SetBool("isRunning", true);
        }

        changeDirectionFacing(xInput); 
        updateRunning(xInput);

        return xInput;
    }

    /**
    * Purpose: Flip the player to the opposite side that they're currently facing 
    */
    void changeDirectionFacing(float xInput){
        //Changes which way the player is facing 
        if(xInput > 0){
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else if(xInput < 0){
            transform.eulerAngles = new Vector3(0,180,0);
        }
    }

    void updateRunning(float xInput){
        if(xInput == 0){
            animator.SetBool("isRunning", false);
        }
        else{
            animator.SetBool("isRunning", true);
        }
    }

    /**
    * Purpose: Listens for player jump action 
    * Return: Player's y velcity 
    */
    float verticalMovement(){
        float yInput = _rigidbody.velocity.y;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if ((Input.GetKey(KeyCode.UpArrow) ||  Input.GetKey(KeyCode.W)) && isGrounded)
        {
            yInput = ySpeed; 
        }

        if(isGrounded){
            animator.SetBool("isInAir", false);
        }
        else{
            animator.SetBool("isInAir", true);
        }

        return yInput;
    }
}
