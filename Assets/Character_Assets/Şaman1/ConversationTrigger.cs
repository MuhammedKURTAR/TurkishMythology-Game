using UnityEngine;

public class ConversationTrigger : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Animator bile�enini al�yoruz
        animator = GetComponent<Animator>();
    }

    // Oyuncu Trigger b�lgesine girdi�inde tetiklenir
    private void OnTriggerEnter(Collider other)
    {
        // Oyuncuyu kontrol etmek i�in "Player" tag'ini kontrol edin
        if (other.CompareTag("Player"))
        {
            // Konu�ma animasyonunu �al��t�r
            animator.SetTrigger("Talk");
        }
    }
}
