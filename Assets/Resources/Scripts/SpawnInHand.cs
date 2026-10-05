using UnityEngine;
using UnityEngine.InputSystem;



public class SpawnInHand : MonoBehaviour
{
    [SerializeField] GameObject glowStick;
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
        GameObject gobj = Instantiate(glowStick, attachTransform.position, attachTransform.rotation);

        var interactable = gobj.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        interactable.attachTransform = attachTransform;
    }
}
