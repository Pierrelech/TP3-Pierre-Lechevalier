using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight;  // R�f�rence � la Directional Light (Soleil)
    public float dayDuration = 60f;  // Dur�e d'une journ�e compl�te en secondes (r�glable)
    public float minIntensity = 0.1f;  // Intensit� minimale de la lumi�re pendant la nuit
    public float maxIntensity = 1.0f;  // Intensit� maximale de la lumi�re pendant la journ�e

    private float time;  // Repr�sente le temps �coul� dans la journ�e
    private float timeMultiplier;  // Multiplicateur pour convertir en heures virtuelles (24 heures)

    void Start()
    {
        // Initialiser le temps et le multiplicateur (divis� par 24 pour un cycle complet)
        timeMultiplier = 24f / dayDuration;
    }

    void Update()
    {
        // Incr�menter le temps en fonction du temps r�el et du multiplicateur
        time += Time.deltaTime * timeMultiplier;

        // Remettre � z�ro le temps apr�s 24 heures
        if (time >= 24f)
        {
            time = 0f;
        }

        // Calculer la rotation de la Directional Light pour simuler le lever et coucher du soleil
        float sunAngle = (time / 24f) * 360f - 90f;  // Un cycle complet de 360 degr�s pour 24 heures
        directionalLight.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);

        // Ajuster l'intensit� de la lumi�re en fonction du temps (jour ou nuit)
        if (time > 6f && time < 18f)  // De 6h � 18h, c'est le jour
        {
            // Augmenter l'intensit� progressivement jusqu'� 12h (midi) et diminuer apr�s
            float t = Mathf.InverseLerp(6f, 12f, time);
            directionalLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
        }
        else  // De 18h � 6h, c'est la nuit
        {
            float t = Mathf.InverseLerp(18f, 24f, time > 18f ? time : time + 24f);
            directionalLight.intensity = Mathf.Lerp(maxIntensity, minIntensity, t);
        }
    }
}
