using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BezierCurve : MonoBehaviour
{
    public Transform point0; // Premier point de contr�le
    public Transform point1; // Deuxi�me point de contr�le (Quadratique)
    public Transform point2; // Troisi�me point de contr�le (Quadratique et Cubique)
    public Transform point3; // Quatri�me point de contr�le (Cubique uniquement)
    public int numPoints = 50; // Nombre de points pour dessiner la courbe
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numPoints;
    }

    void Update()
    {
        if (point3 == null)
        {
            DrawQuadraticBezierCurve();
        }
        else
        {
            DrawCubicBezierCurve();
        }
    }

    // Courbe de B�zier quadratique
    void DrawQuadraticBezierCurve()
    {
        for (int i = 0; i < numPoints; i++)
        {
            float t = i / (float)(numPoints - 1);
            Vector3 point = CalculateQuadraticBezierPoint(t, point0.position, point1.position, point2.position);
            lineRenderer.SetPosition(i, point);
        }
    }

    // Courbe de B�zier cubique
    void DrawCubicBezierCurve()
    {
        for (int i = 0; i < numPoints; i++)
        {
            float t = i / (float)(numPoints - 1);
            Vector3 point = CalculateCubicBezierPoint(t, point0.position, point1.position, point2.position, point3.position);
            lineRenderer.SetPosition(i, point);
        }
    }

    // Calcul du point de B�zier quadratique
    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        return Mathf.Pow(1 - t, 2) * p0 + 2 * (1 - t) * t * p1 + Mathf.Pow(t, 2) * p2;
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
