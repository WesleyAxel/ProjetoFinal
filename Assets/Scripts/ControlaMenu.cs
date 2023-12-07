using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaMenu : MonoBehaviour {

    public GameObject botaoSair;
    public GameObject botaoMudarPersonagem;
    public GameObject botaoVoltar;
    public GameObject botaoJogar;
    public GameObject jogadorPrefab;


    private GameObject skin1;
    private GameObject skin2;
    private GameObject skin3;
    private GameObject skin4;


    private int skinPersonagem = 0;

    private void Start() {
        #if UNITY_STANDALONE || UNITY_EDITOR
        botaoSair.SetActive(true);
        #endif
        skin1 = jogadorPrefab.transform.Find("Personagem_SobreviventeExperiente").gameObject;
        skin2 = jogadorPrefab.transform.Find("Personagem_Cacador").gameObject;
        skin3 = jogadorPrefab.transform.Find("Personagem_Alex_Shadow").gameObject;
        skin4 = jogadorPrefab.transform.Find("Personagem_HomemSobrevivente01").gameObject;
    }

    public void Jogar() {
        //StartCoroutine(MudarCena("Cena_01"));
        PlayerPrefs.SetInt("skinjogador", skinPersonagem);
        PlayerPrefs.SetInt("level", 0);
        SceneManager.LoadScene("Cena_01");
    }

    public void MudarPersonagem()
    {
        botaoSair.SetActive(false);
        botaoJogar.SetActive(false);
        botaoMudarPersonagem.SetActive(true);
        botaoVoltar.SetActive(true);
    }

    public void MudarSkinPersonagem()
    {
        //Checar botão clicado
        skinPersonagem++;
        if (skinPersonagem == 1) {
            skin1.SetActive(false);
            skin2.SetActive(true);
        }

        else if (skinPersonagem == 2)
        {
            skin2.SetActive(false);
            skin3.SetActive(true);
        }

        else if (skinPersonagem == 3)
        {
            skin3.SetActive(false);
            skin4.SetActive(true);
        }

        else if (skinPersonagem == 4)
        {
            skin4.SetActive(false);
            skin1.SetActive(true);
            skinPersonagem = 0;
        }
    }



    public void VoltarPersonagem()
    {
        botaoSair.SetActive(true);
        botaoJogar.SetActive(true);
        botaoMudarPersonagem.SetActive(false);
        botaoVoltar.SetActive(false);
    }


    private IEnumerator MudarCena(string name) {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(name);
    }

    public void Sair() {
        StartCoroutine(SairJogo());
    }

    private IEnumerator SairJogo() {
        yield return new WaitForSeconds(0.3f);
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
