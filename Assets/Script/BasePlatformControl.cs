using UnityEngine;
using UnityEngine.Events;

public class BasePlatformControl : MonoBehaviour
{
    // Referencia al BoxCollider del objeto que actúa como plataforma
    private BoxCollider boxCollider;

    // Evento que se dispara cuando un bloque llega a la plataforma, pasando su posición
    public UnityEvent<Vector3> onBuildingReachPlatform;

    private void Awake()
    {
        // Obtener el componente BoxCollider en el mismo GameObject
        boxCollider = GetComponent<BoxCollider>();
    }



    private void OnCollisionEnter(Collision collision)
    {
        // Si el punto de contacto no viene desde arriba (comparando con el eje Y de la plataforma), ignorar
        if (Vector3.Dot(collision.contacts[0].point, transform.up) <= 0) return;

        // Si el objeto que colisionó tiene un ParticleSystem, emitir 50 partículas
        if (collision.gameObject.TryGetComponent<ParticleSystem>(out ParticleSystem ps))
            ps.Emit(50);

        // Si el objeto que colisionó tiene un Rigidbody...
        if (collision.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
        {
            // Detener su movimiento
            rigidbody.isKinematic = true;

            // Convertir el bloque en hijo de la plataforma (se mantiene su posición global)
            rigidbody.transform.SetParent(transform, true);

            // Alinear su rotación con la plataforma
            rigidbody.transform.localRotation = Quaternion.Euler(Vector3.zero);

            // Calcular la nueva posición para mover el centro del BoxCollider
            Vector3 newPosition = rigidbody.transform.localPosition;
            newPosition.y += rigidbody.transform.localScale.y / 2;

            // Mover el centro del BoxCollider hacia el nuevo bloque
            boxCollider.center = newPosition;

            // Desmarcar el bloque para que no sea detectado por otros scripts que usen tags
            rigidbody.gameObject.tag = "Untagged";

            // Invocar el evento pasando la posición del último hijo (último bloque colocado)
            onBuildingReachPlatform?.Invoke(transform.GetChild(transform.childCount - 1).position);

            UpdatePlatformCollider();

        }
    }

    public void UpdatePlatformCollider()
    {
        if (transform.childCount == 0)
        {
            boxCollider.center = Vector3.zero;
            return;
        }

        Transform topBlock = transform.GetChild(0);
        float maxY = topBlock.localPosition.y;
        foreach (Transform child in transform)
        {
            if (child.localPosition.y > maxY)
            {
                maxY = child.localPosition.y;
                topBlock = child;
            }
        }

        Vector3 newCenter = topBlock.localPosition;
        newCenter.y += topBlock.localScale.y / 2f;
        boxCollider.center = newCenter;
    }


}
