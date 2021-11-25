using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    //objeto jogador
    public Player player;

    private void OnTriggerEnter2D(Collider2D other) //ao colidir no colisor
    {
        if(other.gameObject.tag == "Player") //procura a tag player
        {
            Destroy(this.gameObject); //destroi o item de vida ao colidir com o jogador
            player.life += 2; //dá vida para o jogador
        }
    }
}
