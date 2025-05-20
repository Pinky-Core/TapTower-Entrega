using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Requiere que este GameObject tenga un componente Button
[RequireComponent(typeof(Button))]
public class SceneChangeUI : MonoBehaviour
{
    // Nombre de la escena a la que se va a cambiar al hacer clic
    [SerializeField] private string sceneToSwitchName;

    // Referencia al componente Button
    private Button button;

    void Start()
    {
        // Se obtiene el componente Button al iniciar
        button = GetComponent<Button>();

        // Se le asigna el m�todo LoadNext al evento de clic del bot�n
        button.onClick.AddListener(LoadNext);
    }

    void OnDestroy()
    {
        // Se remueve el listener al destruir el objeto (buena pr�ctica para evitar referencias colgantes)
        button.onClick.RemoveListener(LoadNext);
    }

    // M�todo que carga la nueva escena
    private void LoadNext()
    {
        SceneManager.LoadScene(sceneToSwitchName);
    }
}
