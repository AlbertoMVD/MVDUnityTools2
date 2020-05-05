using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damageRadius = 0.5f;

    private void Start()
    {
        GetComponent<SphereCollider>().radius = damageRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("The projectile hit a body");
        // I want to do something with other
    }
}
