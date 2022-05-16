using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonBehaviour<PlayerManager>
{
    private float xInput;
    private float yInput;
    private bool isCarrying;
    private bool isIdle;
    private bool isRunning;
    private bool isWalking;
    private bool isLiftingToolDown;
    private bool isLiftingToolUp;
    private bool isLiftingToolLeft;
    private bool isLiftingToolRight;
    private bool isUsingToolDown;
    private bool isUsingToolUp;
    private bool isUsingToolLeft;
    private bool isUsingToolRight;
    private bool isSwingingToolDown;
    private bool isSwingingToolUp;
    private bool isSwingingToolLeft;
    private bool isSwingingToolRight;
    private bool isPickingUp;
    private bool isPickingDown;
    private bool isPickingLeft;
    private bool isPickingRight;
    private ToolEffect toolEffect = ToolEffect.none;

    [SerializeField]
    private Rigidbody2D rigidbody2d;

    private Direction playerDirection;

    private float movementSpeed;

    private bool playerInputIsDisabled;
    public bool PlayerInputIsDisabled
    {
        get => playerInputIsDisabled;
        set => playerInputIsDisabled = value;
    }


    private void Update()
    {
        ResetAnimationTriggers();
        PlayerMovementInput();
        PlayerWalkInput();

        EventsHandler.CallMovementEvent(xInput, yInput, isWalking, isRunning, isIdle, isCarrying, toolEffect,
            isUsingToolLeft, isUsingToolRight, isUsingToolUp, isUsingToolDown,
            isLiftingToolRight, isLiftingToolLeft, isLiftingToolUp, isLiftingToolDown,
            isPickingRight, isPickingLeft, isPickingUp, isPickingDown,
            isSwingingToolRight, isSwingingToolLeft, isSwingingToolUp, isSwingingToolDown,
            false, false, false, false);
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        Vector2 move = new Vector2(xInput * movementSpeed * Time.deltaTime, yInput * movementSpeed * Time.deltaTime);
        rigidbody2d.MovePosition(rigidbody2d.position + move);
    }

    private void PlayerWalkInput()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            isRunning = false;
            isWalking = true;
            isIdle = false;
            movementSpeed = Settings.walkigSpeed;
        }
        else
        {
            isRunning = true;
            isWalking = false;
            isIdle = false;
            movementSpeed = Settings.runningSpeed;
        }
    }

    private void PlayerMovementInput()
    {
        yInput = Input.GetAxis("Vertical");
        xInput = Input.GetAxis("Horizontal");

        if (yInput != 0 && xInput != 0) ;
        {
            xInput = xInput * 0.71f;
            yInput = yInput * 0.71f;
        }

        if (xInput != 0 || yInput != 0)
        {
            isRunning = true;
            isWalking = false;
            isIdle = false;
            movementSpeed = Settings.runningSpeed;

            if (xInput < 0)
            {
                playerDirection = Direction.left;
            }
            else if (xInput > 0)
            {
                playerDirection = Direction.right;
            }
            else if (xInput < 0)
            {
                playerDirection = Direction.down;
            }
            else
            {
                playerDirection = Direction.up;
            }
        }
        else if (xInput == 0 && yInput == 0)
        {
            isRunning = false;
            isWalking = false;
            isIdle = true;
        }
    }

    private void ResetAnimationTriggers()
    {
        isLiftingToolDown = false;
        isLiftingToolUp = false;
        isLiftingToolLeft = false;
        isLiftingToolRight = false;
        isUsingToolDown = false;
        isUsingToolUp = false;
        isUsingToolLeft = false;
        isUsingToolRight = false;
        isSwingingToolDown = false;
        isSwingingToolUp = false;
        isSwingingToolLeft = false;
        isSwingingToolRight = false;
        isPickingUp = false;
        isPickingDown = false;
        isPickingLeft = false;
        isPickingRight = false;
        toolEffect = ToolEffect.none;
    }
}