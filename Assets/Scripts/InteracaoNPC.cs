using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InteracaoNPC : MonoBehaviour
{
    public float interactionDistance = 3f;
    public KeyCode interactionKey = KeyCode.E;

    private GameObject currentNPC;

    public Text textoNPC;
    public Image fundoNPC;
    private bool isInteracting;

    void Update()
    {
        if (Input.GetKeyDown(interactionKey) && !isInteracting)
        {
            if (currentNPC != null)
            {
                isInteracting = true;
                //fazer com que o jogador não se movimente mais
                textoNPC.gameObject.SetActive(true);
                fundoNPC.gameObject.SetActive(true);
            }
        }
        else if (Input.GetKeyDown(interactionKey) && isInteracting)
        {
            isInteracting = false;
            textoNPC.gameObject.SetActive(false);
            fundoNPC.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            currentNPC = other.gameObject;
            Debug.Log("NPC próximo: " + currentNPC.name);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC") && other.gameObject == currentNPC)
        {
            currentNPC = null;
            isInteracting = false;
            textoNPC.gameObject.SetActive(false);
            fundoNPC.gameObject.SetActive(false);
            Debug.Log("Saindo da interação com o NPC");
        }
    }
}