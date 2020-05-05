using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    // Auto turret properties
    public float firingRange; // Range of the 
    public float barrelRotationSpeed; // base rotation speed
    [Range(0, 100)] public float velocity; // bullet velocity

    // Auto turret references

    public Transform projectile; // Prefab of my projectile

    public Transform baseRotation;
    public Transform target;
    public Transform gunBody;
    public Transform gunBarrel;
    public ParticleSystem muzzelFlash;

    private bool canFire = false;
    private float currentRotationSpeed; // Temp variable to hold rotationspeed
    [HideInInspector] public Vector3 offset = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SphereCollider>().radius = firingRange;
    }

    // Update is called once per frame
    void Update()
    {
        // Gun barrel rotation
        gunBarrel.transform.Rotate(0, 0, currentRotationSpeed * Time.deltaTime);

        // if can fire turret activates
        if (canFire)
        {
            Debug.Log("Firing");
            Aim();
            Fire();
        }
        else
        {
            // slow down barrel rotation and stop
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, 0, 10 * Time.deltaTime);

            // stop the particle system
            if (muzzelFlash.isPlaying)
                muzzelFlash.Stop();
        }
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
        currentRotationSpeed = barrelRotationSpeed;

        // We create a bullet and we send it into the given direction that we want, using rigidbody physics
        Transform body = Instantiate(projectile, offset, gunBody.transform.rotation);
        body.GetComponent<Rigidbody>().velocity = gunBody.transform.forward * velocity;


        if (!muzzelFlash.isPlaying)
            muzzelFlash.Play();
    }

    // Method to aim a target
    [ContextMenu("Aim")]
    public void Aim()
    {
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        baseRotation.transform.LookAt(targetPosition);
        gunBody.transform.LookAt(target.position);
    }

    [ContextMenu("Reset")]
    public void ResetValues()
    {
        offset = Vector3.zero;
    }
}
