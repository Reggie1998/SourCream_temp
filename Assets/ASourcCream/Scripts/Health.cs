using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Health
{
    [SerializeField]
    private float maxHealth = 100.0f;

    [SerializeField]
    private float currentHealth = 100.0f;

    public void UpdateHealth(float amount)
    {
        this.currentHealth += amount;
        if (this.currentHealth > maxHealth) {
            this.currentHealth = maxHealth;
        }
    }

    public void SetHealth(float newhealth) {
        if (newhealth > maxHealth) {
            this.currentHealth = maxHealth;
        } else {
            this.currentHealth = newhealth;
        }
    }
}
