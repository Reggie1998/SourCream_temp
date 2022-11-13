using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsComponent : MonoBehaviour
{
    [SerializeField]
    private Health health = new Health();

    [SerializeField]
    private Affinity affinity = new Affinity(0.0f, 0.0f, 0.0f); // Character proficiency in each affinity

    [SerializeField]
    private Dictionary<string, Modifier> modifiers;

    [InspectorButton("ApplyModifier")]
    public bool applyModifier;
    public void ApplyModifier() {
        Debug.Log("Clicked");
        var mod = new Modifier(ModifierType.HEALTH, 1.0f);
        mod.AddToModifier(0.5f, 5);
    }
}
