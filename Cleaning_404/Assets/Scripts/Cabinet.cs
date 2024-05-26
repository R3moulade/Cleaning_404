using System.Collections;
using UnityEngine;

public class Cabinet : MonoBehaviour
{
    private Animation cabinetAnimation;
    private bool isOpen = false;
    private bool isAnimating = false;

    void Start()
    {
        cabinetAnimation = GetComponent<Animation>();
    }

    public void ToggleCabinet()
    {
        if (cabinetAnimation != null && !isAnimating)
        {
            if (isOpen)
            {
                StartCoroutine(PlayAnimation("CabinetDoorClose"));
                //Debug.Log("Close Cabinet");
            }
            else
            {
                StartCoroutine(PlayAnimation("CabinetDoorOpen"));
                //Debug.Log("Open Cabinet");
            }
            isOpen = !isOpen; // Toggle the state
        }
    }

    private IEnumerator PlayAnimation(string animationName)
    {
        isAnimating = true;
        cabinetAnimation.Play(animationName);
        yield return new WaitForSeconds(cabinetAnimation[animationName].length);
        isAnimating = false;
    }
}
