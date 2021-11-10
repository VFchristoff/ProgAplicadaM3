using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //posicoes para qual a plataforma vai se movimentar
    public Transform posicao1, posicao2;
    //velocidade que a plataforma vai se mover
    public float velocidade;
    //posicao inicial da plataforma
    public Transform posicaoInicial;
    //vetor da prox posicao
    Vector3 proximaPosicao;

    private void Start()
    {
        //
        proximaPosicao = posicaoInicial.position;
    }

    private void Update()
    {
        if(transform.position == posicao1.position)
        {
            proximaPosicao = posicao2.position;
        } 
        
        if (transform.position == posicao2.position)
        {
            proximaPosicao = posicao1.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, proximaPosicao, velocidade * Time.deltaTime);
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(posicao1.position, posicao2.position);
    }

}
