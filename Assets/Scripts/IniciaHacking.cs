using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IniciaHacking : MonoBehaviour
{

    public Text textoObjetivo;
    public Text textoIniciaHacking;
    public AudioSource audioMusica;
    public AudioSource audioHacking;
    public int timer = 120;

    private GameObject[] spawns;
    private Boolean started = false;
    
    private string textoBase;
    private int loading = 0;
    private float counter = 0;


    // Start is called before the first frame update
    void Start()
    {
     textoIniciaHacking.gameObject.SetActive(false);   
     textoObjetivo.gameObject.SetActive(false);   

     textoBase = textoIniciaHacking.text;

     MostraObjetivo(0);
    }

    // Update is called once per frame
    void Update()
    {
        ChecaEAtualizaCarregamento();
    }

    private void IniciaGeradores()
    {
        spawns ??= GameObject.FindGameObjectsWithTag("Gerador");

        foreach (GameObject spawn in spawns) {
            GeradorZumbis gerador = spawn.GetComponent<GeradorZumbis>();
            gerador.AtualizarEstado(true);
        }
    }

    private void MostraObjetivo(int etapa)
    {
        string textoObjetivoAtual = textoObjetivo.text;

        if (etapa == 1) {
            textoObjetivoAtual = "<color=red>SOBREVIVA</color>";
        }

        textoObjetivo.text = textoObjetivoAtual;
        
        StartCoroutine(DesaparecerTexto(5, textoObjetivo));
    }

    private void ChecaEAtualizaCarregamento() 
    {
        
        if (textoIniciaHacking.IsActive() && counter >= 1) {
            int currentLoading = Math.Min(loading * 100 / timer, 100);

            string loadingProgress = "[..........]";

            for (int i = 0; i < currentLoading / 10; i++) {
                int dotIndex = loadingProgress.IndexOf(".");

                loadingProgress = loadingProgress[..dotIndex] + "=" + loadingProgress[(dotIndex + 1)..];
            }

            string textoInicial = currentLoading == 100 ? "Hacking concluido" : textoBase;


            textoIniciaHacking.text = textoInicial + "\n" + loadingProgress + " " + currentLoading.ToString() + "%";
            
            loading += 1;
            counter = 0;
        }
        
        counter += Time.deltaTime;
    }

    private IEnumerator DesaparecerTexto(float tempoSumir, Text texto) {
        texto.gameObject.SetActive(true);
        
        Color corTexto = texto.color;
        corTexto.a = 1;
        texto.color = corTexto;
        
        yield return new WaitForSeconds(tempoSumir);
        
        texto.gameObject.SetActive(false);        

        // Carrega nova cena
        // SceneManager.LoadScene()
    }

    // Controle da música e tempo de execução
    private IEnumerator TocarMusica(float tempoSumir, AudioSource audio) {
        audio = audio.GetComponent<AudioSource>();
        audio.Play();
        
        yield return new WaitForSeconds(tempoSumir);
        
        audio.Stop();
    }

    private void ExecutarEvento() 
    {
        MostraObjetivo(1);
        IniciaGeradores();
        StartCoroutine(DesaparecerTexto(timer + 5, textoIniciaHacking));
        StartCoroutine(TocarMusica(timer + 5, audioMusica));
        StartCoroutine(TocarMusica(timer + 5, audioHacking));
        started = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Jogador encontra objetivo
        if (!started && collision.gameObject.CompareTag("Jogador")) {
            ExecutarEvento();
        }
    }
}
