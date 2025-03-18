using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndStory : MonoBehaviour
{
    void OnEnable()
    {
        if(gameObject.layer == LayerMask.NameToLayer("AutoSceneLoader"))
        {
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        }
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        }
    }
}
