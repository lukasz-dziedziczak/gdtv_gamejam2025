using System;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] Collider[] colliders;
    [SerializeField] Transform root;

    private void Start()
    {
        TurnOff();
    }

    public void TurnOff()
    {
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
            if (collider.attachedRigidbody != null) 
                collider.attachedRigidbody.isKinematic = true;
        }
    }

    public void TurnOn()
    {
        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
            if (collider.attachedRigidbody != null) 
                collider.attachedRigidbody.isKinematic = false;
            collider.attachedRigidbody.ResetInertiaTensor();
        }
        //colliders[0].attachedRigidbody.AddForce(transform.up * 1000.0f, ForceMode.Impulse);

        Vector3 pos = root.position;
        pos.y += 0.1f;
        root.position = pos;
    }
}
