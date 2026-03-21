using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastShoot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera fpsCam;
    [SerializeField] private Transform gunEnd;
    [SerializeField] private InputAction primaryAction;
    [SerializeField] private LineRenderer laserLine;
    [SerializeField] private AudioSource gunAudio;

    [Header("Settings")]
    [SerializeField] private float fireRate = 0.25f;
    [SerializeField] private float weaponRange = 50f;
    [SerializeField] private float hitForce = 100f;
    [SerializeField] private LayerMask hitMask = ~0;

    private readonly WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private float nextFire;

    private void OnEnable()
    {
        primaryAction.Enable();
        primaryAction.performed += OnPrimaryActionPerformed;
    }

    private void OnDisable()
    {
        primaryAction.performed -= OnPrimaryActionPerformed;
        primaryAction.Disable();
    }

    private void Start()
    {
        if (laserLine != null)
            laserLine.enabled = false;
    }

    private void OnPrimaryActionPerformed(InputAction.CallbackContext context)
    {
        if (Time.time <= nextFire)
            return;

        nextFire = Time.time + fireRate;
        Shoot();
    }

    private void Shoot()
    {
        StartCoroutine(ShotEffect());

        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (laserLine != null)
            laserLine.SetPosition(0, gunEnd.position);

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange, hitMask, QueryTriggerInteraction.Ignore))
        {
            if (laserLine != null)
                laserLine.SetPosition(1, hit.point);

            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * hitForce, ForceMode.Impulse);

            Debug.Log($"Hit: '{hit.collider.gameObject.name}'");
        }
        else
        {
            Vector3 endPoint = rayOrigin + (fpsCam.transform.forward * weaponRange);

            if (laserLine != null)
                laserLine.SetPosition(1, endPoint);

            Debug.Log("Not Hit!");
        }
    }

    private IEnumerator ShotEffect()
    {
        if (gunAudio != null)
            gunAudio.Play();

        if (laserLine != null)
            laserLine.enabled = true;

        yield return shotDuration;

        if (laserLine != null)
            laserLine.enabled = false;
    }
}
