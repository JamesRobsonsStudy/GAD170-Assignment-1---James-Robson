using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
       Player Controller with that moves and turns faster as it gains XP;
	    Manages moving and rotating the player via the arrow keys.
		Up/down arrow keys to move forward and backward.
		Left/right arrow keys to rotate left and right.
*/

public class PlayerControllerThatLevelsUp : MonoBehaviour
{

    //The base move and turn speed
	public float moveSpeed = 1f;
	public float turnSpeed = 45f;
    public float jumpHeight = 5f;

    //The move and turn speed with the buffs you have from leveling up.   
    public float currentMoveSpeed;
    public float currentTurnSpeed;
    public float currentJumpHeight;



    public float xp = 0;	// Amount of XP the player has
    public float xpForNextLevel = 10;   //Xp needed to level up, the higher the level, the harder it gets. 
    public int level = 0;   // Level of the player

    Rigidbody playerRB;
  




    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        SetXpForNextLevel();
        SetCurrentMoveSpeed();
        SetCurrentTurnSpeed();
        SetCurrentJumpHeight();
    }

    //JumpCheck
    bool CheckGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
 
    

    // To level up you need to collect an amount of xp;
    // This starts at 10 xp
    // Each level you gain the xp required gets higher exponentially
    // The exponential growth is slowed by scaling it by 10%

    void SetXpForNextLevel()
    {
        xpForNextLevel = (10f + (level * level * 0.1f));
        Debug.Log("xpForNextLevel " + xpForNextLevel);
    }



    // For each level, the player adds 10% to the move speed 
    void SetCurrentMoveSpeed()
    {
        currentMoveSpeed = this.moveSpeed + (this.moveSpeed * 0.1f * level);
        Debug.Log("currentMoveSpeed = " + currentMoveSpeed);
    }

    // For each level, the player adds 10% to the turn speed 
    void SetCurrentTurnSpeed()
    {
        currentTurnSpeed = this.turnSpeed + (this.turnSpeed * (level * 0.1f));
        Debug.Log("currentTurnSpeed = " + currentTurnSpeed);
    }

    void SetCurrentJumpHeight()
    {
        currentJumpHeight = jumpHeight + (jumpHeight * (level * 0.1f));
        Debug.Log("CurrentJumpHeight = " + currentJumpHeight);
    }


    void LevelUp()
    {
        xp = 0f;
        level++;
        Debug.Log("level" + level);
        SetXpForNextLevel();
        SetCurrentMoveSpeed();
        SetCurrentTurnSpeed();
        SetCurrentJumpHeight();
    }




    //a function to make the player gain the ammount of Xp the you tell it. 
    public void GiveXP(int xpToGain)
    {
        xp += xpToGain;
        Debug.Log("Gained " + xpToGain + " XP, Current Xp = " + xp + ", XP needed to reach next Level = " + xpForNextLevel);
    }
    //Interface to get the level of the player.
    public float GetLevel()
    {
        return level;
    }

        



    

    void Update()
    {
       
        //LevelUp when the appropriate conditions are met.
        if (xp >= xpForNextLevel)
        {
            LevelUp();
        }


 

        
        // Rotation and movement speed is modifed by the level (currentMoveSpeed) of the player and by the time between update frames (Time.deltaTime). 

        // Move player via up/down arrow keys
        if (Input.GetKey(KeyCode.UpArrow) == true) { this.transform.position += this.transform.forward * currentMoveSpeed * Time.deltaTime; }
        if (Input.GetKey(KeyCode.DownArrow) == true) { this.transform.position -= this.transform.forward * currentMoveSpeed * Time.deltaTime; }

        // Rotate player via left/right arrow keys
        // Identify this position, set the vertical axis as the axis to rotate around the set the rotation speed.
        if ( Input.GetKey( KeyCode.RightArrow ) == true ){ this.transform.RotateAround( this.transform.position, Vector3.up, currentTurnSpeed  * Time.deltaTime) ; }
		if ( Input.GetKey( KeyCode.LeftArrow ) == true ){ this.transform.RotateAround( this.transform.position, Vector3.up, -currentTurnSpeed * Time.deltaTime); }

        //jumping
        if ((Input.GetKeyDown(KeyCode.Space)== true)&&CheckGrounded())
        {
            playerRB.velocity += (Vector3.up * currentJumpHeight);
        }

        //Door Interaction
        if (Input.GetKeyDown(KeyCode.E))
        {
        RaycastHit hit;
        int layermask = 1 << 8;
        if(Physics.Raycast(transform.position, Vector3.forward,out hit, 2f, layermask))
            {
                hit.collider.gameObject.GetComponent<DoorHandler>().CheckUnlock(level);
            }
         }
           
    }
}
