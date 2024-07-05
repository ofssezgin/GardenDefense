using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour
{
    public Text healthText;
    
    void Update()
    {   
        healthText.text = LevelManager.main.baseHealth.ToString();
    }
}
