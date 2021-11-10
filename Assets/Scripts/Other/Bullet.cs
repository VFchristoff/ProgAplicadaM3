using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody2D rb;
    public float damage;
    private GameObject player;

    private void Start() 
    {
        player = GameObject.FindWithTag("Player");
        
        Destroy(this.gameObject,3);
        rb.velocity = transform.right * (bulletSpeed * player.transform.localScale.x);
    }

    void OnTriggerEnter2D(Collider2D hitInfo) 
    {
        AIPatrol enemy = hitInfo.GetComponent<AIPatrol>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Debug.Log(hitInfo.name);
        Destroy(this.gameObject);
    }
}
