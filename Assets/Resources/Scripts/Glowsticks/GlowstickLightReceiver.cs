using UnityEngine;

public class GlowstickLightReceiver : MonoBehaviour
{
    public bool isVisible;
    private Camera xrCam;

    void Start()
    {
        xrCam = Camera.main; // XR camera is always MainCamera
    }

    void Update()
    {
        if (!xrCam) return;

        Vector3 vp = xrCam.WorldToViewportPoint(transform.position);

        isVisible =
            vp.z > 0 &&
            vp.x > 0 && vp.x < 1 &&
            vp.y > 0 && vp.y < 1;
    }
}
