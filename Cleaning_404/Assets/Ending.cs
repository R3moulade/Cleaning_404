using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public static Ending Instance { get; private set; } // Singleton instance

    public Camera playerCamera; // set this via inspector
    public bool shake = false;
    public bool fade = false;
    public float shakeAmount = 0.7f;
    public float increaseFactor = 1.0f;
    public Image fadeInOut;
    public AnimationCurve curve;
    public Animation spiderExorcise;
    public GameObject spider;
    public GameObject pentagram;
    public GameObject scarySounds;
    public GameObject flashLight;
    public Material newSkybox;
    public Light sunDirectional;
    public Light sunPoint;
    public Color newAmbientColor;
    public float brightness = 1f;
    public AudioSource birds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {
        if (shake)
        {
            ShakeCamera();
        }
        if (fade)
        {
            StartCoroutine(FadeInOut());
        }
    }

    public void SpiderExorcise(bool exorcise) 
    {
        spiderExorcise.Play();
        shake = true;
        fade = true;
    }

    public void ShakeCamera()
    {
        playerCamera.transform.localPosition = Random.insideUnitSphere * shakeAmount;
        shakeAmount += Time.deltaTime * increaseFactor;
    }

    IEnumerator FadeInOut()
    {
        fade = false;
        float duration = 18f;

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
        Destroy(spider);
        Destroy(pentagram);
        Destroy(scarySounds);
        Destroy(flashLight);
        GameObject candles = GameObject.Find("Candles(Clone)");
        if (candles != null)
        {
            Destroy(candles);
        }
        else
        {
            Debug.Log("No game object named 'candles' found");
        }
        RenderSettings.skybox = newSkybox;
        sunDirectional.enabled = true;
        sunPoint.enabled = true;
        RenderSettings.ambientLight = newAmbientColor * brightness;
        birds.Play();


        duration = 6f; 

        // Fade out with curve
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float alpha = curve.Evaluate(1 - (t / duration));
            fadeInOut.color = new Color(fadeInOut.color.r, fadeInOut.color.g, fadeInOut.color.b, alpha);
            yield return null;
        }
        Door.Instance.ToggleDoor();
    }
}