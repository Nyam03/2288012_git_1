using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Text healthText;

    private PauseMenu pauseMenu;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died!");

        if (pauseMenu != null)
        {
            pauseMenu.ShowDeathUI();
        }
    }
}
