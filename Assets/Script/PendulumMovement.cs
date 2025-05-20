using UnityEngine;

public class PendulumMovement : MonoBehaviour
{
    [SerializeField, Range(0, 360)] private float maxAngle = 45; // �ngulo m�ximo de oscilaci�n en grados
    [SerializeField] private float speedPendulum = 2;            // Velocidad de oscilaci�n del p�ndulo
    [SerializeField] private Rigidbody pivot;                    // Rigidbody que se mover� como el eje del p�ndulo

    private void FixedUpdate()
    {
        // Calcula el �ngulo actual usando una funci�n seno para crear un movimiento oscilatorio
        float angle = maxAngle * Mathf.Sin(Time.time * speedPendulum);

        // Aplica la rotaci�n al Rigidbody como una oscilaci�n en el eje Z (como un p�ndulo)
        pivot.MoveRotation(Quaternion.Euler(0, 0, angle));
    }
}
