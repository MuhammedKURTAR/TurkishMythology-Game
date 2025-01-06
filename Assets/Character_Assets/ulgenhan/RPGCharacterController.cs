using UnityEngine;

public class RPGCharacterController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Y�r�me h�z�
    public float runSpeed = 10.0f; // Ko�ma h�z�
    public float rotationSpeed = 10.0f; // Karakterin d�n�� h�z�
    public Transform cameraTransform; // Kameran�n Transform'u
    private Animator animator;
    private Rigidbody rb;

    private bool isAttacking = false; // Sald�r� durumunu takip eder

    void Start()
    {
        // Animator ve Rigidbody bile�enlerini al
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        // Fare imlecini gizle ve kilitle
        Cursor.lockState = CursorLockMode.Locked; // �mleci ekran�n ortas�na kilitle
        Cursor.visible = false; // �mleci g�r�nmez yap
    }

    void Update()
    {
        // E�er sald�r� yap�l�yorsa veya UI a��k durumdaysa di�er i�lemleri engelle
        if (isAttacking || IsDialogueActive())
            return;

        float currentSpeed = moveSpeed; // Varsay�lan olarak y�r�me h�z�

        // Sol fare t�kland���nda sald�r� animasyonunu ba�lat
        if (Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
            animator.Play("Armature|Armature|Left_Slash|baselayer");
            Invoke("EndAttack", 1.5f); // Sald�r� animasyonu s�resine g�re bir gecikme ayarla
            return;
        }

        // W tu�una bas�l�ysa
        if (Input.GetKey(KeyCode.W))
        {
            // E�er Shift tu�una da bas�l�ysa ko�ma moduna ge�
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = runSpeed;

                // Ko�ma animasyonunu ba�lat
                animator.Play("Armature|Armature|Run_03|baselayer");
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", true);
            }
            else
            {
                // Y�r�me animasyonunu ba�lat
                animator.Play("Armature|Armature|walking_man|baselayer");
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
            }

            // Kameran�n y�n�ne g�re hareket
            Vector3 forward = cameraTransform.forward;
            forward.y = 0; // Y eksenini s�f�rla (d�z zemin hareketi)
            forward.Normalize(); // Y�n� normalize et

            // Karakterin y�n�n� kameran�n y�n�ne do�ru d�nd�r
            Quaternion targetRotation = Quaternion.LookRotation(forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Karakteri ileri do�ru hareket ettir
            Vector3 movement = forward * currentSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
        }
        else
        {
            // Bo�ta kalma animasyonuna ge�
            animator.Play("Armature|Armature|Combat_Stance|baselayer");
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
    }

    // Sald�r� bitti�inde �a�r�l�r
    void EndAttack()
    {
        isAttacking = false;
    }

    // UI Paneli a��k m� kontrol eder
    bool IsDialogueActive()
    {
        GameObject dialoguePanel = GameObject.Find("Panel"); // Panel ad�n� kontrol edin
        return dialoguePanel != null && dialoguePanel.activeSelf;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shahmeran"))
        {
            // Animasyonu idle'a ge�ir
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            animator.Play("Armature|Armature|Combat_Stance|baselayer"); // Idle animasyonu
        }
    }

}
