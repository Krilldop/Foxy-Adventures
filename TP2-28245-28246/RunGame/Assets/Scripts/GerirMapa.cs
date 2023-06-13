using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerirMapa : MonoBehaviour
{
    [SerializeField] GameObject[] mapaPrefabs;
    [SerializeField] float zSpawn = 0;
    [SerializeField] float tamanhoPiso = 20;
    [SerializeField] int numeroPisos = 6;
    [SerializeField] Transform transformJogador;
    private List<GameObject> pisosAtivos = new List<GameObject>();

    void Start()
    {
        for(int i = 0; i < numeroPisos; i++)
        {
            if (i == 0)
            {
                InserirPiso(0);
            }
            else
            {
                InserirPiso(Random.Range(0, mapaPrefabs.Length));
            }
        }
    }


    void Update()
    {
        if(transformJogador.position.z - 30 > zSpawn - (numeroPisos * tamanhoPiso))
        {
            InserirPiso(Random.Range(0, mapaPrefabs.Length));
            ApagarPiso();
        }
    }

    private void InserirPiso(int pisoIndex)
    {
        GameObject go = Instantiate(mapaPrefabs[pisoIndex], transform.forward * zSpawn, transform.rotation);
        pisosAtivos.Add(go);
        zSpawn += tamanhoPiso;
    }

    private void ApagarPiso()
    {
        Destroy(pisosAtivos[0]);
        pisosAtivos.RemoveAt(0);
    }
}
