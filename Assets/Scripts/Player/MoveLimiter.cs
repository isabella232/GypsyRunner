using UnityEngine;

public class MoveLimiter : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Vector3 playerPositionUpdater;

    private void Awake() => playerPositionUpdater = transform.position;

    private void Update()
    {
        playerPositionUpdater.z = player.transform.position.z;
        transform.position = playerPositionUpdater;
    }
}
