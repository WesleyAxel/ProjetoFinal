using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour
{
    public GameObject bala;
    public GameObject canoArma;
    public AudioClip somTiro;

    private ControlaJogador controlaJogador;
    private float tempoProximoTiro;
    private float intervaloTiro;

    private void Start()
    {
        controlaJogador = GetComponent<ControlaJogador>();
        intervaloTiro = 2.5f;
        tempoProximoTiro = Time.time;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= tempoProximoTiro)
        {
            if (controlaJogador != null && controlaJogador.LevelJogador >= 3)
            {
                // Instancia duas balas na diagonal
                Instantiate(bala, canoArma.transform.position, Quaternion.Euler(canoArma.transform.rotation.eulerAngles + new Vector3(0, 5, 0)));
                Instantiate(bala, canoArma.transform.position, Quaternion.Euler(canoArma.transform.rotation.eulerAngles + new Vector3(0, -5, 0)));
                ControlaAudio.instancia.PlayOneShot(somTiro);
            }
            else
            {
                // Instancia uma única bala
                Instantiate(bala, canoArma.transform.position, canoArma.transform.rotation);
                ControlaAudio.instancia.PlayOneShot(somTiro);
            }

            tempoProximoTiro = Time.time + intervaloTiro;
            AtualizarIntervaloTiro();
        }
    }

    private void AtualizarIntervaloTiro()
    {
        // Diminui o intervalo de tiro em 0.5 segundos a cada level, até um mínimo de 0.5 segundos
        if (controlaJogador != null)
        {
            float novoIntervalo = 2.5f - (controlaJogador.LevelJogador - 1) * 0.5f;
            intervaloTiro = Mathf.Max(0.5f, novoIntervalo);
        }
    }
}