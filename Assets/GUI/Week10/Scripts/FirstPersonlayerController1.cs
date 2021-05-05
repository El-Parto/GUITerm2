using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// Input systems


[RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class FirstPersonlayerController1 : MonoBehaviour
{
    // a property
    private Transform CamTransform => cameraPivot.transform;

    [Header("Movement Settings")]
    [SerializeField, Min(1.0f)] private float defaultSpeed = 5f;
    [SerializeField, Min(1.0f)] private float walkSpeedModifier = 1f;
    [SerializeField, Min(1.9f)] private float sprintSpeedModifier = 2f;
    [SerializeField, Min(.5f)] private float crouchSpeedModifier = .5f;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference sprintAction;
    [SerializeField] private InputActionReference crouchAction;

    private bool isSprinting = false;
    private bool isCrouching = false;


    //actions we want
    [SerializeField] private InputActionReference lookAction;
    [SerializeField] private Transform cameraPivot;

    //declaring new capsule collider so it refers to just this one.
    private new CapsuleCollider collider;
    //declaring new rigid body so it refers to just this one.
    private new Rigidbody rigidbody;

    [Header("Look Settings")]
    // * This value is set to a low number because our camera controller
    // * is not frame rate locked which makes it feel smoother.
    [SerializeField, Range(0, 3)] private float sensitivity = .5f;

    // * this is how far u and down the amera will be able to look.
    // * 90 means full vertical look without inverting the camera.
    [SerializeField, Range(0, 90)] private float verticallookCap = 90f;
    [SerializeField] private float cameraSmoothing = 1;
    //cameras are not locked to frame rate to prevent jank

    //* the current rotation of the camera that gets updated every
    // * time the input is changed.
    private Vector2 rotation = Vector2.zero;

    // the amount of movement that is waiting to be applied
    private Vector3 movement = Vector3.zero;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //specifying collider
        collider = gameObject.GetComponent<CapsuleCollider>();
        //specifying rigidbody
        rigidbody = gameObject.GetComponent<Rigidbody>();

        //* this is how you link the function into the actual press/usage
        //* of the action such as moving the mouse will perform the lookc action.
        //when any input change is detected, this will run
        lookAction.action.performed += OnLookPerformed;
        //sprintAction.action.performed += OnSprintPerformed; // doesn't work
        //crouchAction.action.performed += OnCrouchPerformed; // doesn't work
        //  ended = cancelled, performed, during the process or the process of doing it


        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        sprintAction.action.performed += (_context) => isSprinting = true;
        sprintAction.action.canceled += (_context) => isSprinting = false;
        crouchAction.action.performed += (_context) => isCrouching = true;
        crouchAction.action.canceled += (_context) => isCrouching = false;

        sprintAction.action.GetBindingDisplayString(0, out string device, out string key);
        Debug.Log($"Sprint is mapped to <{device}>/{key}");

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camEuler = CamTransform.eulerAngles;

        //rotates the camera by the the Y axis using the left axis to rotate.
        CamTransform.localRotation = Quaternion.AngleAxis(rotation.y, Vector3.left);
        // rotates game object  by the up axis of the x direction
        transform.localRotation = Quaternion.AngleAxis(rotation.x, Vector3.up);


        // *apply the the movement and reset it to 0
        UpdateMovement();
        transform.position += movement;
        movement = Vector3.zero;
    }

    private void OnLookPerformed(InputAction.CallbackContext _context)
    {
        // * there has been some sort of input update

        // * get the actual value of the input
        Vector2 value = _context.ReadValue<Vector2>();

        rotation.x += value.x * sensitivity;
        rotation.y += value.y * sensitivity;

        // * prevent the vertical look from going outside the specified angle
        rotation.y = Mathf.Clamp(rotation.y, -verticallookCap, verticallookCap);
        
    }

    private void UpdateMovement()
    {
        float speed = defaultSpeed *
            (isCrouching ? crouchSpeedModifier :
                isSprinting ? sprintSpeedModifier : walkSpeedModifier)
                * Time.deltaTime;
        Vector2 value = moveAction.action.ReadValue<Vector2>();
        movement += transform.forward * value.y * speed;
        movement += transform.right * value.x * speed;

    }
    /*
    private void OnSprintPerformed(InputAction.CallbackContext _context)
    {
        // * there has been some sort of input update

        float speed = defaultSpeed;
        float modifier = sprintSpeedModifier;

        // * get the actual value of the input
        Vector3 value = _context.ReadValue<Vector3>();
        movement += transform.right * value.x * modifier;
        movement += transform.forward * value.y * modifier;

    }
    private void OnCrouchPerformed(InputAction.CallbackContext _context)
    {
        // * there has been some sort of input update

        float speed = defaultSpeed;
        float modifier = crouchSpeedModifier;

        // * get the actual value of the input
        Vector3 value = _context.ReadValue<Vector3>();
        movement += transform.right * value.x * modifier;
        movement += transform.forward * value.y * modifier;

    }

    */


}
