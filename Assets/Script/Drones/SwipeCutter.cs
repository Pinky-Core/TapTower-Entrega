using UnityEngine;

public class SwipeCutter : MonoBehaviour
{
    [Header("Configuración del corte")]
    public float swipeRadius = 1f; // Radio del SphereCast
    public LayerMask detectionLayer; // Capa que contiene los objetos que se pueden cortar

    private Vector3 previousPosition;
    private bool isSwiping = false;

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            isSwiping = true;
            previousPosition = GetWorldPosition(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isSwiping = false;
        }

        if (isSwiping)
        {
            Vector3 currentPosition = GetWorldPosition(Input.mousePosition);
            DetectCutBetween(previousPosition, currentPosition);
            previousPosition = currentPosition;
        }

#else // Dispositivos móviles
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isSwiping = true;
                previousPosition = GetWorldPosition(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isSwiping = false;
            }

            if (isSwiping && touch.phase == TouchPhase.Moved)
            {
                Vector3 currentPosition = GetWorldPosition(touch.position);
                DetectCutBetween(previousPosition, currentPosition);
                previousPosition = currentPosition;
            }
        }
#endif
    }

    Vector3 GetWorldPosition(Vector3 screenPos)
    {
        screenPos.z = 10f; // Distancia desde la cámara
        return Camera.main.ScreenToWorldPoint(screenPos);
    }

    void DetectCutBetween(Vector3 start, Vector3 end)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;

        if (distance == 0)
            return;

        RaycastHit[] hits = Physics.SphereCastAll(start, swipeRadius, direction.normalized, distance, detectionLayer);
        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Drone"))
            {
                Destroy(hit.collider.gameObject); // Corta el drone
            }
        }
    }
}
