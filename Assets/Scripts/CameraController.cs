using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float camSpeed;
    private float lookAhead;

    private void Update()
    {
        // Room cam
        // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z),
        //ref velocity, speed);

        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * camSpeed); // Gradually moves value from lookAhead to aheaddistance * direction
    }

    public void MoveNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}
