using UnityEngine;
using UnityEngine.UI;

public class DialogKoylu : MonoBehaviour
{
    public Image targetImage; // A�?p kapatmak istedi?iniz Image referans?

    private void Start()
    {
        if (targetImage != null)
        {
            targetImage.gameObject.SetActive(false); // Ba?lang?�ta kapal? olsun
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && targetImage != null) // Oyuncunun etiketi "Player" olmal?
        {
            targetImage.gameObject.SetActive(true); // G�r�n�r yap
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && targetImage != null)
        {
            targetImage.gameObject.SetActive(false); // G�r�nmez yap
        }
    }
}
