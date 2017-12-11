using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guy : MonoBehaviour {
    public float InputHorizontalAxis;
    public float InputVerticalAxis;
    public Rigidbody2D rb;
    public float HorizontalSensitivity;
    public float BrakeSensitivity;
    public float DiveSensitivity;
    public float hVel;
    public float vVel;

    public List<GameObject> Poses = new List<GameObject>();
    
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

    void ChangePose(int newPose) {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if(gameObject.transform.GetChild(i).gameObject.activeSelf == true)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        Poses[newPose].gameObject.SetActive(true);
    }
    
	void FixedUpdate () {
		InputHorizontalAxis = Input.GetAxis ("Horizontal");
        InputVerticalAxis = Input.GetAxis ("Vertical");

        Vector2 verticalVelocity = rb.velocity;
        
        if (InputVerticalAxis > 0) {
            verticalVelocity.y = BrakeSensitivity;
        } else if (InputVerticalAxis < 0) {
            rb.AddForce(transform.up * InputVerticalAxis * DiveSensitivity);
        } else {
            verticalVelocity.y = 0;
        }

        rb.velocity = verticalVelocity;    
        rb.AddForce(transform.right * InputHorizontalAxis * HorizontalSensitivity);

        // change pose (gO) based on h/v velocity
        {
            hVel = rb.velocity.x;
            vVel = rb.velocity.y;

            if (hVel > 0 && vVel >0) {
                ChangePose(2);
            }

            if (hVel < 0 && vVel >0) {
                ChangePose(0);
            }

            if (hVel == 0 && vVel >0) {
                ChangePose(1);
            }

            if (hVel < 0 && vVel <0) {
                ChangePose(5);
            }

            if (hVel > 0 && vVel <0) {
                ChangePose(6);
            }

            if (hVel == 0 && vVel <0) {
                ChangePose(7);
            }

            if (hVel > 0 && vVel >0) {
                ChangePose(2);
            }

            if (hVel > 0 && vVel == 0) {
                ChangePose(4);
            }

            if (hVel < 0 && vVel == 0) {
                ChangePose(3);
            }                       
            
        }
	}
}
