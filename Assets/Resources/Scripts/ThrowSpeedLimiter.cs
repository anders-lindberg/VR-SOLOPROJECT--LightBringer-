using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ThrowSpeedLimiter : MonoBehaviour
{
    public float maxLinearVelocity = 8f;

    private XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectExited.AddListener(OnReleased);
    }

    void OnReleased(SelectExitEventArgs args)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb.linearVelocity.magnitude > maxLinearVelocity)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxLinearVelocity;
        }
    }
}
