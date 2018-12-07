using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour {

    [SerializeField] private float moveSpeed;
    [SerializeField] private int damagesDone;

    private Transform player;
    //Scripts
    private Rigidbody2D rigid;
    private PlayerHealth playerHealthScript;
    private PlayerMovement playerMovementScript;
    private SpriteRenderer spriteRendererComponent;

    void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealthScript = player.GetComponent<PlayerHealth>();
        playerMovementScript = player.GetComponent<PlayerMovement>();
        spriteRendererComponent = GetComponent<SpriteRenderer>();
       
	}

    private void Update()
    {
        float yVel = rigid.velocity.y;

        //Flip the sprite toward the player
        Vector2 vectorToPlayer = transform.position - player.position;
        spriteRendererComponent.flipX = (vectorToPlayer.x > 0);


    }

    void FixedUpdate ()
    {
        Vector2 playerPos = new Vector2(player.position.x, transform.position.y);
    
        transform.position = Vector3.MoveTowards(transform.position, playerPos, moveSpeed);
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            ContactPoint2D contacts = other.GetContact(0);

            Vector2 forceToApply = new Vector2(contacts.point.x - player.position.x, contacts.point.y - player.position.y);
            playerMovementScript.StartCoroutine("EnemyTouch", -forceToApply);
            playerHealthScript.RemoveLife(damagesDone);
            Destroy(gameObject);
        }
    }
}
