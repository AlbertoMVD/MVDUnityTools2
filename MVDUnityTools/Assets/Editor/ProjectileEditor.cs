using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Projectile))]
public class ProjectileEditor : Editor
{
    [DrawGizmo(GizmoType.Selected)]
    static void DrawGizmosSelected(Projectile projectile, GizmoType gizmoType)
    {
        //Gizmos.DrawWireSphere(projectile.transform.position, projectile.damageRadius);
    }

    private void OnSceneGUI()
    {
        // We are getting the projectile damage radius
        // We are updating the radius with the handle changes
        // We are updating the collider radius with the new value
        Projectile bullet = (Projectile)target;
        bullet.damageRadius = Handles.RadiusHandle(bullet.transform.rotation, bullet.transform.position, bullet.damageRadius);
        bullet.GetComponent<SphereCollider>().radius = bullet.damageRadius;

        // Get the prefab and update the values
    }
}
