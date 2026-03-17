//using System.Collections;
//using System.Collections.Generic;
//using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lection12.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Transform cameraAnchor;
        [SerializeField] private float speed;
        [SerializeField] InputAction horizontalAxis;
        [SerializeField] InputAction verticalAxis;
        [SerializeField] private InputAction lookHorizontal;
        [SerializeField] private InputAction lookVertical;

        [Header("Look")]
        [SerializeField] private float sensitivity;

[Header("Exposed debug stuff")]         
[SerializeField] private Vector2 input;
[SerializeField] private Vector3 velocity;
[SerializeField] private float yaw;
[SerializeField] private float pitch;

/// This function is called when the object becomes enabled and active.
/// </summary>
private void OnEnable()
{
    horizontalAxis.Enable();
    verticalAxis.Enable();
    lookHorizontal.Enable();
    lookVertical.Enable();
}

/// This function is called when the behaviour becomes disabled or inactive.
/// </summary>
private void OnDisable()
{
    horizontalAxis.Disable();
    verticalAxis.Disable();
    lookHorizontal.Disable();
    lookVertical.Disable();
}
private void Update()
        {
    input.x = horizontalAxis.ReadValue<float>(); 
    input.y = verticalAxis.ReadValue<float>();

    float mouseX = lookHorizontal.ReadValue<float>();
    float mouseY = lookVertical.ReadValue<float>();

    yaw += mouseX * sensitivity * Time.deltaTime;
    pitch -= mouseY * sensitivity * Time.deltaTime;

    pitch = Mathf.Clamp(pitch, -40f, 80f);

    transform.rotation = Quaternion.Euler(0f, yaw, 0f);
    cameraAnchor.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }

        private void FixedUpdate()
        {
        velocity = transform.right * input.x + transform.forward * input.y;
        characterController.SimpleMove(velocity * speed);

        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
{
    Rigidbody body = hit.collider.attachedRigidbody;

    if (body == null || body.isKinematic)
        return;

    Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0f, hit.moveDirection.z);
    body.velocity = pushDirection * 2f;
}
    }

  
    //[RequireComponent(typeof(CharacterController))]
     /*public class PlayerController : MonoBehaviour
     {
         [SerializeField] private CharacterController characterController;
         [SerializeField] private Transform cameraAnchor;

         [Header("Movement")]
         [SerializeField] private float speed = 6f;
         [SerializeField] private float gravity = -9.81f;

         [Header("Look")]
         [SerializeField] private float sensitivity = 120f;

         [Header("Input")]
         [SerializeField] private InputAction horizontalAxis;
         [SerializeField] private InputAction verticalAxis;
         [SerializeField] private InputAction lookHorizontal;
         [SerializeField] private InputAction lookVertical;

         [Header("Debug")]
         [SerializeField] private Vector2 input;
         [SerializeField] private Vector3 velocity;
         [SerializeField] private float yaw;
         [SerializeField] private float pitch = 20f;

         private float verticalVelocity;

         private void OnEnable()
         {
             horizontalAxis.Enable();
             verticalAxis.Enable();
             lookHorizontal.Enable();
             lookVertical.Enable();
         }

         private void OnDisable()
         {
             horizontalAxis.Disable();
             verticalAxis.Disable();
             lookHorizontal.Disable();
             lookVertical.Disable();
         }

         private void Update()
         {
             HandleLook();
             HandleMovement();
         }

         private void HandleLook()
         {
             float mouseX = lookHorizontal.ReadValue<float>();
             float mouseY = lookVertical.ReadValue<float>();

             yaw += mouseX * sensitivity * Time.deltaTime;
             pitch -= mouseY * sensitivity * Time.deltaTime;

             pitch = Mathf.Clamp(pitch, -40f, 80f);

             transform.rotation = Quaternion.Euler(0f, yaw, 0f);
             cameraAnchor.localRotation = Quaternion.Euler(pitch, 0f, 0f);
         }

         private void HandleMovement()
         {
             input.x = horizontalAxis.ReadValue<float>();
             input.y = verticalAxis.ReadValue<float>();

             Vector3 move = transform.right * input.x +
                            transform.forward * input.y;

             if (characterController.isGrounded && verticalVelocity < 0)
                 verticalVelocity = -2f;

             verticalVelocity += gravity * Time.deltaTime;

             velocity = move * speed;
             velocity.y = verticalVelocity;

             characterController.Move(velocity * Time.deltaTime);
         }

         private void OnApplicationFocus(bool focus)
         {
             Cursor.lockState = focus ? CursorLockMode.Locked : CursorLockMode.None;
             Cursor.visible = !focus;
         }
     }*/
}