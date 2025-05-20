using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    // Si es verdadero, se cargan los datos guardados del puntaje al iniciar
    [SerializeField] private bool loadDataOnAwake = false;

    // Texto que se muestra antes del valor numérico (por ejemplo, "Score: ")
    [SerializeField] private string prevText = "Score: ";

    // Referencia a la variable de tipo entero que almacena el puntaje
    [SerializeField] private IntVariable intVariable;

    // Referencia al componente de texto (TextMeshProUGUI)
    private TextMeshProUGUI scoreLabel;

    private void Awake()
    {
        // Obtenemos el componente de texto (debe estar en el mismo GameObject)
        scoreLabel = GetComponent<TextMeshProUGUI>();

        // Si se desea cargar los datos previamente guardados, se hace ahora
        if (loadDataOnAwake) intVariable.LoadData();

        // Se actualiza el texto de la UI con el valor actual de la variable
        scoreLabel.text = $"{prevText}{intVariable.GetValue()}";

        // Nos suscribimos al evento para actualizar la UI cuando cambie el valor
        intVariable.onValueChange += OnValueChnage;
    }

    private void OnDestroy()
    {
        // Buenas prácticas: cancelamos la suscripción al evento cuando se destruye el objeto
        intVariable.onValueChange -= OnValueChnage;
    }

    // Este método se llama automáticamente cuando el valor de la variable cambia
    private void OnValueChnage(int value)
    {
        // Se actualiza el texto en pantalla
        scoreLabel.text = $"{prevText}{value}";
    }
}
