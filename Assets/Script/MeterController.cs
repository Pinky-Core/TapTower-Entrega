using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class MeterController : MonoBehaviour
{
    [SerializeField] private IntVariable scoreVariable;       // Variable para la puntuaci�n actual (altura en metros)
    [SerializeField] private IntVariable highScoreVariable;   // Variable para guardar el r�cord
    [SerializeField] private Transform pivot, buildingsContainer; // Pivot (parte superior de la l�nea) y contenedor de edificios
    [SerializeField] private TextMeshPro meterLabel;          // Texto para mostrar la altura

    private LineRenderer lineRenderer; // L�nea que une el origen con la cima
    private int childAmount;           // Cantidad de edificios colocados actualmente

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>(); // Obtiene el LineRenderer del objeto
        lineRenderer.positionCount = 2;              // Necesitamos dos puntos para una l�nea
        meterLabel.text = "";                        // Inicia el texto vac�o
    }

    private void LateUpdate()
    {
        // Si la cantidad de edificios cambi�, actualizar
        if (childAmount != buildingsContainer.childCount)
        {
            childAmount = buildingsContainer.childCount;

            // Fijar el primer punto de la l�nea en la base (este objeto)
            lineRenderer.SetPosition(0, transform.position);

            // Iniciar animaci�n hacia el nuevo edificio en la cima
            UpdatePositionTask(1, buildingsContainer.GetChild(childAmount - 1).transform.position);
        }

        // Si la puntuaci�n actual supera el r�cord, actualizar el r�cord
        if (scoreVariable.GetValue() > highScoreVariable.GetValue())
            highScoreVariable.SetValue(scoreVariable.GetValue());
    }

    // Corrutina as�ncrona que mueve suavemente el pivot hacia la cima de la torre
    private async void UpdatePositionTask(float duration, Vector3 endValue)
    {
        float time = 0;

        Vector3 startValue = pivot.position;

        // Bloqueamos X y Z para que solo se mueva en Y (altura)
        endValue.x = startValue.x;
        endValue.z = startValue.z;

        while (time < duration)
        {
            // Interpolaci�n suave del movimiento del pivot
            pivot.position = Vector3.Lerp(startValue, endValue, time / duration);

            // Actualizamos el segundo punto de la l�nea
            lineRenderer.SetPosition(1, pivot.position);

            // Convertimos la altura en metros y la asignamos al score
            scoreVariable.SetValue(Mathf.RoundToInt(Mathf.Lerp(scoreVariable.GetValue(), endValue.y * 100, time / duration)));

            // Mostramos el texto en metros
            meterLabel.text = $"{scoreVariable.GetValue():0}mts";

            time += Time.deltaTime;
            await Task.Yield(); // Espera al siguiente frame
        }

        // Al finalizar, fijamos las posiciones
        pivot.position = endValue;
        lineRenderer.SetPosition(1, endValue);
    }
}
