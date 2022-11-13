using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public Skill(Affinity affinity, Affinity minAffinity, float skillCost, float damage) {
        this.affinity = affinity;
        this.minAffinity = minAffinity;
        this.skillCost = skillCost;
        this.damage = damage;
    }

    // Game
    private GameObject bodyPart;
    private Animation animation;

    // Stats
    private Affinity affinity;          // Conversion rates between affinity types for this skill
    private Affinity minAffinity;       // Minimum character affinity to use the skill
    private float skillCost;            // Affinity resource cost & generation
    private float damage;
}