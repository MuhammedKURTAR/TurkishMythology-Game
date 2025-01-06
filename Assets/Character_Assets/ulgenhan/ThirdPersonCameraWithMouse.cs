using UnityEngine;

public class ThirdPersonCameraWithMouse : MonoBehaviour
{
    public Transform target; // Karakterin Transform'u
    public float distance = 4.0f; // Kameran�n hedefe olan uzakl���
    public float height = 2.0f;   // Kameran�n hedefe olan y�ksekli�i
    public float rotationSpeed = 5.0f; // Fare hareketine duyarl�l�k
    public float minHeight = 1.0f;     // Kameran�n yere yakla�abilece�i minimum y�kseklik
    public float maxHeight = 5.0f;     // Kameran�n ��kabilece�i maksimum y�kseklik

    private float currentYaw = 0.0f;   // Yatay d�nd�rme a��s�
    private float currentPitch = 0.0f; // Dikey d�nd�rme a��s�

    void LateUpdate()
    {
        // Fare hareketine g�re a��y� g�ncelle
        currentYaw += Input.GetAxis("Mouse X") * rotationSpeed;
        currentPitch -= Input.GetAxis("Mouse Y") * rotationSpeed;

        // Dikey a��y� s�n�rla (yukar� ve a�a�� d�nmeyi kontrol eder)
        currentPitch = Mathf.Clamp(currentPitch, -30f, 60f); // Dikey a�� s�n�rlar� (�rnek de�erler)

        // Kameran�n hedef pozisyonunu hesapla
        Vector3 offset = Quaternion.Euler(currentPitch, currentYaw, 0) * Vector3.back * distance;
        Vector3 desiredPosition = target.position + offset;

        // Kameran�n y�ksekli�ini s�n�rla
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, target.position.y + minHeight, target.position.y + maxHeight);

        // Kameray� hedef pozisyona ta��
        transform.position = desiredPosition;

        // Kameray� hedefe do�ru �evir
        transform.LookAt(target);
    }
}
