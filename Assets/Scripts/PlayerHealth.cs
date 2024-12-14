using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Text healthText;

    public AudioClip damageSound;
    public Image damageFlashImage;
    public float flashDuration = 0.1f;
    private AudioSource audioSource;

    private PauseMenu pauseMenu;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = 0.1f;
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

        PlayDamageSound();
        StartCoroutine(FlashDamageEffect());
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

    void PlayDamageSound()
    {
        if (damageSound != null && audioSource != null)
        {
            audioSource.clip = damageSound;
            audioSource.Play();
        }
    }

    IEnumerator FlashDamageEffect()
    {
        if (damageFlashImage != null)
        {
            damageFlashImage.color = new Color(1, 0, 0, 0.2f); // 빨간색
            yield return new WaitForSeconds(flashDuration);
            damageFlashImage.color = new Color(1, 0, 0, 0); // 투명하게
        }
    }
}
