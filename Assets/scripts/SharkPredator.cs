using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkPredator : MonoBehaviour
{
    float speed = 5.0f;
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
        else if (Physics.Raycast(transform.position, this.transform.forward * 50, out hit1))
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
                speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
        }

        transform.Translate(0, 0, Time.deltaTime * speed);

        //if (Random.Range(0, 100) < 10)
        //    ApplyRules();
        
    }

    void ApplyRules()
    {
        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.01f;
        int groupSize = 0;

        vcentre = vcentre / groupSize + (myManager.goalPos - this.transform.position);
        speed = gSpeed / groupSize;

        Vector3 direction = (vcentre + vavoid) - transform.position;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
        }
    }
}
