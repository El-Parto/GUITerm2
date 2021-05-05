using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//third person
[RequireComponent(typeof(Camera))]
public class ThirdPersonCamera : MonoBehaviour
{


    [Header("Camera Settings")]
    [SerializeField, Min(.1f)] private float cameraDistance = .1f;
    [SerializeField, Range(.05f, .2f)] private float damping = .1f;

    [SerializeField] private Transform target;
    [SerializeField] private Transform player;
    [SerializeField, Min(.5f)] private float minDistanceToPlayer = .5f;


    private new Camera camera;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        // wana get the direction and move the camera to where it should be
        transform.localPosition = new Vector3(0, 0, -cameraDistance);
        camera = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //* update the cameras position based on distance
        //* This will give us a smooth zoom effect for transition
        Vector3 newPos = Vector3.SmoothDamp(
            transform.position, CalculatePos(), ref velocity, damping);

        transform.position = newPos;
    }

    public bool CamRotate()
    {
        //* if the distance is too low, negate rotation.
        return Vector3.Distance(transform.position, player.position) > minDistanceToPlayer;

    }

    private Vector3 CalculatePos()
    {
        //default pos
        Vector3 newPos = target.position - target.forward * cameraDistance;

        //* calculate actual end point
        //direction first
        Vector3 direction = -transform.forward;

        if(Physics.Raycast(target.position, direction, out RaycastHit hit, cameraDistance))
        {
            // takes point in world and makes it local
            newPos = hit.point;
        }
        return newPos;

    }
}
