using UnityEngine;

public class WallMovementManager : MonoBehaviour
{
    public float wallSpeed;

    private void FixedUpdate()
    {
        transform.Translate(((Vector3.up * wallSpeed) * Time.deltaTime));
    }
}
