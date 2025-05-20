using TMPro;
using UnityEngine;

// Asegura que el GameObject tenga un componente TextMeshProUGUI
[RequireComponent(typeof(TextMeshProUGUI))]
public class TextColorPingPong : MonoBehaviour
{
    // Gradiente que se usará para cambiar el color del texto
    [SerializeField] Gradient gradient;

    // Velocidad del efecto de cambio de color
    [SerializeField] private float pingpongSpeed = 1f;

    // Referencia al componente TextMeshProUGUI
    private TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        // Obtenemos el componente de texto cuando el objeto se activa
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Usamos PingPong para oscilar entre 0 y 1 continuamente con el tiempo
        // Evaluamos el gradiente con ese valor para obtener un color interpolado
        textMeshProUGUI.color = gradient.Evaluate(Mathf.PingPong(Time.time * pingpongSpeed, 1));
    }
}
