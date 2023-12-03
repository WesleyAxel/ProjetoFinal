using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IniciaHacking : MonoBehaviour
{

    public Text textoIniciaHacking;
    public int timer = 120;
    
    private string textoBase;
    private int loading = 0;
    private float counter = 0;

    

    // Start is called before the first frame update
    void Start()
    {
     textoIniciaHacking.gameObject.SetActive(false);   
     textoBase = textoIniciaHacking.text;
    }

    // Update is called once per frame
    void Update()
    {
        ChecaEAtualizaCarregamento();
    }

    private void ChecaEAtualizaCarregamento() {
        
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

    private void IniciaEvento() 
    {
        StartCoroutine(DesaparecerTexto(timer + 5, textoIniciaHacking));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Jogador")) {
            print("Colidiu!");

            IniciaEvento();
        }
    }
}
