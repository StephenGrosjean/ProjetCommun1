using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour {
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int damagesDone;
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private AudioClip destroySound;

    private Transform player;
    private GameObject gameManager;
    public Transform currentWayPoint;

    //Scripts
    private Rigidbody2D rigid;
    private GameManager gameManagerScript;
    private PlayerMovement playerMovementScript;
    private SpriteRenderer spriteRendererComponent;
    private AudioSource audioSourceComponent;

    void Start ()
    {
        currentWayPoint = wayPoints[0];
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        playerMovementScript = player.GetComponent<PlayerMovement>();
        spriteRendererComponent = GetComponent<SpriteRenderer>();
        audioSourceComponent = GetComponent<AudioSource>();
       
	}

    private void Update()
    {
        float yVel = rigid.velocity.y;

        //Flip the sprite toward the player
        Vector2 vectorToPlayer = transform.position - currentWayPoint.position;
        spriteRendererComponent.flipX = (vectorToPlayer.x > 0);


    }

    void FixedUpdate ()
    {
        Vector2 playerPos = new Vector2(currentWayPoint.position.x, transform.position.y);
        if(transform.position.x == currentWayPoint.position.x)
        {
            currentWayPoint = NextWayPoint();
        }
        transform.position = Vector3.MoveTowards(transform.position, playerPos, moveSpeed);
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            ContactPoint2D contacts = other.GetContact(0);

            Vector2 forceToApply = new Vector2(contacts.point.x - player.position.x, contacts.point.y - player.position.y);
            playerMovementScript.StartCoroutine("EnemyTouch", -forceToApply);
            gameManagerScript.RemoveHealth(damagesDone);
            audioSourceComponent.PlayOneShot(destroySound);
            DestroyParticles();
            Destroy(gameObject);
        }
    }

    Transform NextWayPoint()
    {
        Transform newWayPoint;
        if (currentWayPoint == wayPoints[0])
        {
            newWayPoint = wayPoints[1];
        }
        else
        {
            newWayPoint = wayPoints[0];
        }
        return newWayPoint;
    }

    public void DestroyParticles()
    { 
        Instantiate(deathParticles, transform.position, Quaternion.identity);
    }
}
