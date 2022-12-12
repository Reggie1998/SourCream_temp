using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{   
    
    private CharMovement holder;
    // Start is called before the first frame update
    void Start()
    {
        holder = GetComponentInParent<CharMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collider)
    {
        //Debug.Log("enter");
        holder.onColide(collider);
    }

    public void OnTriggerExit(Collider collider)
    {
        //Debug.Log("exit");
    }

}
