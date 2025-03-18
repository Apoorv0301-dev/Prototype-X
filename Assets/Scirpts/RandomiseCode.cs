using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RandomiseCode : MonoBehaviour
{
    public TextMeshPro[] digits;
    public string code = "";
    // Start is called before the first frame update
    void Start()
    {
        System.Random random = new System.Random();
        for(int i = 0; i < digits.Length; i++)
        {
            int num = random.Next(1, 10);
            digits[i].text = num.ToString();
            code = code + num.ToString();
        }
    }
}
