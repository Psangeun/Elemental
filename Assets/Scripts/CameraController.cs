using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject player;
    
    Transform target;
    [SerializeField] float smoothing = 0.2f;
    [SerializeField] Vector2 minCameraBoundary;
    [SerializeField] Vector2 maxCameraBoundary;

    // Start is called before the first frame update
    void Start()
    {
        target = player.transform;
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, this.transform.position.z);

        targetPos.x = Mathf.Clamp(targetPos.x, minCameraBoundary.x, maxCameraBoundary.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minCameraBoundary.y, maxCameraBoundary.y);

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
    }
}
