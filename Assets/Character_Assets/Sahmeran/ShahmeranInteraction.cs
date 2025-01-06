using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShahmeranInteraction : MonoBehaviour
{
    public GameObject dialoguePanel; // Sohbet paneli
    public TextMeshProUGUI dialogueText; // Sohbet ge�mi�ini g�steren metin alan�
    public TMP_InputField playerInputField; // Oyuncunun mesaj�n� yazd��� Input Field
    public OpenAIIntegration aiIntegration; // Yapay zeka entegrasyonu

    private string currentRiddle; // �u anki bilmece
    private string correctAnswer; // Do�ru cevap
    private bool abilityUnlocked = false; // Yetenek kazan�ld� m�?
    private bool riddleAsked = false; // Bilmece soruldu mu?
    private int incorrectAttempts = 0; // Yanl�� cevap say�s�
    private const int maxAttempts = 3; // Maksimum deneme hakk�

    private List<string> chatHistory = new List<string>(); // Sohbet ge�mi�ini tutar

    private enum ConversationState { Introduction, Riddle, Reward }
    private ConversationState currentState = ConversationState.Introduction;

    void Start()
    {
        aiIntegration = FindObjectOfType<OpenAIIntegration>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialoguePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            StartConversation();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CloseDialogue();
    }

    public void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SendPlayerMessage()
    {
        string playerMessage = playerInputField.text.Trim();
        if (!string.IsNullOrEmpty(playerMessage))
        {
            playerInputField.text = "";
            AddToChatHistory("�lgen Han: " + playerMessage); // Oyuncunun mesaj�n� ge�mi�e ekle
            ProcessPlayerMessage(playerMessage);
        }
    }

    private void StartConversation()
    {
        string introductionMessage = "Ho� geldin, �lgen Han. Ben �ahmeran, T�rk mitolojisinin bilgeli�ini temsil ediyorum. Sana Erlik'i yenmende yard�mc� olaca��m.";
        AddToChatHistory("�ahmeran: " + introductionMessage);
        currentState = ConversationState.Introduction;
    }

    private void ProcessPlayerMessage(string playerMessage)
    {
        switch (currentState)
        {
            case ConversationState.Introduction:
                HandleIntroduction(playerMessage);
                break;
            case ConversationState.Riddle:
                HandleRiddle(playerMessage);
                break;
            case ConversationState.Reward:
                AddToChatHistory("�ahmeran: Yolun a��k olsun, �lgen Han.");
                break;
        }
    }

    private void HandleIntroduction(string playerMessage)
    {
        if (playerMessage.ToLower().Contains("erlik"))
        {
            string message = "Erlik'i yenmek i�in bilgeli�imi test etmelisin. Sana bir bilmece soraca��m.";
            AddToChatHistory("�ahmeran: " + message);
            AskRiddle();
            currentState = ConversationState.Riddle;
        }
        else
        {
            string message = "�zg�n�m, bu konuda yard�mc� olamam. Bana Erlik'i yenmekle ilgili sorular sorabilirsin.";
            AddToChatHistory("�ahmeran: " + message);
        }
    }

    private void AskRiddle()
    {
        if (!riddleAsked)
        {
            aiIntegration.StartCoroutine(aiIntegration.SendMessageToAI("T�rk mitolojisiyle ilgili bir bilmece sor.", OnRiddleReceived));
            riddleAsked = true;
        }
    }

    private void OnRiddleReceived(string aiMessage)
    {
        string message;
        if (aiMessage.ToLower().Contains("�zg�n�m") || aiMessage.ToLower().Contains("bilmiyorum"))
        {
            message = "Bilmeceyi sormakta bir sorun ya�ad�m. Sana T�rk mitolojisiyle ilgili ba�ka bir bilmece sunaca��m.";
            AddToChatHistory("�ahmeran: " + message);
            AskRiddle();
        }
        else
        {
            message = "Bilmecem �u: " + aiMessage;
            AddToChatHistory("�ahmeran: " + message);
            currentRiddle = aiMessage;
            correctAnswer = "y�lan"; // Do�ru cevab� burada ayarlay�n
        }
    }

    private void HandleRiddle(string playerAnswer)
    {
        if (playerAnswer.ToLower() == correctAnswer.ToLower())
        {
            string message = "Do�ru cevap, �lgen Han! Art�k �zel yetene�ini kullanabilirsin.";
            AddToChatHistory("�ahmeran: " + message);
            abilityUnlocked = true;
            currentState = ConversationState.Reward;
        }
        else
        {
            incorrectAttempts++;
            if (incorrectAttempts >= maxAttempts)
            {
                string message = $"�zg�n�m, �ok fazla yanl�� cevap verdiniz. Do�ru cevap: {correctAnswer}";
                AddToChatHistory("�ahmeran: " + message);
                currentState = ConversationState.Reward;
            }
            else
            {
                string message = "Yanl�� cevap. �pucu: T�rk mitolojisinde bilgeli�i temsil eden bir sembol d���n.";
                AddToChatHistory("�ahmeran: " + message);
            }
        }
    }

    private void AddToChatHistory(string message)
    {
        chatHistory.Add(message);
        dialogueText.text = string.Join("\n", chatHistory); // Sohbet ge�mi�ini g�ncelle
    }
}
