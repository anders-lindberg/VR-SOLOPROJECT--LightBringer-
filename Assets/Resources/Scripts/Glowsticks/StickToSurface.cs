using UnityEngine;

public class StickToSurface : MonoBehaviour
{
    private bool hasAttached = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Glowstick"))
        {
            if(hasAttached) return;
        Rigidbody rb = GetComponent<Rigidbody>();
        CapsuleCollider cc = GetComponent<CapsuleCollider>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        //cc.enabled = false;

        hasAttached = true;
        }
        

    }
}
