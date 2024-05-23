using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animation animation1;
    public Animation animation2;

    public GameObject DirtyDrain;
    public GameObject BloodBath;

    private bool animation1Active = false;
    private bool animation2Active = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == animation1.gameObject)
                {
                    ToggleAnimation1();
                }
            }
        }
    }

    private void ToggleAnimation1()
    {
        if (!animation1Active)
        {
            animation1Active = true;
            // Start animation for animation1
            animation1.Play("Drain");
            // After 3 seconds, start animation2
            animation2.Play("FlushingBathtub");

                Destroy(DirtyDrain, 3f);
                Destroy(BloodBath, 3f);
            
        }
        else
        {
            animation1Active = false;
            // Stop animation for animation1
            animation1.Stop();
        }
    }

    private void StartAnimation2()
    {
        animation2Active = true;
        // Start animation for animation2
        animation2.Play();
    }
}