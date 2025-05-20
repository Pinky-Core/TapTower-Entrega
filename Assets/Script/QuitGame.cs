using System;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private void Awake()
    {

#if UNITY_WEBGL
         gameObject.SetActive(false);
#endif
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
