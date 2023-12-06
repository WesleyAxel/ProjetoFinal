using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour {

    public Slider sliderVidaJogador;
    public GameObject painelGameOver;
    public Text textoTempoSobrevivencia;
    public Text textoSobrevivenciaMax;
    public Text textoQuantidadeZumbiMortos;
    public Text textoLevelJogador;
    public Text textoChefeAparece;
    public Text textoLevelUp;
    public Image fundoInventario;
    public AudioClip levelUpClip;

    private ControlaJogador controlaJogador;
    private float pontuacaoMaxima;
    private int quantidadeDeZumbisMortos = 0;
    private int levelJogador = 0;

    private void Start() {
        Time.timeScale = 1;
        controlaJogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();
        sliderVidaJogador.maxValue = controlaJogador.statusJogador.vida;
        AtualizaSliderVida();
        AtualizaLevelJogador();
        pontuacaoMaxima = PlayerPrefs.GetFloat("pontuacaoMaxima");
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            textoLevelJogador.gameObject.SetActive(true);
            fundoInventario.gameObject.SetActive(true);
            textoQuantidadeZumbiMortos.gameObject.SetActive(false);
            sliderVidaJogador.gameObject.SetActive(false);

        }
    
        if (Input.GetKeyUp(KeyCode.I)) 
        {
            textoLevelJogador.gameObject.SetActive(false);
            fundoInventario.gameObject.SetActive(false);
            textoQuantidadeZumbiMortos.gameObject.SetActive(true);
            sliderVidaJogador.gameObject.SetActive(true);
        }
       
    }

    public void AtualizaSliderVida() {
        sliderVidaJogador.value = controlaJogador.statusJogador.vida;
    }

    public void AtualizarQuantidadeDeZumbisMortos() {
        quantidadeDeZumbisMortos++;
        textoQuantidadeZumbiMortos.text = string.Format("x {0}", quantidadeDeZumbisMortos);
    }

    public void AtualizaLevelJogador() {
        levelJogador++;
        textoLevelJogador.text = string.Format("Alex Shadow - Level: {0}", levelJogador);
        if(levelJogador != 1)
        {
            StartCoroutine(DesabilitarTextoLevelUp(3f));
            ControlaAudio.instancia.PlayOneShot(levelUpClip);
        }
    }

    public void GameOver() {
        Time.timeScale = 0;
        painelGameOver.SetActive(true);
        
        int minutos = (int)Time.timeSinceLevelLoad / 60;
        int segundos = (int)Time.timeSinceLevelLoad % 60;
        textoTempoSobrevivencia.text = "Você sobreviveu por " + minutos + "min e " + segundos + "s";

        AjustarPontuacaoMaxima(Time.timeSinceLevelLoad);
    }

    public void Reiniciar() {
        SceneManager.LoadScene("game");
    }

    private void AjustarPontuacaoMaxima(float tempoAtual) {
        if (tempoAtual > pontuacaoMaxima) {
            PlayerPrefs.SetFloat("pontuacaoMaxima", tempoAtual);
            pontuacaoMaxima = tempoAtual;
        }

        int minutos = (int)pontuacaoMaxima / 60;
        int segundos = (int)pontuacaoMaxima % 60;
        textoSobrevivenciaMax.text = string.Format("Seu melhor tempo é {0}min e {1}s", minutos, segundos);
    }

    public void AparecerTextoChefeCriado() {
        StartCoroutine(DesaparecerTexto(2, textoChefeAparece));
    }

    private IEnumerator DesabilitarTextoLevelUp(float tempo)
    {
        textoLevelUp.gameObject.SetActive(true);
        yield return new WaitForSeconds(tempo);
        textoLevelUp.gameObject.SetActive(false);
    }

    private IEnumerator DesaparecerTexto(float tempoSumir, Text texto) {
        texto.gameObject.SetActive(true);
        Color corTexto = texto.color;
        corTexto.a = 1;
        texto.color = corTexto;
        yield return new WaitForSeconds(tempoSumir);
        float contador = 0;
        while (texto.color.a > 0) {
            contador += Time.deltaTime / tempoSumir;
            corTexto.a = Mathf.Lerp(1, 0, contador);
            texto.color = corTexto;
            if (texto.color.a <= 0) {
                texto.gameObject.SetActive(false);
            }
            yield return null;
        }
    }

    public int getQuantidadeMortos()
    {
        return quantidadeDeZumbisMortos;
    }

}
