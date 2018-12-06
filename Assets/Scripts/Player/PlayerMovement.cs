﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius;

    private Rigidbody2D rigid;
    private float axis;
    private bool jumpPressed;
    private bool isGrounded;


	void Start () {
        rigid = GetComponent<Rigidbody2D>();
	}
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    }
    private void Update(){
        axis = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space)){
            jumpPressed = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space)){
            jumpPressed = Input.GetKeyDown(KeyCode.Space);
        }
        else{
            jumpPressed = Input.GetKeyDown(KeyCode.Space);
        }


        if (isGrounded && jumpPressed){
            Vector2 force = new Vector2(0, jumpForce);
            rigid.AddForce(force, ForceMode2D.Impulse);
        }

    }

    void FixedUpdate () {
       Vector2 velocity = new Vector2(axis * moveSpeed, rigid.velocity.y);
        rigid.velocity = velocity;
	}
}
