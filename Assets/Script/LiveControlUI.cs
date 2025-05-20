using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Controla la UI de vidas del jugador usando toggles (por ejemplo, corazones)
public class LiveControlUI : MonoBehaviour
{
    [SerializeField] private IntVariable lives;       // Referencia al valor compartido de vidas (ScriptableObject)
    [SerializeField] private int maxLives = 3;         // Vidas máximas al empezar
    [SerializeField] private Toggle heartPref;         // Prefab de un corazón (Toggle)

    private List<Toggle> hearts = new List<Toggle>();  // Lista de corazones instanciados en la UI

    public UnityEvent onGameOver;                      // Evento que se dispara al quedarse sin vidas

    private void Awake()
    {
        heartPref.isOn = true;                         // Asegura que el prefab esté encendido por defecto
        lives.SetValue(maxLives);                      // Inicializa el valor de vidas

        // Crea tantos corazones (toggles) como vidas máximas y los agrega a la lista
        for (int i = 0; i < lives.GetValue(); i++)
        {
            hearts.Add(Instantiate<Toggle>(heartPref, transform));
        }

        // Se suscribe al evento que se dispara cuando cambian las vidas
        lives.onValueChange += OnValueChange;
    }

    private void OnDestroy()
    {
        // Al destruir el objeto, se desuscribe para evitar errores
        lives.onValueChange -= OnValueChange;
    }

    // Este método se ejecuta cada vez que cambian las vidas
    private void OnValueChange(int value)
    {
        // Activa o desactiva los corazones dependiendo del valor actual de vidas
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].isOn = i < value;
        }

        // Si las vidas llegan a cero o menos, lanza el evento de Game Over
        if (value <= 0)
            onGameOver?.Invoke();

        // Desactiva el script (ya no se necesita seguir actualizando)
        this.enabled = false;
    }
}
