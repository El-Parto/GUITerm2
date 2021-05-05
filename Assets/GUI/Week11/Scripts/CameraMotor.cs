using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // need this for Nue input system

namespace ThirdPersonController
{
    
    public class CameraMotor : MonoBehaviour
    {

        //defining var's
        [SerializeField, Range(0, 3)] private float sensitivity = .5f;
        [SerializeField] private Vector2 verticalLookBounds = new Vector2(-90f, 45f);
        [SerializeField] private InputActionReference look; // need this with inspecter version
        [SerializeField] private Transform player;//handles camera stuff


        //private new Camera camera;

        [Header("Collisiions")]
        //calc between cam pv and cam
        [SerializeField] private new Transform camera;
        //* the distance from the player the camera will sit
        [SerializeField, Min(.1f)] private float cameraDistance;
        //* How smooth the camera will be.
        [SerializeField, Range(.05f, .2f)] private float damping = .1f;
        //* THe radius that the camera has a collider of and offset its position from hit things
        [SerializeField, Range(.1f, .5f)] private float colliderRadius = .25f;

        private Vector2 rotation = Vector2.zero;
        private Vector3 velocity = Vector3.zero;

       // now need to hook input sys with code
       // 2 ways to set; set in inspector (better)
       // or hard code

        // Start is called before the first frame update
        void Start()
        {
            // remember that information inputs which is the event way (proper?) 
            // movement based whi
            //linking
            look.action.performed += OnLookPerformed;

            //* Start the camera at the maximum distance from the player
            camera.localPosition = new Vector3(0, 0, -cameraDistance);
            
        }

        // Update is called once per frame
        void Update()
        {
            // applying position
            transform.localRotation = Quaternion.AngleAxis(rotation.y, Vector3.left);
            player.localRotation = Quaternion.AngleAxis(rotation.x, Vector3.up);

            // Calculate the smoothed out position based on the environment around the camera.
            Vector3 newPos = Vector3.SmoothDamp(
                camera.position, CalculatePosition(),
                ref velocity, damping);

            //set cam pos
            camera.position = newPos;
        }

        private Vector3 CalculatePosition()
        {
            //* Calculate the default position of the camera using the target. Transform is the target
            Vector3 newPos = transform.position - transform.forward * cameraDistance;

            // * use the inverse of the forward for the direction
            Vector3 direction = -transform.forward;

            if(Physics.Raycast(transform.position, direction, out RaycastHit hit, cameraDistance))
            {
                // Set the newPosition to slightly offset from the hit collider
                newPos = hit.point + transform.forward * colliderRadius;
            }
            return newPos;
            // now ya calced the end ppoint
        }


        // callback context;
        /*You can convert to string, wait just press controll click to see, just remember Tvalue REadValue
          InputActionPhase- input interaction is like the input manager inplementation.*/
        private void OnLookPerformed(InputAction.CallbackContext _context)
        {
            //get value from it
            // * The Actual Value of the input ( Thumbstick pos/Mouse delta
            Vector2 value = _context.ReadValue<Vector2>();

            //add value
            //*Add the input values X sensititivity to the rotation and clamp the Y rotation
            rotation += value * sensitivity;

            rotation.y = Mathf.Clamp(rotation.y, verticalLookBounds.x, verticalLookBounds.y);
        }

        private void OnDrawGizmosSelected()
        {
            // drawing the boom arm

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position - transform.forward * cameraDistance);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position - transform.forward* cameraDistance, colliderRadius);
        }

    }

}