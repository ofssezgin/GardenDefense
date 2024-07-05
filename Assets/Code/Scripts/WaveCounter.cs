using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveCounter : MonoBehaviour
{
    public Text waveCounterText;
    
    void Update()
    {   
        if (EnemySpawner.main.currentWave < 10){
            waveCounterText.text ="Wave: " + EnemySpawner.main.currentWave.ToString() + "/10";
        }
        else {
            waveCounterText.text ="Wave: 10/10";
        }
    }
}