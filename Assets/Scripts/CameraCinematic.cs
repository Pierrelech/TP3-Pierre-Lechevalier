using UnityEngine;

public class CameraCinematic : MonoBehaviour
{
    public Transform point0; // Premier point de contr�le
    public Transform point1; // Deuxi�me point de contr�le
    public Transform point2; // Troisi�me point de contr�le
    public Transform point3; // Quatri�me point de contr�le
    public float speed = 100f; // Vitesse de la cin�matique
    public bool isCinematic = true; // Indique si la cin�matique est active
    private float t = 0f; // Progression sur la courbe
    private CameraOrbit cameraOrbitScript; // R�f�rence au script CameraOrbit

    void Start()
    {
        // D�sactive CameraOrbit au d�but de la cin�matique
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
            // R�active CameraOrbit apr�s la cin�matique
            if (cameraOrbitScript != null && !cameraOrbitScript.enabled)
            {
                cameraOrbitScript.enabled = true;
            }
        }
    }

    void MoveAndOrientAlongBezierCurve()
    {
        // D�placer la cam�ra le long de la courbe de B�zier cubique
        Vector3 position = CalculateCubicBezierPoint(t, point0.position, point1.position, point2.position, point3.position);
        transform.position = position;

        // Calculer un point l�g�rement plus loin pour d�terminer la direction
        float lookAtT = Mathf.Clamp(t + 0.01f, 0f, 1f);
        Vector3 lookAtPosition = CalculateCubicBezierPoint(lookAtT, point0.position, point1.position, point2.position, point3.position);

        // Orienter la cam�ra pour regarder dans la direction du prochain point
        transform.LookAt(lookAtPosition);

        // Incr�menter t pour d�placer la cam�ra
        t += Time.deltaTime * speed * 4f / Vector3.Distance(point0.position, point3.position);

        // Quand t atteint 1, la cin�matique est termin�e
        if (t >= 1f)
        {
            t = 1f; // S'assurer que la cam�ra reste � la fin
            EndCinematic();
        }
    }

    void EndCinematic()
    {
        isCinematic = false;
    }

    // Calcul du point de B�zier cubique
    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return Mathf.Pow(1 - t, 3) * p0 +
               3 * Mathf.Pow(1 - t, 2) * t * p1 +
               3 * (1 - t) * Mathf.Pow(t, 2) * p2 +
               Mathf.Pow(t, 3) * p3;
    }
}
