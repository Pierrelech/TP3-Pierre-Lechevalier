using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform sphere;     // La sph�re autour de laquelle la cam�ra gravite
    public Transform center;     // Le centre vers lequel la cam�ra va toujours regarder
    public float radius = 5f;    // Distance entre la cam�ra et le centre (rayon initial de la sph�re)

    public float sensitivityX = 10f; // Sensibilit� pour la rotation horizontale
    public float sensitivityY = 10f; // Sensibilit� pour la rotation verticale
    public float zoomSpeed = 2f;     // Vitesse de zoom
    public float minRadius = 2f;     // Rayon minimum de la sph�re
    public float maxRadius = 10f;    // Rayon maximum de la sph�re

    private float horizontalAngle = 0f; // Angle de rotation autour de l'axe Y
    private float verticalAngle = 0f;   // Angle de rotation autour de l'axe X

    void Update()
    {
        // R�cup�rer les mouvements de la souris
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;

        // Ajuster les angles en fonction des mouvements de la souris
        horizontalAngle += mouseX;
        verticalAngle -= mouseY;

        // Limiter l'angle vertical pour �viter des rotations incorrectes
        verticalAngle = Mathf.Clamp(verticalAngle, -80f, 80f);

        // Calculer la nouvelle position de la cam�ra sur la surface de la sph�re
        Vector3 newPosition = new Vector3(
            Mathf.Cos(verticalAngle * Mathf.Deg2Rad) * Mathf.Sin(horizontalAngle * Mathf.Deg2Rad),
            Mathf.Sin(verticalAngle * Mathf.Deg2Rad),
            Mathf.Cos(verticalAngle * Mathf.Deg2Rad) * Mathf.Cos(horizontalAngle * Mathf.Deg2Rad)
        );

        // Ajuster le rayon en fonction du zoom
        radius -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        radius = Mathf.Clamp(radius, minRadius, maxRadius);

        // Multiplier par le rayon pour placer la cam�ra sur les bords de la sph�re
        newPosition *= radius;

        // Positionner la cam�ra � la nouvelle position relative � la sph�re
        transform.position = sphere.position + newPosition;

        // Faire regarder la cam�ra vers le Centre
        transform.LookAt(center);
    }
}
