using UnityEngine;

public class DestroyerZone : MonoBehaviour
{
    // El tag que debe tener el objeto para que sea destruido (por ejemplo: "Block")
    [SerializeField] private string tagID;

    // Este m�todo se llama autom�ticamente cuando otro objeto colisiona con este
    private void OnCollisionEnter(Collision collision)
    {
        // Si el objeto que colision� tiene el tag especificado...
        if (collision.gameObject.CompareTag(tagID))
        {
            // ...destruimos ese objeto
            Destroy(collision.gameObject);
        }
    }
}
