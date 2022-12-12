using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsComponent : MonoBehaviour
{
    [SerializeField]
    public Health health = new Health();

    [SerializeField]
    private Dictionary<string, Modifier> modifiers;


    [InspectorButton("ApplyModifier")]
    public bool applyModifier;
    public void ApplyModifier() {
        Debug.Log("Clicked");
        var mod = new Modifier(ModifierType.HEALTH, 1.0f);
        mod.AddToModifier(0.5f, 5);
        

    }

    public void Damage(float amount) {
        health.UpdateHealth(-amount);
    }

}
