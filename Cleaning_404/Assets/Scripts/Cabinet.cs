using UnityEngine;

public class Cabinet : MonoBehaviour
{
    private Animation cabinetAnimation;
    private bool isOpen = false;

    void Start()
    {
        cabinetAnimation = GetComponent<Animation>();
    }

    public void ToggleCabinet()
    {
        if (cabinetAnimation != null)
        {
            if (isOpen)
            {
                cabinetAnimation.Play("CabinetDoorClose");
            }
            else
            {
                cabinetAnimation.Play("CabinetDoorOpen");
            }
            isOpen = !isOpen;
        }
    }
}