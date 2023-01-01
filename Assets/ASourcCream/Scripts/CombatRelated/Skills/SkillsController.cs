using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SkillsController : MonoBehaviour
{
    private IndicatorController indicatorController;
    private ColliderController colliderController;

    [SerializeField]
    private GameObject skillParticle;

    // Start is called before the first frame update
    void Start()
    {
        indicatorController = GetComponent<IndicatorController>();
        colliderController = GetComponent<ColliderController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseAOESkill(CommonParams skillParams, Phase phase) {

        if (phase == Phase.Start) {
            StartAOESkill(skillParams);
        } else if (phase == Phase.End) {
            EndAOESkill(skillParams);
        }
    }

    private void StartAOESkill(CommonParams skillParams) {

        indicatorController.SpawnDecal(skillParams, true);
    }

    private void EndAOESkill(CommonParams skillParams) {
        indicatorController.DespawnDecal();
        
        //Create collider
        colliderController.SpawnCollider(skillParams.location, IndicatorShape.Circle, 0.5f, 10.0f, 10.0f , null);
        //Spawn particle
        var instancedSkillParticle = Instantiate(skillParticle);
        instancedSkillParticle.transform.localPosition = skillParams.location;
        StartCoroutine(GenericMonoHelper.Instance.DestroyAfterDelay(6.0f, instancedSkillParticle));
        

    }
}
public enum Phase {
    Start,
    End,
}
public struct CommonParams {
    public Vector3 location {get; set;}
    public Vector3 size {get; set;}
    public IndicatorShape shape {get; set;}
    public Vector3 rotation {get; set;}

    public CommonParams SetValues(IndicatorShape shape, params Vector3[] sizeLocatonRotation) {
        this.size = sizeLocatonRotation[0];
        this.location = sizeLocatonRotation[1];
        this.rotation = sizeLocatonRotation[2];
        this.shape = shape;
        return this;
    }
}