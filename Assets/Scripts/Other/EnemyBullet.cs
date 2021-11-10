using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{   
    public float bulletSpeed;
    public Rigidbody2D rb;
    public float damage;

    public Transform playerLocation;
    private int direction;

    private void Start() 
    {
        playerLocation = GameObject.FindWithTag("Player").transform;


        if(playerLocation.position.x < transform.position.x) direction = -1;
        if(playerLocation.position.x > transform.position.x) direction = 1;
        Destroy(this.gameObject,3);
        rb.velocity = new Vector2(1,0) * bulletSpeed * direction;

        Debug.Log(direction);
    }

    void OnTriggerEnter2D(Collider2D hitInfo) 
    {
        Player player = hitInfo.GetComponent<Player>();
        if(player != null)
        {
            player.TakeDamage(damage);
        }
        Debug.Log(hitInfo.name);
        Destroy(this.gameObject);
    }

}
