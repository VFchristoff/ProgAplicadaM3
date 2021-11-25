using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //VARIÁVEIS DECLARADAS

    public float bulletSpeed; //velocidade da bala
    public Rigidbody2D rb; //corpo da bala
    public float damage; //variavel de dano

    public Transform playerLocation; //localizacao do jogador
    private int direction; //direcao

    private void Start() 
    {
        playerLocation = GameObject.FindWithTag("Player").transform; //procura a localizacao do jogador e armazena na variavel
        //calcula se a localizacao do jogador está na direita ou na esquerda
        if (playerLocation.position.x < transform.position.x) direction = -1;
        if(playerLocation.position.x > transform.position.x) direction = 1;
        //destroi o objeto bala
        Destroy(this.gameObject,3);
        //calcula a velocidade da bala
        rb.velocity = new Vector2(1,0) * bulletSpeed * direction;

        Debug.Log(direction);
    }

    //COLISAO DA BALA
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
