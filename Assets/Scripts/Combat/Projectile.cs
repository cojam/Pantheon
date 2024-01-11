using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float startTime = 0.0f;
    float maxLifeSpan = 0.5f;
    public Vector3 endLocation;
    public float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > startTime + maxLifeSpan){
            Destroy(this.gameObject);
        }
        if(endLocation != null){
            if (transform.position != endLocation)
            {
                transform.position = Vector3.MoveTowards(transform.position , endLocation, moveSpeed * Time.deltaTime);
            }
            else{
                Destroy(this.gameObject);
            }
        }
    }
}
