using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //CONTROLADOR DE CENAS

    //ao clicar no botao, vai para a fase 1
    public void OnClickPlay()
    {
        SceneManager.LoadScene("Fase1"); 
    }

    //ao clicar no botao, vai para o menu
    public void OnClickMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    //ao clicar no botao, sair do executavel
    public void OnClickExit()
    {
        Application.Quit();
    }
}
