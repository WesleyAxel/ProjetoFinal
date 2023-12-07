using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaCamera : MonoBehaviour {

    public GameObject jogador;


    private GameObject skin1;
    private GameObject skin2;
    private GameObject skin3;
    private GameObject skin4;
    private int skinPersonagem; 

    private Vector3 distanciaCompensar;

    private void Start() {
        distanciaCompensar = transform.position - jogador.transform.position;
        skinPersonagem = PlayerPrefs.GetInt("skinjogador");
        skin1 = jogador.transform.Find("Personagem_SobreviventeExperiente").gameObject;
        skin2 = jogador.transform.Find("Personagem_Cacador").gameObject;
        skin3 = jogador.transform.Find("Personagem_Alex_Shadow").gameObject;
        skin4 = jogador.transform.Find("Personagem_HomemSobrevivente01").gameObject;

        if (skinPersonagem == 1)
        {
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
        }

    }
    
    private void Update() {
        transform.position = jogador.transform.position + distanciaCompensar;
    }
}
