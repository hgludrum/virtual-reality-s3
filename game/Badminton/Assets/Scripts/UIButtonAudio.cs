using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class UIButtonAudio : MonoBehaviour
{
    [Header("UI Click Sound")]
    [SerializeField] private AudioClip buttonClickClip;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Hittar alla knappar i scenen/menyn och lägger till klickljudet
        Button[] buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach (Button btn in buttons)
        {
            btn.onClick.AddListener(PlayClickSound);
        }
    }

    public void PlayClickSound()
    {
        if (buttonClickClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickClip, 0.8f);
        }
    }
}