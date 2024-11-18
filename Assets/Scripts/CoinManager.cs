using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public Text coinText;
    private int coinScore;
    public WeaponManager weaponManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            weaponManager.playerCoin += 10; // 코인 1개당 10 골드 추가
            Destroy(other.gameObject); // 코인 오브젝트 제거
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoins(int amount)
    {
        coinScore += amount;
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        coinText.text = coinScore.ToString();
    }
}
