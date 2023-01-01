using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMonoHelper : MonoBehaviour
{
    private static GenericMonoHelper _instance;

    public static GenericMonoHelper Instance {
        get {
            if (_instance is null) {
                Debug.Log("Generic Mono helper is Null");
            }
            return _instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DestroyAfterDelay(float delay, GameObject objectToDestroy) {
        yield return new WaitForSeconds(delay);
        Destroy(objectToDestroy);
    }
}
