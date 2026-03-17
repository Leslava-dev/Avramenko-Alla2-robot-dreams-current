using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    [SerializeField] private Collider player;
    //[SerializeField] private Renderer targetRenderer;
    //[SerializeField] private Material inactiveMaterial;
    //[SerializeField] private Material activeMaterial;


    private void OnTriggerEnter(Collider incoming)
    {
        if (incoming != player)
        return;
        //targetRenderer.sharedMaterial = activeMaterial;
        Debug.Log("Player Entered");
    }

    private void OnTriggerExit(Collider incoming)
    {
        if (incoming != player)
        return;
        //targetRenderer.sharedMaterial = inactiveMaterial;
        Debug.Log("Player Exited");
    }
}
