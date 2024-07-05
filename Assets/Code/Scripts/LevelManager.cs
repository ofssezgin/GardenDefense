using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;
    [SerializeField] public GameObject noMoneyUI;

    public int currency;
    public int baseHealth;

    private void Awake() {
        main = this;
    }

    private void Start() {
        currency = 200;
        baseHealth = 10;
    }

    public void IncreaseCurrency(int amount) {
        currency += amount;
    }

    public bool SpendCurrency(int amount) {
        if (amount <= currency) {
            // Buy item
            currency -= amount;
            return true;
        } else {
            return false;
        }
    }

    public void notEnoughMoney()
    {
        noMoneyUI.SetActive(true);
        StartCoroutine(HideNoMoneyUIAfterDelay(2f));
    }

    private IEnumerator HideNoMoneyUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        noMoneyUI.SetActive(false);
    } 

}
