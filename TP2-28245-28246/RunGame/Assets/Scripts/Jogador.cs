using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using System.Collections;

public class JogadorControlos : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 direcao;
    [SerializeField] float velocidade;
    private int linha = 1;
    [SerializeField] float distancia = 3;
    [SerializeField] float forcaSalto;
    [SerializeField] float gravidade = -20;
    [SerializeField] Animator animator;
    [SerializeField] float velocidadeMax;
    private bool validacaoDeslizar = false;
    [SerializeField] AudioSource audioSaltar;
    [SerializeField] AudioSource audioDeslizar;
    [SerializeField] AudioSource audioPerder;
    private AudioSource[] allAudioSources;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!EstadosJogador.jogoComecou)
        {
            return;
        }

        if (velocidade < velocidadeMax)
        {
            velocidade += 0.1f * Time.deltaTime;
        }

        animator.SetBool("jogoComecou", true);
        animator.SetBool("noChao", characterController.isGrounded);
        direcao.z = velocidade;
        direcao.y += gravidade * Time.deltaTime;
        Vector3 posicao = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (characterController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Salto();
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && !validacaoDeslizar)
        {
            StartCoroutine(Desliza());
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            linha++;
            if (linha == 3)
                linha = 2;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            linha--;
            if (linha == -1)
                linha = 0;
        }

        if (linha == 0)
        {
            posicao += Vector3.left * distancia;

        }
        else if (linha == 2)
        {
            posicao += Vector3.right * distancia;
        }

        if (transform.position == posicao)
            return;
        Vector3 diferenca = posicao - transform.position;
        Vector3 mover = diferenca.normalized * 25 * Time.fixedDeltaTime;
        if(mover.sqrMagnitude < diferenca.sqrMagnitude)
        {
            characterController.Move(mover);
        }
        else
        {
            characterController.Move(diferenca);
        }
    }

    private void FixedUpdate()
    {
        if (!EstadosJogador.jogoComecou)
        {
            return;
        }
        characterController.Move(direcao * Time.fixedDeltaTime);
    }

    private void Salto()
    {
        direcao.y = forcaSalto;
        audioSaltar.Play();
    }

    private IEnumerator Desliza()
    {
        animator.SetBool("deslizar", true);
        characterController.center = new Vector3(0, -0.5f, 0);
        characterController.height = 1;
        validacaoDeslizar = true;
        audioDeslizar.Play();

        yield return new WaitForSeconds(1f);

        animator.SetBool("deslizar", false);
        characterController.center = new Vector3(0, 0, 0);
        characterController.height = 2;
        validacaoDeslizar = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource audioS in allAudioSources)
            {
                audioS.Stop();
            }
            audioPerder.Play();
            EstadosJogador.fimJogo = true;
        }
    }
}
