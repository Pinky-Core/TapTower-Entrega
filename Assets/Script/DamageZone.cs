using UnityEngine;

public class DamageZone : MonoBehaviour
{
    // El tag que debe tener el objeto para que reciba daño (por ejemplo: "Player")
    [SerializeField] private string tagID;

    // Referencia a una variable entera ScriptableObject que representa las vidas del jugador
    [SerializeField] private IntVariable lives;

    // Se ejecuta cuando otro collider entra en este trigger
    private void OnTriggerEnter(Collider other)
    {
        // Si el objeto que entró tiene el tag correcto...
        if (other.CompareTag(tagID))
        {
            // ...restamos una vida (valor negativo)
            lives.AddValue(-1);
        }
    }
}
