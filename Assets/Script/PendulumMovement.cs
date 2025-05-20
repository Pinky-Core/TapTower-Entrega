using UnityEngine;

public class PendulumMovement : MonoBehaviour
{
    [SerializeField, Range(0, 360)] private float maxAngle = 45; // Ángulo máximo de oscilación en grados
    [SerializeField] private float speedPendulum = 2;            // Velocidad de oscilación del péndulo
    [SerializeField] private Rigidbody pivot;                    // Rigidbody que se moverá como el eje del péndulo

    private void FixedUpdate()
    {
        // Calcula el ángulo actual usando una función seno para crear un movimiento oscilatorio
        float angle = maxAngle * Mathf.Sin(Time.time * speedPendulum);

        // Aplica la rotación al Rigidbody como una oscilación en el eje Z (como un péndulo)
        pivot.MoveRotation(Quaternion.Euler(0, 0, angle));
    }
}
