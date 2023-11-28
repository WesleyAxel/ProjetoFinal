﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel {

    public ControlaInterface controlaInterface;
    public AudioClip somDano;
    public Status statusJogador;

    public int LevelJogador = 1;
    private Vector3 direcao;
    private MovimentaJogador movimentaJogador;
    private AnimaPersonagem animaJogador;
    private bool jaFezMudancaDeLevel = false;

    private void Start() {
        movimentaJogador = GetComponent<MovimentaJogador>();
        animaJogador = GetComponent<AnimaPersonagem>();
        statusJogador = GetComponent<Status>();
    }

    private void Update() {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");
        direcao = new Vector3(eixoX, 0, eixoZ);
        animaJogador.Movimentar(direcao.magnitude);
        checaLevelUp();
    }

    private void FixedUpdate() {
        movimentaJogador.Movimentar(direcao, statusJogador.velocidade);
        movimentaJogador.RotacionarJogador();
    }

    public void TomarDano(int dano) {
        statusJogador.vida -= dano;
        controlaInterface.AtualizaSliderVida();
        ControlaAudio.instancia.PlayOneShot(somDano);

        if (statusJogador.vida <= 0) {
            Morrer();
        }
    }

    public void Morrer() {
        controlaInterface.GameOver();
    }

    public void CurarVida(int quantidadeDeCura) {
        statusJogador.vida += quantidadeDeCura;

        if (statusJogador.vida > statusJogador.vidaInicial) {
            statusJogador.vida = statusJogador.vidaInicial;
        }

        controlaInterface.AtualizaSliderVida();
    }

    private void checaLevelUp()
    {
        int quantidadeMortos = controlaInterface.getQuantidadeMortos();

        if (!jaFezMudancaDeLevel && quantidadeMortos % 2 == 0 && quantidadeMortos != 0)
        {
            levelUP();
            jaFezMudancaDeLevel = true;
            controlaInterface.AtualizaLevelJogador();
        }
        else if (quantidadeMortos % 2 != 0)
        {
            jaFezMudancaDeLevel = false; // Permite fazer a mudança de level novamente
        }
    }

    private void levelUP()
    {
        LevelJogador = LevelJogador + 1;
        statusJogador.uparVida(20);
        CurarVida((int)statusJogador.vidaInicial);
    }
}
