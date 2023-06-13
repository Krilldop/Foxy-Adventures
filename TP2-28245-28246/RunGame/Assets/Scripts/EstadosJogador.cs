using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EstadosJogador : MonoBehaviour
{
    public static bool fimJogo;
    [SerializeField] GameObject painelFimJogo;
    public static bool jogoComecou;
    [SerializeField] GameObject textoInicial;
    public static int numeroMoedas;
    [SerializeField] Text textoNumeroMoedas;

    void Start()
    {
        Time.timeScale = 1;
        fimJogo = false;
        jogoComecou = false;
        numeroMoedas = 0;
    }

    void Update()
    {
        if(fimJogo)
        {
            Time.timeScale = 0;
            painelFimJogo.SetActive(true);
        }

        textoNumeroMoedas.text = "Moedas: " + numeroMoedas;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            jogoComecou = true;
            Destroy(textoInicial);
        }
    }
}
