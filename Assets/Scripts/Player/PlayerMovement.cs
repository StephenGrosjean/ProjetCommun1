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
    [SerializeField] private AudioClip jumpSound;


    private Animator animatorComponent;
    private SpriteRenderer spriteComponent;
    private Rigidbody2D rigid;
    private AudioSource audioSourceComponent;

    private float axis;
    private bool jumpPressed;
    private bool isGrounded;
    private bool asTouchedEnemy;

    private int lastAxis;
    public int currentAxis;

	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        animatorComponent = playerSpriteObject.GetComponent<Animator>();
        spriteComponent = playerSpriteObject.GetComponent<SpriteRenderer>();
        audioSourceComponent = GetComponent<AudioSource>();
        currentAxis = 1;
	}
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    }
    private void Update(){
        axis = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        lastAxis = currentAxis;

        if((axis > -1.0f && axis < -0.1f) || (axis < 1.0f && axis > 0.1f))
        {
            currentAxis = Mathf.CeilToInt(axis);
        }

        if (currentAxis != lastAxis)
        {
            if(currentAxis > 0)
            {
                spriteComponent.flipX = false;

            }
            else
            {
                spriteComponent.flipX = true;

            }
        }

        if (Input.GetButtonDown("Jump")){
            jumpPressed = true;
        }
        else if (Input.GetButtonUp("Jump")){
            jumpPressed = Input.GetButtonDown("Jump");
        }
        else{
            jumpPressed = Input.GetButtonDown("Jump");
        }

        if (isGrounded && jumpPressed){
            Vector2 force = new Vector2(0, jumpForce);
            rigid.AddForce(force, ForceMode2D.Impulse);
            audioSourceComponent.PlayOneShot(jumpSound);
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
