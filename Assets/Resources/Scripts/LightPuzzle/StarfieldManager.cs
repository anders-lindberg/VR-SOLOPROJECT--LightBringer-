using UnityEngine;

public class StarfieldManager : MonoBehaviour
{
    public StarStone[] stones;
    public float activationDuration = 2f;

    public void ActivateStarfield()
    {
        foreach (var stone in stones)
        {
            stone.ActivateRandomMaterial(activationDuration);
        }
    }
}
