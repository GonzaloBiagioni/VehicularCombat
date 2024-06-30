using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public TextMeshProUGUI healthText; 

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthText(); 

        if (currentHealth <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Derrota"); 
        }
    }

    void UpdateHealthText()
    {
        healthText.text = "HP: " + currentHealth.ToString();
    }
}