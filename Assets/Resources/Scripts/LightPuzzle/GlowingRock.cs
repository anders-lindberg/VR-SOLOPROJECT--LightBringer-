using UnityEngine;
using System;
using System.Collections;

public class GlowingRock : MonoBehaviour
{
    public Material redMat;
    public Material greenMat;
    public Material blueMat;

    public float cycleDuration = 3f;
    public float fadeDuration = 1f;

    public Material CurrentMaterial { get; private set; }

    public event Action<Material> OnMaterialChanged;

    private Renderer rend;
    private Light rockLight;

    private Material[] mats;
    private int index = 0;
    private float t = 0f;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        rockLight = GetComponentInChildren<Light>();

        mats = new Material[] { redMat, greenMat, blueMat };

        CurrentMaterial = mats[0];
        ApplyInstant(CurrentMaterial);
    }

    void Update()
    {
        t += Time.deltaTime / cycleDuration;

        if (t >= 1f)
        {
            t = 0f;
            index = (index + 1) % mats.Length;

            Material nextMat = mats[index];
            StartCoroutine(FadeToMaterial(nextMat));

            CurrentMaterial = nextMat;
            OnMaterialChanged?.Invoke(CurrentMaterial);
        }
    }

    void ApplyInstant(Material m)
    {
        rend.material = m;
        rockLight.color = m.GetColor("_EmissionColor");
    }

    IEnumerator FadeToMaterial(Material target)
    {
        Material startMat = rend.material;

        Color startEmission = startMat.GetColor("_EmissionColor");
        Color targetEmission = target.GetColor("_EmissionColor");

        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float lerp = time / fadeDuration;

            Color currentEmission = Color.Lerp(startEmission, targetEmission, lerp);

            rend.material.SetColor("_EmissionColor", currentEmission);
            rockLight.color = currentEmission;

            yield return null;
        }

        // Final swap
        rend.material = target;
    }

    // ⭐ Add this missing method
    public bool MatchesMaterial(Material other)
    {
        return other == CurrentMaterial;
    }
}
