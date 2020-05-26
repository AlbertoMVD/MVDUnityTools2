using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[ExecuteInEditMode]
public class ClickSpawn : MonoBehaviour
{
    public GameObject prefab;

    // Placing parameters needed.
    public int layerIndex; // Layer to work with
    // tag
    // Physics

    // Custom transformation
    // position
    // rotation
    // scale

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        if(!Application.isEditor)
        {
            Destroy(this);
        }

        SceneView.duringSceneGui += OnScene;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui += OnScene;
    }

    // Do all the raycasting and necessary tests to place objects.
    void OnScene(SceneView scene)
    {
        // TO-DO
        // Get the info from the MVDTools window
        // Set the layer that was given by the UI
        // Set the parent that was given by the UI

        // Set the transform settings
        SceneView.lastActiveSceneView.Repaint();

        // Get the mouse coordinates from the screen
        Vector3 mousePos = Event.current.mousePosition;
        float ppp = EditorGUIUtility.pixelsPerPoint;
        mousePos.y = scene.camera.pixelHeight - mousePos.y * ppp;
        mousePos.x *= ppp;

        // Make a raycast from the scene cursor view
        RaycastHit hit;
        Ray ray = scene.camera.ScreenPointToRay(mousePos);
        //int layerMask = layerIndex < 0 ? ~(1 << layerIndex | 1 << 2) : (1 << layerIndex);
        int layerTestMask = LayerMask.GetMask("Environment");

        bool castRay = Physics.Raycast(ray, out hit, float.PositiveInfinity, layerTestMask, QueryTriggerInteraction.Ignore);

        Vector3 point = castRay ? hit.point : ray.origin + 10 * ray.direction;
        Vector3 normal = castRay ? hit.normal: Vector3.up;

        if(this)
        {
            // To refactor in the future
            float[] values = new[] {
                Mathf.Abs(Vector3.Dot(Vector3.right, normal)),
                Mathf.Abs(Vector3.Dot(Vector3.up, normal)),
                Mathf.Abs(Vector3.Dot(Vector3.forward, normal)) };
            Vector3 myforward = System.Array.IndexOf(values, values.Max()) == 1 ? Vector3.right : Vector3.up;

            Quaternion new_rotation = Quaternion.LookRotation(myforward, normal);
            transform.position = point;
            transform.rotation = new_rotation;
            //transform.localScale = 
            //transform.SetParent()

            HandlePlacement(Event.current, normal);
        }

        // Place me in the proper position
    }

    // Use this method to instantiate all the prefab copies.
    private void HandlePlacement(Event e, Vector3 normal)
    {
        if (e.type == EventType.MouseDown && e.button == 0)
        {
            GameObject go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            //go.transform.localScale = scale;
            go.transform.position = transform.position;
            go.transform.rotation = transform.rotation;

            // If we want to have some physics, just test here.
        }
    }
}
