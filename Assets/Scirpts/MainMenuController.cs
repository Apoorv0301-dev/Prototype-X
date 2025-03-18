using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private void Start() {
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void Play()
    {
        SceneManager.LoadScene("Backstory", LoadSceneMode.Single);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
