using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;
    public Transform cameraTransform;
    public float distanceFromCamera = 2.0f;
    public bool activePauseUI;

    private CanvasGroup pauseCanvasGroup;
    private InputAction togglePauseAction;

    private void Awake()
    {
        if (pauseUI == null)
            pauseUI = gameObject;

        pauseCanvasGroup = pauseUI.GetComponent<CanvasGroup>();
        if (pauseCanvasGroup == null)
            pauseCanvasGroup = pauseUI.AddComponent<CanvasGroup>();

        FindCamera();
    }

    private void OnEnable()
    {
        // Meta Quest maps the right Touch controller's A button to primaryButton.
        togglePauseAction = new InputAction("Toggle Pause", InputActionType.Button);
        togglePauseAction.AddBinding("<XRController>{RightHand}/primaryButton");
        togglePauseAction.AddBinding("<OculusTouchController>{RightHand}/primaryButton");
        togglePauseAction.Enable();
    }

    private void OnDisable()
    {
        togglePauseAction?.Disable();
        togglePauseAction?.Dispose();
        togglePauseAction = null;
    }

    private void Start()
    {
        SetPauseMenuVisible(false);
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (togglePauseAction != null && togglePauseAction.WasPressedThisFrame())
            DisplayPauseUI();
    }

    public void DisplayPauseUI()
    {
        if (activePauseUI)
        {
            ResumeGame();
            return;
        }

        ShowInFrontOfPlayer();
        SetPauseMenuVisible(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        SetPauseMenuVisible(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void ShowInFrontOfPlayer()
    {
        FindCamera();
        if (cameraTransform == null)
            return;

        pauseUI.transform.position = cameraTransform.position + cameraTransform.forward * distanceFromCamera;
        pauseUI.transform.rotation = Quaternion.LookRotation(pauseUI.transform.position - cameraTransform.position,
            cameraTransform.up);
    }

    private void FindCamera()
    {
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    private void SetPauseMenuVisible(bool visible)
    {
        activePauseUI = visible;
        pauseCanvasGroup.alpha = visible ? 1f : 0f;
        pauseCanvasGroup.interactable = visible;
        pauseCanvasGroup.blocksRaycasts = visible;
    }
}
