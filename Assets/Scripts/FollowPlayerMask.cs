using UnityEngine;

public class FollowPlayerMask : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = Vector3.zero;

    void LateUpdate()
    {
        if (!player) return;
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z) + offset;
    }
}