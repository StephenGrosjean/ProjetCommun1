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

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealthScript = player.GetComponent<PlayerHealth>();
       
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector2 playerPos = new Vector2(player.position.x, transform.position.y);
    
        transform.position = Vector3.MoveTowards(transform.position, playerPos, moveSpeed);
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerHealthScript.RemoveLife(damagesDone);
            Destroy(gameObject);
        }
    }
}
