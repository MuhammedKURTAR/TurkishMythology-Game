using UnityEngine;

public class ConversationTrigger : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Animator bileþenini alýyoruz
        animator = GetComponent<Animator>();
    }

    // Oyuncu Trigger bölgesine girdiðinde tetiklenir
    private void OnTriggerEnter(Collider other)
    {
        // Oyuncuyu kontrol etmek için "Player" tag'ini kontrol edin
        if (other.CompareTag("Player"))
        {
            // Konuþma animasyonunu çalýþtýr
            animator.SetTrigger("Talk");
        }
    }
}
