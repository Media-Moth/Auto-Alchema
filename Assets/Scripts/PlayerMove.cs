using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    InputAction moveAction;
   [SerializeField] float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVec = moveAction.ReadValue<Vector2>() * Time.deltaTime;
        Vector3 moveVec = Quaternion.AngleAxis(45, Vector3.up) * new Vector3(inputVec.x, 0, inputVec.y) * speed;
        transform.LookAt(moveVec + transform.position, Vector3.up);
        transform.position += moveVec;
    }
}
