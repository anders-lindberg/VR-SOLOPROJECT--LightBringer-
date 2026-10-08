using UnityEngine;

public class RockTriggerZone : MonoBehaviour
{
    public GlowingRock glowingRock;
    public StarfieldManager starfield;

    private Collider triggerCol;

    void Awake()
    {
        triggerCol = GetComponent<Collider>();
        glowingRock.OnMaterialChanged += OnRockMaterialChanged;
    }

    void OnTriggerEnter(Collider other)
    {
        GlowstickReader stick = other.GetComponent<GlowstickReader>();
        if (stick == null) return;

        if (glowingRock.MatchesMaterial(stick.glowMaterial))
        {
            starfield.ActivateStarfield();
        }
    }

    void OnRockMaterialChanged(Material newMat)
    {
        Collider[] hits = Physics.OverlapBox(
            triggerCol.bounds.center,
            triggerCol.bounds.extents,
            triggerCol.transform.rotation
        );

        foreach (var hit in hits)
        {
            GlowstickReader stick = hit.GetComponent<GlowstickReader>();
            if (stick == null) continue;

            if (glowingRock.MatchesMaterial(stick.glowMaterial))
            {
                starfield.ActivateStarfield();
                return;
            }
        }
    }
}
