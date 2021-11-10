using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //variáveis declaradas
    private Rigidbody2D rb; // variavel de física

    public float speed; // velocidade

    public float life; //vida do player

    public float JumpForce; //potencia do pulo
    private float jumpCd;  // variavel de segurança para não poder pular no ar
    private bool jumpIsCd; // variavel para verificar se o pulo está em cooldown

    private float rayDistance = 1f;

    public LayerMask whatIsGround;  //encontrar tudo que é chão (ESTÁ NA LAYER GROUND)

    public Transform firePoint;

    
	 
    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>(); //encontrar rigidbody
    }
    void Start()
    {
        jumpCd = 0f;
    }

    // Update is called once per frame
    void Update()
    {   
        
        JumpReset();
        //isso aqui é apenas um debug visual, não altera nada no jogo
        if(IsGrounded()) Debug.DrawRay (new Vector2(transform.position.x - 0.5f,transform.position.y - 0.51f), Vector3.right * 1, Color.green);
        if(!IsGrounded()) Debug.DrawRay (new Vector2(transform.position.x - 0.5f,transform.position.y - 0.51f), Vector3.right * 1, Color.red);
        //##############################################################

        if(Input.GetMouseButtonDown(0))
        {
            Shoot(); //atirar
        }
    }
    
    private void FixedUpdate() 
    {
        //chamar movimento
        Move();
        //chamar pulo
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(IsGrounded()) Jump();
        }
    }

    void Move()
    {    //movimento
       float x = Input.GetAxis("Horizontal");

       if(x < 0) transform.localScale = new Vector2( -1 ,transform.localScale.y);
       if(x > 0) transform.localScale = new Vector2( 1 ,transform.localScale.y);
        rb.velocity = new Vector2(speed * x,rb.velocity.y);
    }

    #region JUMP
    void Jump()
    {	
	//pulo
        rb.AddForce(Vector2.up * JumpForce);
        jumpIsCd = true;
    }

    private bool JumpCd()
    {	
	//controle do cooldown
        if(jumpCd == 0)return true; else return false;
    }
    
    void JumpReset()
    {

	//resetar pulo
        if(jumpIsCd)
        {
            jumpCd += Time.deltaTime;
        }

        if(jumpCd > 1) 
        {
            jumpCd = 0;
            jumpIsCd = false;
        }
    }
    #endregion

    void Shoot()
    {
        var bulletPrefab = Resources.Load("Projectiles/Bullet");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    private bool IsGrounded()
    {
       return Physics2D.Raycast(new Vector2(transform.position.x - 0.5f,transform.position.y - 0.51f), Vector2.right, rayDistance, whatIsGround.value);
       /*return Physics2D.Raycast(new Vector2(transform.position.x - (transform.position.x/2),transform.position.y), Vector2.down, rayDistance, whatIsGround.value);
       return Physics2D.Raycast(new Vector2(transform.position.x + (transform.position.x/2),transform.position.y), Vector2.down, rayDistance, whatIsGround.value);
       */
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
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
}
