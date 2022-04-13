using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed = 3.0f;
    public float jumpForce = 4.0f;

    public bool idle = true;

    private bool mflag = false;
    private Vector2 pos1, pos2;
    private float minDst=0.01f; 
    private float progress;

    private Rigidbody2D rigidBody;

    void Start()
    {
        pos1 = transform.position;
        pos2 = new Vector2(0.0f, -1.0f);

        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if (idle) {
            rigidBody.gravityScale = 0;
            idleAnimation();
        } else {
            rigidBody.gravityScale = 1;
            rigidBody.velocity = new Vector2(transform.localScale.x * moveSpeed, rigidBody.velocity.y);
        }
    }

    void Update()
    {
        if(Input.GetButtonDown("Jump")) {
            jump();
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "wall") {
            flip();
        }
    }

    void jump() {
        rigidBody.velocity = new Vector2(transform.localScale.x, jumpForce);
        idle = false;
    }

    void flip() {
        Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void idleAnimation () {
        if (mflag) {
           
            transform.position = Vector2.MoveTowards(pos1, pos2, Time.deltaTime);
            //progress += Time.deltaTime;   
            if (Vector2.Distance(transform.position,pos2)<minDst) {
              mflag=!mflag;
            }

        } else {
            transform.position = Vector2.MoveTowards(pos2, pos1, Time.deltaTime);
            //progress -= Time.deltaTime;
            if (Vector2.Distance(transform.position,pos1)<minDst) {
              mflag=!mflag;
            }
        }
    }    
}
