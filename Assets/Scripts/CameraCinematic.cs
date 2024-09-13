using UnityEngine;

public class CameraCinematic : MonoBehaviour
{
    public Transform point0; // Premier point de contrôle
    public Transform point1; // Deuxième point de contrôle
    public Transform point2; // Troisième point de contrôle
    public Transform point3; // Quatrième point de contrôle
    public float speed = 100f; // Vitesse de la cinématique
    public bool isCinematic = true; // Indique si la cinématique est active
    private float t = 0f; // Progression sur la courbe
    private CameraOrbit cameraOrbitScript; // Référence au script CameraOrbit

    void Start()
    {
        // Désactive CameraOrbit au début de la cinématique
        cameraOrbitScript = GetComponent<CameraOrbit>();
        if (cameraOrbitScript != null)
        {
            cameraOrbitScript.enabled = false;
        }
    }

    void Update()
    {
        if (isCinematic)
        {
            MoveAndOrientAlongBezierCurve();
        }
        else
        {
            // Réactive CameraOrbit après la cinématique
            if (cameraOrbitScript != null && !cameraOrbitScript.enabled)
            {
                cameraOrbitScript.enabled = true;
            }
        }
    }

    void MoveAndOrientAlongBezierCurve()
    {
        // Déplacer la caméra le long de la courbe de Bézier cubique
        Vector3 position = CalculateCubicBezierPoint(t, point0.position, point1.position, point2.position, point3.position);
        transform.position = position;

        // Calculer un point légèrement plus loin pour déterminer la direction
        float lookAtT = Mathf.Clamp(t + 0.01f, 0f, 1f);
        Vector3 lookAtPosition = CalculateCubicBezierPoint(lookAtT, point0.position, point1.position, point2.position, point3.position);

        // Orienter la caméra pour regarder dans la direction du prochain point
        transform.LookAt(lookAtPosition);

        // Incrémenter t pour déplacer la caméra
        t += Time.deltaTime * speed * 4f / Vector3.Distance(point0.position, point3.position);

        // Quand t atteint 1, la cinématique est terminée
        if (t >= 1f)
        {
            t = 1f; // S'assurer que la caméra reste à la fin
            EndCinematic();
        }
    }

    void EndCinematic()
    {
        isCinematic = false;
    }

    // Calcul du point de Bézier cubique
    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return Mathf.Pow(1 - t, 3) * p0 +
               3 * Mathf.Pow(1 - t, 2) * t * p1 +
               3 * (1 - t) * Mathf.Pow(t, 2) * p2 +
               Mathf.Pow(t, 3) * p3;
    }
}
