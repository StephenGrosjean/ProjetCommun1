using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float enemyTouchForce;
    [SerializeField] private GameObject playerSpriteObject;


    private Animator animatorComponent;
    private SpriteRenderer spriteComponent;
    private Rigidbody2D rigid;
    private float axis;
    private bool jumpPressed;
    private bool isGrounded;
    private bool asTouchedEnemy;

	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        animatorComponent = playerSpriteObject.GetComponent<Animator>();
        spriteComponent = playerSpriteObject.GetComponent<SpriteRenderer>();
	}
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    }
    private void Update(){
        axis = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        float yVel = rigid.velocity.y;
        spriteComponent.flipX = (axis < 0);



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

        animatorComponent.SetBool("isWalking", ((axis != 0) && isGrounded));
        animatorComponent.SetBool("isJumping", !isGrounded);


    }

    void FixedUpdate () {
       Vector2 velocity = new Vector2(axis * moveSpeed, rigid.velocity.y);

        if (!asTouchedEnemy){
            rigid.velocity = velocity;
        }
    }

    public IEnumerator EnemyTouch(Vector2 forceVector){
        asTouchedEnemy = true;
        rigid.AddForce(forceVector*enemyTouchForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(.3f);
        asTouchedEnemy = false;
    }
}
