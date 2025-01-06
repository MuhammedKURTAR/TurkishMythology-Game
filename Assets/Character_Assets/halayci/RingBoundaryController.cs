using UnityEngine;

public class RingBoundaryController : MonoBehaviour
{
    public Vector3 centerPoint = Vector3.zero; // Halkan�n merkezi
    public float innerRadius = 3.0f;          // �� s�n�r�n yar��ap�
    public float outerRadius = 5.0f;          // D�� s�n�r�n yar��ap�
    public float rotationSpeed = 5.0f;        // Yumu�ak d�n�� h�z�
    private Quaternion targetRotation;        // Hedef rotasyon
    private bool shouldRotate = false;        // D�n�� kontrol�

    void Update()
    {
        // Karakterin merkezden uzakl���n� hesapla
        Vector3 offset = transform.position - centerPoint;
        float distanceFromCenter = offset.magnitude;

        // E�er i� s�n�r�n alt�na girerse veya d�� s�n�r�n d���na ��karsa
        if (distanceFromCenter < innerRadius || distanceFromCenter > outerRadius)
        {
            // Y ekseninde 180 derece d�nmesi i�in hedef rotasyonu belirle
            targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + 180, 0);
            shouldRotate = true; // D�n�� sinyalini etkinle�tir

            // Karakteri s�n�rlar�n i�inde kalacak �ekilde geri �ek
            if (distanceFromCenter < innerRadius)
            {
                // �� s�n�rda kalacak �ekilde ayarla
                transform.position = centerPoint + offset.normalized * innerRadius;
            }
            else if (distanceFromCenter > outerRadius)
            {
                // D�� s�n�rda kalacak �ekilde ayarla
                transform.position = centerPoint + offset.normalized * outerRadius;
            }
        }

        // E�er d�n�� yap�lmas� gerekiyorsa
        if (shouldRotate)
        {
            // Karakteri yava��a hedef rotasyona d�nd�r
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // Hedef rotasyona ula��ld���nda d�n��� durdur
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                shouldRotate = false;
            }
        }
    }
}
