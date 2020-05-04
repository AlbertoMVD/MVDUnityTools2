using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyGameScripts/BounceSphere")]
public class BounceSphere : MonoBehaviour
{
    [RangeAttribute(0,100)]
    public int health = 0;
    public int maxHealth = 100;

    [Header("Shield Settings")]
    [HideInInspector] public int shield = 0;
    [HideInInspector] public int maxShield = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
