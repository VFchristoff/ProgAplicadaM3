using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class script : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        SceneManager.LoadScene("Fase2");
    }
}
