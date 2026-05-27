using UnityEngine;

public class CameraMoveToPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    // late update makes sure this script executes after the player move, otherwise it could move the camera before the player, resulting it in being a frame behind, making it look ugly and jittering
    void LateUpdate()
    {
        transform.position = player.transform.position + new Vector3(-12.7f, 12.1f, -12.7f);
    }
}
