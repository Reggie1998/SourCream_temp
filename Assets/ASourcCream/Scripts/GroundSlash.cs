using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Not complete from -> https://youtu.be/qh3TEYUN6cA
public class GroundSlash : MonoBehaviour
{
    public float speed = 30.0f;
    public float slowDownRate = 0.01f;
    public float detectingDistance = 0.01f;
    public float destroyDelay = 5;

    private Rigidbody rb;
    private bool stopped;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, 0 , transform.position.z);
        rb = GetComponent<Rigidbody>();
        StartCoroutine(SlowDown());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if (!stopped) {
            RaycastHit hit;
            Vector3 distance = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            if (Physics.Raycast(distance, transform.TransformDirection(-Vector3.up), out hit, detectingDistance)) {
                transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            } else {
                transform.position = new Vector3(transform.position.x, 0 , transform.position.z);
            }
            Debug.DrawRay(distance, transform.TransformDirection(-Vector3.up * detectingDistance), Color.red);
        }    
    }

    IEnumerator SlowDown()
    {
        float t = 1;
        while (t > 0) {
            rb.velocity = Vector3.Lerp(Vector3.zero, rb.velocity, t);
            t -= slowDownRate;
            yield return new WaitForSeconds(0.1f);
        }

        stopped = true;
    }
}
