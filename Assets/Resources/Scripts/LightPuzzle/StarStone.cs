using UnityEngine;
using System.Collections;

public class StarStone : MonoBehaviour
{
    public Material redMat;
    public Material greenMat;
    public Material blueMat;

    public float fadeDuration = 1f;
    public float maxRandomDelay = 0.5f;   // each stone waits up to 0.5s before glowing

    private Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public void ActivateRandomMaterial(float duration)
    {
        Material[] mats = { redMat, greenMat, blueMat };
        Material target = mats[Random.Range(0, mats.Length)];

        StartCoroutine(FadeToMaterial(target, duration));
    }

    IEnumerator FadeToMaterial(Material target, float duration)
    {
        // Random delay before starting the fade
        float delay = Random.Range(0f, maxRandomDelay);
        yield return new WaitForSeconds(delay);

        Material start = rend.sharedMaterial;

        Color startEmission = start.GetColor("_EmissionColor");
        Color targetEmission = target.GetColor("_EmissionColor");

        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float lerp = t / duration;

            Color currentEmission = Color.Lerp(startEmission, targetEmission, lerp);
            rend.sharedMaterial.SetColor("_EmissionColor", currentEmission);

            yield return null;
        }

        // Final swap
        rend.sharedMaterial = target;
    }
}
