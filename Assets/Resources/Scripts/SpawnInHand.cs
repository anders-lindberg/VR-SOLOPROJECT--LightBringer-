using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;



public class SpawnInHand : MonoBehaviour
{
    [SerializeField] List<GameObject> glowSticks;
    public InputActionReference spawnAction;
    [SerializeField] Transform attachTransform;
    void OnEnable()
    {
        spawnAction.action.performed += Spawn;        
    }

    // Update is called once per frame
    void OnDisable()
    {
        spawnAction.action.performed -= Spawn;
    }
    void Spawn(InputAction.CallbackContext ctx)
    {
        if(glowSticks == null) return;
        var randomGlowstick = Random.Range(0, glowSticks.Count);
        GameObject gobj = Instantiate(glowSticks[randomGlowstick], attachTransform.position, attachTransform.rotation);

        var interactable = gobj.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        interactable.attachTransform = attachTransform;
    }
}
