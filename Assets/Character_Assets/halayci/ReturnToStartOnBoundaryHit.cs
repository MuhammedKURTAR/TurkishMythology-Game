using UnityEngine;

public class ReturnToStartOnBoundaryHit : MonoBehaviour
{
    public Vector3 centerPoint = Vector3.zero; // Halkan�n merkezi
    public float innerRadius = 3.0f;          // �� s�n�r�n yar��ap�
    public float outerRadius = 5.0f;          // D�� s�n�r�n yar��ap�
    private Vector3 startPosition;            // Ba�lang�� pozisyonu
    private Quaternion startRotation;         // Ba�lang�� rotasyonu

    void Start()
    {
        // Ba�lang�� pozisyonu ve rotasyonu kaydet
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        // Karakterin merkezden uzakl���n� hesapla
        Vector3 offset = transform.position - centerPoint;
        float distanceFromCenter = offset.magnitude;

        // E�er i� s�n�r�n alt�na girerse veya d�� s�n�r�n d���na ��karsa
        if (distanceFromCenter < innerRadius || distanceFromCenter > outerRadius)
        {
            // Karakteri ba�lang�� pozisyonuna ve rotasyonuna d�nd�r
            transform.position = startPosition;
            transform.rotation = startRotation;
        }
    }
}
