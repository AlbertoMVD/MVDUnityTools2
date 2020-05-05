using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    // Auto turret properties
    public float firingRange; // Range of the 
    public float rotationSpeed; // base rotation speed
    public float velocity; // bullet velocity

    // Auto turret references

    public Transform projectile; // Prefab of my projectile

    public Transform baseRotation;
    public Transform target;
    public Transform gunBody;
    public Transform gunBarrel;
    public ParticleSystem muzzleFlash;

    [HideInInspector] public bool canFire = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SphereCollider>().radius = firingRange;
    }

    // Update is called once per frame
    void Update()
    {
        // In case i can fire, i want to fire.
        if(canFire)
        {
            Debug.Log("I am firing");
            Aim();
            Fire();
        }

        // Task for you guys, update the turret functionality, to lerp smoothly.
        // Work on the particles effects.
    }

    // Events to handle tricky situations
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canFire = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canFire = false;
        }
    }

    // Method to open fire on a target
    [ContextMenu("Fire")]
    public void Fire()
    {
        // We create a bullet and we send it into the given direction that we want, using rigidbody physics
        Transform bullet = Instantiate(projectile, gunBarrel.transform.position, gunBody.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = gunBody.transform.forward * velocity;

        if(!muzzleFlash.isPlaying)
        {
            muzzleFlash.Play();
        }
    }

    // Method to aim a target
    [ContextMenu("Aim")]
    public void Aim()
    {
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        baseRotation.transform.LookAt(targetPosition);
        gunBody.transform.LookAt(target.position);
    }
}
