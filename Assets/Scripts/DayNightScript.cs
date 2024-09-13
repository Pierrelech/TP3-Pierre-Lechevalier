using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight;  // Référence à la Directional Light (Soleil)
    public float dayDuration = 60f;  // Durée d'une journée complète en secondes (réglable)
    public float minIntensity = 0.1f;  // Intensité minimale de la lumière pendant la nuit
    public float maxIntensity = 1.0f;  // Intensité maximale de la lumière pendant la journée

    private float time;  // Représente le temps écoulé dans la journée
    private float timeMultiplier;  // Multiplicateur pour convertir en heures virtuelles (24 heures)

    void Start()
    {
        // Initialiser le temps et le multiplicateur (divisé par 24 pour un cycle complet)
        timeMultiplier = 24f / dayDuration;
    }

    void Update()
    {
        // Incrémenter le temps en fonction du temps réel et du multiplicateur
        time += Time.deltaTime * timeMultiplier;

        // Remettre à zéro le temps après 24 heures
        if (time >= 24f)
        {
            time = 0f;
        }

        // Calculer la rotation de la Directional Light pour simuler le lever et coucher du soleil
        float sunAngle = (time / 24f) * 360f - 90f;  // Un cycle complet de 360 degrés pour 24 heures
        directionalLight.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);

        // Ajuster l'intensité de la lumière en fonction du temps (jour ou nuit)
        if (time > 6f && time < 18f)  // De 6h à 18h, c'est le jour
        {
            // Augmenter l'intensité progressivement jusqu'à 12h (midi) et diminuer après
            float t = Mathf.InverseLerp(6f, 12f, time);
            directionalLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
        }
        else  // De 18h à 6h, c'est la nuit
        {
            float t = Mathf.InverseLerp(18f, 24f, time > 18f ? time : time + 24f);
            directionalLight.intensity = Mathf.Lerp(maxIntensity, minIntensity, t);
        }
    }
}
