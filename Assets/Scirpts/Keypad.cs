using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Keypad : MonoBehaviour
{
    [SerializeField] private Text Ans;
    private string answer;
    public RandomiseCode randi;
    void Start(){
        answer = randi.code;
    }
    public void Number(int number)
    {
        Ans.text += number.ToString();
    }
    public void Execute(){
        if(Ans.text == answer){
            Ans.text = "You Win!";
            StartCoroutine("Win");
        }
        else{
            Ans.text = "Try Again!";
            StartCoroutine("ClearBar");
        }
    }
    IEnumerator ClearBar()
    {
        yield return new WaitForSeconds(1f);
        Ans.text = "";
        
    }
    IEnumerator Win()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("End");
    }

}
