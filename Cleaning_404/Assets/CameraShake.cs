using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraShake : MonoBehaviour
{
    public Camera playerCamera; // set this via inspector
    public bool shake = true;
    public bool fade = true;
    public float shakeAmount = 0.7f;
    public float increaseFactor = 1.0f;
    public Image fadeInOut;
    public AnimationCurve curve;
    void Update()
    {
        if (shake) {
            ShakeCamera();
        
        }
        if (fade) {
            StartCoroutine(FadeInOut());
        }
    }

    public void ShakeCamera()
    {
        playerCamera.transform.localPosition = Random.insideUnitSphere * shakeAmount;
        shakeAmount += Time.deltaTime * increaseFactor;
    }

    IEnumerator FadeInOut()
    {
        fade = false;
        float duration = 7f; // Fade in over 6 seconds

        // Fade in with curve
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float alpha = curve.Evaluate(t / duration);
            fadeInOut.color = new Color(fadeInOut.color.r, fadeInOut.color.g, fadeInOut.color.b, alpha);
            yield return null;
        }

        // Keep it white for 4 seconds
        shake = false;
        yield return new WaitForSeconds(4);

        // Fade out with curve
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float alpha = curve.Evaluate(1 - (t / duration));
            fadeInOut.color = new Color(fadeInOut.color.r, fadeInOut.color.g, fadeInOut.color.b, alpha);
            yield return null;
        }
    }
}