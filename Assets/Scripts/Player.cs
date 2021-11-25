using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //VARIÁVEIS DECLARADAS
    private Rigidbody2D rb; // variável de física

    public float speed; // velocidade

    public float life; // vida do player

    public float JumpForce; // potencia do pulo
    private float jumpCd;  // variavel de segurança para não poder pular no ar
    private bool jumpIsCd; // variavel para verificar se o pulo tá em cooldown

    private float rayDistance = 1f; // distancia do raio da bala

    public LayerMask whatIsGround;  // encontrar tudo que é chão (ESTÁ NA LAYER GROUND)

    public Transform firePoint; // componente de localizaçao da bala

    private Vector3 scaleChange;


    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>(); //encontrar rigidbody
    }
    void Start()
    {
        jumpCd = 0f; // variavel de segurança para o pulo
    }

    void Update()
    {
        DontDestroyOnLoad(this.gameObject);
        //scaleChange = new Vector3(0.1f, 0.1f, 0.1f); //correcao da escala do personagem
        //this.gameObject.GetComponent<Transform>().localScale = scaleChange;
        // reseta o pulo
        JumpReset();
        // debug visual
        if(IsGrounded()) Debug.DrawRay (new Vector2(transform.position.x - 0.5f,transform.position.y - 0.51f), Vector3.right * 1, Color.green);
        if(!IsGrounded()) Debug.DrawRay (new Vector2(transform.position.x - 0.5f,transform.position.y - 0.51f), Vector3.right * 1, Color.red);

        // função de atirar, se o jogador clicar com o botao esquerdo, atira
        if(Input.GetMouseButtonDown(0))
        {
            Shoot(); //atirar
        }
    }
    
    private void FixedUpdate() 
    {
        // chama a função MOVIMENTO
        Move();

        //PULO
        // ao clicar barra de espaço, ocorre o pulo
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(IsGrounded()) Jump(); // identifica se o asset é do layer Ground, pula
        }
    }

    //MOVIMENTO
    void Move()
    {    
        // a variavel x é o eixo horizontal
       float x = Input.GetAxis("Horizontal");

       // determina quando o jogador vai para direita ou esquerda 
       if(x < 0) transform.localScale = new Vector2( -1 ,transform.localScale.y);
       if(x > 0) transform.localScale = new Vector2( 1 ,transform.localScale.y);

       // calculo da variavel velocidade
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

    //TIRO DO PERSONAGEM
    void Shoot()
    {
        // prefab da bala carrega o asset da bala na pasta
        var bulletPrefab = Resources.Load("Projectiles/Bullet");
        // instancia a bala, a posicao e a rotacao da bala
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    //VARIAVEL PARA VERIFICAR O CHAO
    private bool IsGrounded()
    {
           return Physics2D.Raycast(new Vector2(transform.position.x - 0.5f,transform.position.y - 0.51f), Vector2.right, rayDistance, whatIsGround.value);
    }

    //DANOS RECEBIDOS
    public void TakeDamage(float damage)
    {
        // retira vida quando recebe um tiro
        life -= damage;

        //condicao para a morte do personagem
        if (life <= 0)
        {
            Die();
        }
    }

    //MORRER
    void Die()
    {
        //condicao para a morte do personagem
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene("GameOver");
    }
}