using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moedas : MonoBehaviour
{
    [SerializeField] AudioSource audioMoeda;
    void Update()
    {
        transform.Rotate(40 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EstadosJogador.numeroMoedas += 1;
            audioMoeda.Play();
            Destroy(gameObject, 0.1f);
        }
    }
}
