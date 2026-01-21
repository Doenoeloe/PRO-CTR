using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCombat : MonoBehaviour
{
    
    [SerializeField] private float maxHealth = 30;
    [SerializeField] private Slider healthSlider;
    private float currentHealth;
    
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void StartAttackMinigame(Action onComplete)
    {
        // Start bullet hell minigame
        // When player survives/completes, call onComplete()
    }
    
    public int CalculateAttackDamage()
    {
        // Based on how well player dodged
        return 3;
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        Debug.Log($"Enemy took {damage} damage. HP: {currentHealth}/{maxHealth}");
    }
    
    public bool IsDead()
    {
        return currentHealth <= 0;
    }
}
