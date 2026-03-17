//using System.Collections;
//using System.Collections.Generic;
//using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.UIElements;


public class Targeter : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private InputAction primaryAction;
    [SerializeField] private InputAction secondaryAction;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionForce;

    private void OnEnable()
    {
        primaryAction.Enable();
        secondaryAction.Enable();
        primaryAction.performed += OnPrimaryClick;
        secondaryAction.performed += OnSecondaryClick;
    }

    private void OnDisable()
    {
        primaryAction.performed -= OnPrimaryClick;
        secondaryAction.performed -= OnSecondaryClick;
        primaryAction.Disable();
        secondaryAction.Disable();
    }

    private void OnPrimaryClick(CallbackContext context)
    {
        Ray ray = new Ray();
        ray.origin = cameraTransform.position;
        ray.direction = cameraTransform.forward;
        bool hit = Physics.Raycast(ray, out RaycastHit hitInfo);
        if (!hit)
            Debug.Log("Not Hit!");
        else
            Debug.Log($"Hit: '{hitInfo.collider.gameObject.name}'");
    }

    private void OnSecondaryClick(CallbackContext context)
    {
        Ray ray = new Ray();
        ray.origin = cameraTransform.position;
        ray.direction = cameraTransform.forward;
        bool hit = Physics.Raycast(ray, out RaycastHit hitInfo);

        Vector3 explostionCenter = hitInfo.point;
        Collider[] colliders = Physics.OverlapSphere(explostionCenter, explosionRadius);

        for (int i = 0; i < colliders.Length; ++i)
        {
            Collider collider = colliders[i];
            if (collider.attachedRigidbody == null)
                continue;
            Vector3 vector = (collider.attachedRigidbody.position - explostionCenter);
            Vector3 direction = vector.normalized;
            float distance = vector.magnitude;
            collider.attachedRigidbody.AddForce(direction * explosionForce);
        }
    }
}

/*public class Targeter : MonoBehaviour
{
   [SerializeField] private Transform cameraTransform;
   [SerializeField] private InputAction primaryAction;

   private void OnEnable()
    {
        primaryAction.Enable();
        primaryAction.performed += OnPrimaryClick;
    }

    private void OnDisable();
    {
        primaryAction.performed -= OnPrimaryClick;
        primaryAction.Disable();
    }

   private void OnPrimaryClick(CallbackContext context)
    {
        Ray ray = new Ray();
        ray.origin = cameraTransform.position;
        ray.direction = cameraTransform.forward;
        bool hit = Physics.Raycast(ray, out RaycastHit hitInfo);
        if (!hit)
            Debug.Log("Not Hit!");
        else
            Debug.Log($"Hit: '{hitInfo.collider.gameObject.name}'");
    }
}*/
