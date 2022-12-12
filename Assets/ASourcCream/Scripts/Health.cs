using UnityEngine;
using System;

[System.Serializable]
public class Health
{
    [SerializeField]
    private float maxHealth = 100.0f;

    [SerializeField]
    private float currentHealth = 100.0f;

    public int healthPct = 100;

    public event Action<float, float> onHealthChanged = delegate { };


    public void UpdateHealth(float amount)
    {
        this.currentHealth += amount;
        if (this.currentHealth > maxHealth) {
            this.currentHealth = maxHealth;
        }
        if (this.currentHealth + amount < 0) {
            this.currentHealth = 0;
        }
        onHealthChanged(currentHealth, maxHealth);
    }

    public void SetHealth(float newhealth) {
        if (newhealth > maxHealth) {

            this.currentHealth = maxHealth;
            onHealthChanged(currentHealth, maxHealth);
        } else {
            this.currentHealth = newhealth;
            onHealthChanged(currentHealth, maxHealth);
        }
        
    }

}
