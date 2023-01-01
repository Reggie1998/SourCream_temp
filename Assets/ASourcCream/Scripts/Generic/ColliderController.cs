using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    [SerializeField]
    private GameObject ConeColliderGameObject;
    [SerializeField]
    private GameObject SimpleCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCollider(Vector3 location, IndicatorShape shape, float timer, float width, float height, float? length) {
        
        Collider colliderObject = null;
        GameObject spawnedObject = null;
        switch(shape) {
            case IndicatorShape.Square:
            spawnedObject = Instantiate<GameObject>(SimpleCollider);
            colliderObject = spawnedObject.AddComponent<BoxCollider>();
            (colliderObject as BoxCollider).size = new Vector3(width, height, (float)(length == null ? length : 1.0f));
            break;
            case IndicatorShape.Circle:
            spawnedObject = Instantiate<GameObject>(SimpleCollider);
            colliderObject = spawnedObject.AddComponent<CapsuleCollider>();
            (colliderObject as CapsuleCollider).height = height;
            (colliderObject as CapsuleCollider).radius = width;
            break;
            case IndicatorShape.Cone:
            spawnedObject = Instantiate<GameObject>(ConeColliderGameObject);
            colliderObject = spawnedObject.GetComponent<MeshCollider>();
            spawnedObject.transform.localScale = new Vector3(50.0f, 50.0f, 50.0f);
            break;
        }
        if (colliderObject) {
            colliderObject.isTrigger = true;
        }
        spawnedObject.transform.position = new Vector3(location.x, location.y, location.z);

        StartCoroutine(GenericMonoHelper.Instance.DestroyAfterDelay(timer + 10.0f, spawnedObject));

    }
}

