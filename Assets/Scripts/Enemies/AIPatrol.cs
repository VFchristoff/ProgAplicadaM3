using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{

    public float life; // vida do inimigo
    public float speed; // velocidade do inimigo
    public float range; //alcance da visão do inimigo
    public float attackRange; // alcance do ataque do inimigo melee
    private float meleeSpeed, shooterSpeed; // velocidade do ataque do inimigo melee e de longo alcance
    public float meleeDamage, shooterDamage; //dano do ataque do inimigo melee e de longo alcance

    public Player player; //achar o script player

    [HideInInspector]
    public bool mustPatrol; // verificar se o inimigo deve patrulhar
    [HideInInspector]
    public bool mustTurn; // verificar se o inimigo deve virar
    [HideInInspector]
    public bool isChasing; // verificar se o inimigo está em modo de perseguição
    public bool canAttack; //verificar se o inimigo pode atacar

    public Rigidbody2D rb; // variável do rigidbody, se não sabe o que é, é melhor não mexer pois pode dar problemas nas físicas.
    public Transform groundCheckPos; //verificar se há chão a diante
    public LayerMask groundLayer; // mascara de camada do chão

    public Transform playerLocation; //localização do player

    private float distToPlayer; //distancia entre o player e o inimigo

    public Collider2D bodyCollider; //hitbox "invisivel" para detectar paredes
    public Collider2D footCollider; // hitbox real do inimigo
    public Transform firePoint;
    
    void Start()
    {
        isChasing = false;
        mustPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(mustPatrol)
        {
            if(footCollider.IsTouchingLayers(groundLayer)) Patrol(); //só começa a patrulhar se estiver pisando em algum chão
            
        }

        distToPlayer = Vector2.Distance(transform.position, playerLocation.position);

        if(distToPlayer <= range)
        {   
            if(playerLocation.position.x > transform.position.x && transform.localScale.x < 0 || playerLocation.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }

            Attack();
        }else
        {
            mustPatrol = true;
            isChasing = false;
        }
    }

    private void FixedUpdate()
    {
        if(mustPatrol && footCollider.IsTouchingLayers(groundLayer))
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }

    void Patrol()
    {
        if(mustTurn || bodyCollider.IsTouchingLayers(groundLayer))//virar quando encostar em uma parede
        {
            Flip();
        }

        rb.velocity = new Vector2(speed, rb.velocity.y);// não mude nada aqui, a velocidade pode ser mudada no inspecionar 
    }

    void Attack()
    {
        mustPatrol = false;
        if(this.tag == "Melee") Chase(); // verifica o tipo do inimigo
        if(this.tag == "Shooter") Shoot(); // verifica o tipo do inimigo
    }

    void Chase() // ativa o modo de perseguição para inimigos corpo a corpo
    {
        var attackSpeed = 2; //coloque a velocidade do ataque aqui, quanto maior mais tempo demorará para atacar
        mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);

        if(distToPlayer <= attackRange)
        {
            canAttack = true;
        }else
        {
            canAttack = false;
        }

        if(canAttack)
        {
            meleeSpeed += Time.deltaTime;
            if(meleeSpeed >= attackSpeed)
            {
                meleeSpeed = 0;
                player.life -= meleeDamage;
            }
        }else
        {
            meleeSpeed = 0;
        }

        
        isChasing = true;
        if(!mustTurn)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);// não mude nada aqui, a velocidade pode ser mudada no inspecionar 
        
        }else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void Shoot() //ativa o modo de tiro para inimigos de longo alcance
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        mustPatrol = false;
        var attackSpeed = 2; //coloque a velocidade do ataque aqui, quanto maior mais tempo demorará para atacar

        if(distToPlayer <= range)
        {
            canAttack = true;
        }else
        {
            canAttack = false;
        }

        if(canAttack)
        {
            shooterSpeed += Time.deltaTime;
            if(shooterSpeed >= attackSpeed)
            {
                var bulletPrefab = Resources.Load("Projectiles/EnemyBullet");
                shooterSpeed = 0;
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                
            }

        }else
        {
            shooterSpeed = 0;
        }
    }

    void Flip() //não recomendo alterar nada aqui, esse é o método que vira o inimigo.
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x*-1 ,transform.localScale.y);
        speed *= -1;
        mustPatrol = true;

    }

    private void OnDrawGizmosSelected() 
    {
        //debugs
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        //##################################################
    }

    public void TakeDamage(float damage)
    {
        life -= damage;

        if(life <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
