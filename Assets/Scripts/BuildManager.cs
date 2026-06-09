using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildManager : MonoBehaviour
{
    InputAction clickAction;
    InputAction mousePositionAction;
    InputAction rightClickAction;
    [SerializeField] GameObject structure;
    [SerializeField] float gridSize;

    HashSet<Vector2> placed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        placed = new HashSet<Vector2>();

        clickAction = InputSystem.actions.FindAction("Click");        
        rightClickAction = InputSystem.actions.FindAction("RightClick");        
        mousePositionAction = InputSystem.actions.FindAction("MousePosition");
    }

    // Update is called once per frame
    void Update()
    {
        // if left click
        if (clickAction.triggered)
        {
            
            // get mouse position in form of (-1, -1) to (1,1) vector with (1,1) being the top right
            Vector2 mousePos = mousePositionAction.ReadValue<Vector2>();

            // raycast from the camera to the ground
            // make sure it raycasts to detect only the floor and not the player or placed items
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            // if the raycasts hit, which it should always
            if (Physics.Raycast(ray, out hit))
            {


                Debug.Log("Hit: " + hit.collider.name);

                Debug.DrawLine(ray.origin, hit.point, Color.red, 100f);

                // constrain to grid
                Vector3 constrainedPosition = new Vector3(Mathf.Round(hit.point.x / gridSize) * gridSize, 0, Mathf.Round(hit.point.z / gridSize) * gridSize);
                Vector2 constrainedPositionTwoD = new Vector2(constrainedPosition.x, constrainedPosition.z);

                if (!placed.Contains(constrainedPositionTwoD))
                {
                    placed.Add(constrainedPositionTwoD);
                    GameObject clone = Instantiate(structure, constrainedPosition, Quaternion.identity);
                }
            }
        }
        if (rightClickAction.triggered)
        {
            
            // get mouse position in form of (-1, -1) to (1,1) vector with (1,1) being the top right
            Vector2 mousePos = mousePositionAction.ReadValue<Vector2>();

            // raycast from the camera to the ground
            // make sure it raycasts to detect only the floor and not the player or placed items
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            // if the raycasts hit, which it should always
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit: " + hit.collider.name);

                Debug.DrawLine(ray.origin, hit.point, Color.red, 100f);

                if (hit.transform.parent != null && hit.collider.transform.parent.gameObject.CompareTag("Structure"))
                {
                    Vector3 constrainedPosition = new Vector3(Mathf.Round(hit.point.x / gridSize) * gridSize, 0, Mathf.Round(hit.point.z / gridSize) * gridSize);
                    Vector2 constrainedPositionTwoD = new Vector2(constrainedPosition.x, constrainedPosition.z);

                    if (placed.Contains(constrainedPositionTwoD)) {
                        placed.Remove(constrainedPositionTwoD);
                        Destroy(hit.collider.transform.parent.gameObject);
                    }
                }
            }

        }
    }
}
