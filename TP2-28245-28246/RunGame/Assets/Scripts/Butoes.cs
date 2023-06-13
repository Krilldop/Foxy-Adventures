using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Butoes : MonoBehaviour
{
    public void ReiniciarJogo()
    {
        SceneManager.LoadScene("Nivel");
    }

    public void SairJogo()
    {
        Application.Quit();
    }
}
