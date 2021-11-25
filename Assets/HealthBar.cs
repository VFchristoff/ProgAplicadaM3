using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Image barra; //sprite da barra de vida
    public float vidaAtualjogador; //vida atual do jogador
    public float totalVida = 100f; //total de vida inicial 100% da barra
    public GameObject jogador;

    public void Start()
    {
        barra = this.GetComponent<Image>(); //associa a imagem da barra
    }

    public void Update()
    {
        DontDestroyOnLoad(this.gameObject);
        //armazena a vida atual do jogador e calcula a barra de acordo
        ////com a quantidade de vida
        vidaAtualjogador = jogador.GetComponent<Player>().life;
        barra.fillAmount = vidaAtualjogador / totalVida;
        if (jogador == null)
        {
            Destroy(this.gameObject);
        }
    }

}
