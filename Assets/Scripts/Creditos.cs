using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creditos : MonoBehaviour
{
    public float velocidadeScroll = 20f; // Velocidade de deslocamento do texto
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Move o texto para cima
        rectTransform.anchoredPosition += Vector2.up * velocidadeScroll * Time.deltaTime;
    }
}
