using UnityEngine;

public class MoveBackwardOnAnimation : MonoBehaviour
{
    public Animator animator; // Karakterin Animator bile�eni
    public float moveSpeed = 2.0f; // Geri hareket h�z�

    private void Update()
    {
        // Animator'�n �u anki animasyon durumunu kontrol ediyoruz
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // E�er "BackLeft_run" animasyonu oynat�l�yorsa
        if (stateInfo.IsName("Armature|Armature|BackLeft_run|baselayer"))
        {
            // Karakteri geri hareket ettir
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
    }
}
