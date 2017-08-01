using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovementInputController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public Text v_input;
    public Text h_input;

    public float maxSpeed;
    public float h_value;
    public float v_value;

    public float h_offset;
    public float v_offset;


    private void Update()
    {
        //DetectTouch();

        //h_value = CrossPlatformInputManager.GetAxis("Horizontal");
        //v_value = CrossPlatformInputManager.GetAxis("Vertical");

    }
    private void FixedUpdate()
    {
        // Send values and parameters to movement service
        ////MovementService.MoveObject(h_value, rigidBody, maxSpeed);
        //MovementService.TranslateObjectHorizontally(this.gameObject, h_value, maxSpeed);

        ////MovementService.BoostObjectVertically(h_value, v_value, rigidBody, maxSpeed);
        MovementService.BoostObject(h_value, v_value, rigidBody, maxSpeed);
        h_value = 0;
        v_value = 0;
    }

    void DetectTouch()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // Get movement of the finger since last frame
            Vector2 touchDeltaPosition = Input.GetTouch(0).position;
            Vector3 worldTouchPoint = Camera.main.ScreenToWorldPoint( new Vector3(touchDeltaPosition.x, touchDeltaPosition.y));

            h_input.text = worldTouchPoint.x.ToString();
            v_input.text = worldTouchPoint.y.ToString();

            if (worldTouchPoint.x > transform.position.x)
            {
                h_value = 1; //Right
            }
            else if (worldTouchPoint.x <   transform.position.x)
            {
                h_value = -1; //Left
            }
            else
            {
                h_value = 0;
            }

            if (worldTouchPoint.y > transform.position.y + v_offset)
            {
                v_value = 1; //Up
            }
            else if (worldTouchPoint.y <  transform.position.y - v_offset)
            {
                v_value = -1; //Down
            }
            else
            {
                v_value = 0;
            }
        }
        else
        {
            h_value = 0;
            v_value = 0;
        }
    }
    public void MoveUp()
    {
        RemoveVelocity();
        v_value = 1;
    }

    public void MoveDown()
    {
        RemoveVelocity();
        v_value = -1;
    }

    public void MoveLeft()
    {
        RemoveVelocity();
        h_value = -1;
        
        //Add opposite force
        //MovementService.BoostObject(0.3f, 0, rigidBody, maxSpeed);
    }

    public void MoveRight()
    {
        RemoveVelocity();
        h_value = 1;

        //Add opposite force
        //MovementService.BoostObject(-0.3f, 0, rigidBody, maxSpeed);
    }

    void RemoveVelocity()
    {
        this.rigidBody.velocity = Vector2.zero;
        this.rigidBody.angularVelocity = 0;
    }
}
