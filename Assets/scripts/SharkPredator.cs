using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkPredator : MonoBehaviour
{
    float speed = 1.0f;
    public FlockManager myManager;
    bool turning1 = false;
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        Bounds bounds = new Bounds(myManager.transform.position, myManager.swimLimits * 2);

        RaycastHit hit1 = new RaycastHit();
        Vector3 direction1 = myManager.transform.position - transform.position;

        if (!bounds.Contains(transform.position))
        {
            turning1 = true;
        }
        else if (Physics.Raycast(transform.position, this.transform.forward * 100, out hit1))
        {
            turning1 = true;
            direction1 = Vector3.Reflect(this.transform.forward, hit1.normal);
        }
        else
        {
            turning1 = false;
        }

        if (turning1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(direction1), myManager.rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (Random.Range(0, 100) < 10)
                speed = 7.0f;
        }

        transform.Translate(0, 0, Time.deltaTime * speed);

        if(myManager.isFlocking)
            ApplyRules();
        
    }

    void ApplyRules()
    {
        Vector3 vcentre1 = Vector3.zero;

        vcentre1 = vcentre1 + (myManager.flockCenter - this.transform.position);

        Vector3 direction1 = vcentre1;
        if (direction1 != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction1), 1 * Time.deltaTime);
        }
    }
}
