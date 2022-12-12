using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{

    public float forwardForce, upwardForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void shootSimpleProjectile(Vector3 targetPoint, Vector3 spawnerLocation, GameObject projectile, float force)
    {   
        Vector3 direction = targetPoint - spawnerLocation;
        GameObject currentProjectile = Instantiate(projectile, spawnerLocation, Quaternion.identity);
        currentProjectile.transform.forward = direction.normalized;
        currentProjectile.GetComponent<Rigidbody>().AddForce(direction.normalized * force, ForceMode.Impulse);
    }
    //shoot aoe

    // get where to cast

    // get the size/shape

    //cast
}
