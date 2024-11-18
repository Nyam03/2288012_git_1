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
            weaponManager.playerCoin += 10; // ���� 1���� 10 ��� �߰�
            Destroy(other.gameObject); // ���� ������Ʈ ����
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
