using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float maxHealth = 20;
    [SerializeField] private int criticalDamageMultiplier = 3;
    [SerializeField] private int baseFightDamage = 5;// 3x damage
    [SerializeField] Skillcheck skillcheck;
    private float currentHealth;
    private float lastCalculatedDamage;
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void StartFightMinigame(Action onComplete)
    {
        // Start skillcheck with callback
        skillcheck.StartSkillCheck(baseFightDamage, (damage) => {
            // Store damage when skillcheck completes
            lastCalculatedDamage = damage;
            
            // Continue to next phase
            onComplete?.Invoke();
        });
    }
    
    public float CalculateFightDamage()
    {
        return lastCalculatedDamage;
    }
    
    public void StartCriticalMinigame(Action onComplete)
    {
        // Implement critical attack minigame (harder timing, more intense)
        // When complete, call onComplete()
    }
    
    public float CalculateCriticalDamage()
    {
        // Explosive damage - much higher than normal attack
        float baseDamage = CalculateFightDamage();
        return baseDamage * criticalDamageMultiplier;
    }
    
    public void ShowCriticalCharge()
    {
        // Play charge animation/particle effects
        Debug.Log("Critical attack is charging!");
    }
    
    public void ShowItemMenu(Action onComplete)
    {
        // Show items
        onComplete();
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Player took {damage} damage. HP: {currentHealth}/{maxHealth}");
    }
    
    public bool IsDead()
    {
        return currentHealth <= 0;
    }
}
