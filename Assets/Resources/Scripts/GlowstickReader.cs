using UnityEngine;

public class GlowstickReader : MonoBehaviour
{
    public Color glowColor {get; private set;}
    public float glowIntensity{get; private set;}
    public float glowRange {get; private set;}

    [SerializeField] Renderer rend;
    [SerializeField] Light prefabLight;

    void Awake()
    {
        // Read the prefab’s actual Light settings
        glowColor = prefabLight.color;
        glowIntensity = prefabLight.intensity;
        glowRange = prefabLight.range;
    }

    void Start()
    {
        if (!rend)
            rend = GetComponent<Renderer>();

        // Read emission from the prefab’s material
        Color emissionColor = rend.material.GetColor("_EmissionColor");

        // Ensure emission stays correct
        rend.material.EnableKeyword("_EMISSION");
        rend.material.SetColor("_EmissionColor", emissionColor);
    }
}
