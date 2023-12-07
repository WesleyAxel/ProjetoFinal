using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{

    public GameObject jogadorPrefab;

   
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se o objeto colidido � o prefab do jogador
        if (collision.gameObject.CompareTag("Jogador") && collision.gameObject == jogadorPrefab)
        {
            // Executa o comando desejado
            ExecutarComando();
        }
    }

    private void ExecutarComando()
    {
        SceneManager.LoadScene("Cena_01_Transicao");
    }

}
