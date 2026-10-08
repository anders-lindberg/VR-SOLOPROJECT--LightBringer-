using UnityEngine;
using System.Collections;

public class StarStone : MonoBehaviour
{
    public Material redMat;
    public Material greenMat;
    public Material blueMat;

    public float fadeDuration = 1f;
    public float maxRandomDelay = 0.5f;

    private Renderer rend;
    private Material runtimeMat;

    void Awake()
    {
        rend = GetComponent<Renderer>();

        // Clone the starting material so we never modify the asset
        runtimeMat = new Material(rend.sharedMaterial);
        rend.material = runtimeMat;

        // Start fully dark (no emission)
        runtimeMat.SetColor("_EmissionColor", Color.black);
    }

    public void ActivateRandomMaterial(float duration)
    {
        Material[] mats = { redMat, greenMat, blueMat };
        Material target = mats[Random.Range(0, mats.Length)];

        StartCoroutine(FadeToMaterial(target, duration));
    }

    IEnumerator FadeToMaterial(Material target, float duration)
    {
        float delay = Random.Range(0f, maxRandomDelay);
        yield return new WaitForSeconds(delay);

        // Start from whatever emission the runtimeMat currently has
        Color startEmission = runtimeMat.GetColor("_EmissionColor");
        Color targetEmission = target.GetColor("_EmissionColor");

        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float lerp = t / duration;

            Color currentEmission = Color.Lerp(startEmission, targetEmission, lerp);
            runtimeMat.SetColor("_EmissionColor", currentEmission);

            yield return null;
        }

        // Finalize runtime material to match target
        runtimeMat.CopyPropertiesFromMaterial(target);
    }
}
