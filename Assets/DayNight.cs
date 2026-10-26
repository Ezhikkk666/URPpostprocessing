
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Настройки дня и ночи")]
    public Light sun;

    public float cycleDuration = 120f;
    public float dayIntensity = 1.2f;
    public float nightIntensity = 0.05f;

    public float dayAmbient = 1f;
    public float nightAmbient = 0.15f;

    public bool automaticCycle = true;

    private float timer;
    private bool isDay = true;

    void Start()
    {
        if (sun == null)
            sun = RenderSettings.sun;
    }

    void Update()
    {
        if (automaticCycle)
        {
            timer += Time.deltaTime;

            float angle = timer / cycleDuration * 360f;
            float daylight =
                (1f + Mathf.Cos(angle * Mathf.Deg2Rad)) / 2f;

            ApplyLighting(daylight);

            if (sun != null)
            {
                sun.transform.rotation =
                    Quaternion.Euler(angle, 30f, 0f);
            }
        }
    }

    void ApplyLighting(float daylight)
    {
        if (sun != null)
        {
            sun.intensity = Mathf.Lerp(
                nightIntensity,
                dayIntensity,
                daylight
            );
        }

        RenderSettings.ambientIntensity = Mathf.Lerp(
            nightAmbient,
            dayAmbient,
            daylight
        );
    }

    public void ToggleDayNight()
    {
        automaticCycle = false;
        isDay = !isDay;

        Debug.Log("КНОПКА НАЖАТА!");

        float daylight = isDay ? 1f : 0f;

        if (sun != null)
        {
            sun.transform.rotation = isDay
                ? Quaternion.Euler(50f, 30f, 0f)
                : Quaternion.Euler(180f, 30f, 0f);
        }

        ApplyLighting(daylight);
    }

    public void SetDay()
    {
        automaticCycle = false;
        isDay = true;

        if (sun != null)
            sun.transform.rotation =
                Quaternion.Euler(50f, 30f, 0f);

        ApplyLighting(1f);
    }

    public void SetNight()
    {
        automaticCycle = false;
        isDay = false;

        if (sun != null)
            sun.transform.rotation =
                Quaternion.Euler(180f, 30f, 0f);

        ApplyLighting(0f);
    }
}
