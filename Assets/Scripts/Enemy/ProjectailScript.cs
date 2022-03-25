using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents.Integrations.Match3;
using UnityEngine;

public class ProjectailScript : MonoBehaviour
{
    public float speed = 20f;
    public float speedRotation = 3f;
    public float damage = 10f;
    public float lifeTime = 20f;
    public bool forward = false;
    private Transform pl;
    
    void Start()
    {
        pl = GameObject.Find("Player").transform;
        //Vector3 dir = (pl.position - transform.position).normalized;
        //transform.rotation = Quaternion.LookRotation(dir, Vector3.forward);
    }

    
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
        
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (!forward)
        {
            Vector3 dir = (pl.position - transform.position).normalized;
            transform.forward = Vector3.Slerp(transform.forward, dir, speedRotation * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerHits>() != null)
            {
                other.GetComponent<PlayerHits>().OnHit(damage);
            }
        }
        Destroy(gameObject);
    }
}
