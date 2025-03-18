using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouLoseTryAgain : MonoBehaviour
{
    private void OnEnable() {
        SceneManager.LoadScene("Backstory", LoadSceneMode.Single);
    }
}
