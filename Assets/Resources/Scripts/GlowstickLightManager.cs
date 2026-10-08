using UnityEngine;
using System.Collections.Generic;

public class GlowstickLightManager : MonoBehaviour
{
    public int poolSize = 10;
    public float fadeSpeed = 6f;

    private List<Light> pool = new List<Light>();
    private Camera xrCam;

    void Awake()
    {
        xrCam = Camera.main;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = new GameObject("GlowstickLight_" + i);
            Light l = obj.AddComponent<Light>();
            l.type = LightType.Point;
            l.intensity = 0f;
            l.shadows = LightShadows.None;
            l.enabled = true;

            pool.Add(l);
        }
    }

    void Update()
    {
        GlowstickLightReceiver[] sticks = FindObjectsByType<GlowstickLightReceiver>(FindObjectsSortMode.None);

        // Sort by distance to XR camera
        System.Array.Sort(sticks, (a, b) =>
        {
            float da = Vector3.Distance(xrCam.transform.position, a.transform.position);
            float db = Vector3.Distance(xrCam.transform.position, b.transform.position);
            return da.CompareTo(db);
        });

        int index = 0;

        foreach (var stick in sticks)
        {
            if (!stick.isVisible)
                continue;

            if (index >= pool.Count)
                break;

            GlowstickReader gs = stick.GetComponent<GlowstickReader>();
            Light l = pool[index];

            // Copy the glowstick’s actual settings
            l.color = gs.glowColor;
            l.range = gs.glowRange;

            l.transform.position = stick.transform.position;

            // Fade in
            l.intensity = Mathf.Lerp(l.intensity, gs.glowIntensity, Time.deltaTime * fadeSpeed);

            index++;
        }

        // Fade out unused lights
        for (int i = index; i < pool.Count; i++)
        {
            Light l = pool[i];
            l.intensity = Mathf.Lerp(l.intensity, 0f, Time.deltaTime * fadeSpeed);
        }
    }
}
