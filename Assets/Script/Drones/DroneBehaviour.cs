using UnityEngine;

public class DroneBehaviour : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 direction;

    public void InitializeDirection(float xPosition)
    {
        direction = xPosition < 0 ? Vector3.right : Vector3.left;
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // Destruir si se sale de la pantalla
        if (Mathf.Abs(transform.position.x) > 30f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Untagged") && other.transform.parent != null)
        {
            // Guardamos referencia al padre antes de destruir el objeto
            Transform parent = other.transform.parent;

            // Destruimos el bloque y el drone
            Destroy(other.gameObject);
            Destroy(gameObject);

            // Buscar el controlador de plataforma en algún padre
            BasePlatformControl platform = parent.GetComponentInParent<BasePlatformControl>();
            if (platform != null)
            {
                platform.UpdatePlatformCollider(); // Actualiza el BoxCollider
            }
        }
    }

}
