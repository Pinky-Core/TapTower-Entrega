using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject dronePrefab;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float spawnOffsetY = 1f;

    private float lastSpawnTime;

    private void Update()
    {
        if (Time.time - lastSpawnTime >= spawnInterval)
        {
            SpawnDrone();
            lastSpawnTime = Time.time;
        }
    }

    private void SpawnDrone()
    {
        // Instancia el drone en la posición actual del spawner
        GameObject drone = Instantiate(dronePrefab, transform.position + Vector3.down * spawnOffsetY, Quaternion.identity);

        // Le damos una referencia al gancho para que sepa si está a la izquierda o derecha
        DroneBehaviour droneScript = drone.GetComponent<DroneBehaviour>();
        if (droneScript != null)
        {
            droneScript.InitializeDirection(transform.position.x);
        }
    }
}
