using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform sphere;     // La sphère autour de laquelle la caméra gravite
    public Transform center;     // Le centre vers lequel la caméra va toujours regarder
    public float radius = 5f;    // Distance entre la caméra et le centre (rayon initial de la sphère)

    public float sensitivityX = 10f; // Sensibilité pour la rotation horizontale
    public float sensitivityY = 10f; // Sensibilité pour la rotation verticale
    public float zoomSpeed = 2f;     // Vitesse de zoom
    public float minRadius = 2f;     // Rayon minimum de la sphère
    public float maxRadius = 10f;    // Rayon maximum de la sphère

    private float horizontalAngle = 0f; // Angle de rotation autour de l'axe Y
    private float verticalAngle = 0f;   // Angle de rotation autour de l'axe X

    void Update()
    {
        // Récupérer les mouvements de la souris
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;

        // Ajuster les angles en fonction des mouvements de la souris
        horizontalAngle += mouseX;
        verticalAngle -= mouseY;

        // Limiter l'angle vertical pour éviter des rotations incorrectes
        verticalAngle = Mathf.Clamp(verticalAngle, -80f, 80f);

        // Calculer la nouvelle position de la caméra sur la surface de la sphère
        Vector3 newPosition = new Vector3(
            Mathf.Cos(verticalAngle * Mathf.Deg2Rad) * Mathf.Sin(horizontalAngle * Mathf.Deg2Rad),
            Mathf.Sin(verticalAngle * Mathf.Deg2Rad),
            Mathf.Cos(verticalAngle * Mathf.Deg2Rad) * Mathf.Cos(horizontalAngle * Mathf.Deg2Rad)
        );

        // Ajuster le rayon en fonction du zoom
        radius -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        radius = Mathf.Clamp(radius, minRadius, maxRadius);

        // Multiplier par le rayon pour placer la caméra sur les bords de la sphère
        newPosition *= radius;

        // Positionner la caméra à la nouvelle position relative à la sphère
        transform.position = sphere.position + newPosition;

        // Faire regarder la caméra vers le Centre
        transform.LookAt(center);
    }
}
