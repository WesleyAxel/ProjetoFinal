using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteracaoNPC : MonoBehaviour
{
    public float interactionDistance = 3f;
    public KeyCode interactionKey = KeyCode.E; 

    private GameObject currentNPC; 
    private bool isInteracting; 

    void Update()
    {
        if (Input.GetKeyDown(interactionKey) && !isInteracting)
        {
            if (currentNPC != null)
            {
                isInteracting = true;
                // Exibir a caixa de diálogo 
            }
        }
        else if(Input.GetKeyDown(interactionKey) && isInteracting)
        {
            Debug.Log("NÃO");
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
            Debug.Log("Saindo da interação com o NPC");
        }
    }

}


