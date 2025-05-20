using UnityEngine;

public class DestroyerZone : MonoBehaviour
{
    // El tag que debe tener el objeto para que sea destruido (por ejemplo: "Block")
    [SerializeField] private string tagID;

    // Este método se llama automáticamente cuando otro objeto colisiona con este
    private void OnCollisionEnter(Collision collision)
    {
        // Si el objeto que colisionó tiene el tag especificado...
        if (collision.gameObject.CompareTag(tagID))
        {
            // ...destruimos ese objeto
            Destroy(collision.gameObject);
        }
    }
}
