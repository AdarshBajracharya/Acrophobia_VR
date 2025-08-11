using UnityEngine;
using UnityEngine.UI;          // For legacy UI Text
using UnityEngine.InputSystem; // New Input System

public class CalmDownPopup : MonoBehaviour
{
    public GameObject popupCanvas;    // The popup UI Canvas (set inactive by default)
    public Transform playerCamera;    // The VR camera (player’s head)
    public Text messageText;          // Legacy UI Text component inside popup

    private string[] calmingSteps = new string[]
    {
        "Step 1: Take a deep breath.",
        "Step 2: Focus on your breathing rhythm.",
        "Step 3: Slowly look around and ground yourself.",
        "Step 4: Remind yourself you are safe here.",
        "Step 5: When ready, continue to the next step."
    };

    private int currentStep = 0;
    private bool isPopupVisible = false;

    void Start()
    {
        popupCanvas.SetActive(false); // Hide initially
        ShowCalmSteps();              // Show first step immediately
    }

    void Update()
    {
        if (InputDetected())
        {
            if (isPopupVisible)
            {
                ShowNextStep();
            }
            else
            {
                // Popup hidden, start over on next click
                ShowCalmSteps();
            }
        }
    }

    private bool InputDetected()
    {
        return Mouse.current.leftButton.wasPressedThisFrame ||
               (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasReleasedThisFrame);
    }

    public void ShowCalmSteps()
    {
        currentStep = 0;
        popupCanvas.SetActive(true);
        isPopupVisible = true;
        PositionPopup();
        ShowNextStep();  // Show the first step
    }

    public void ShowNextStep()
    {
        if (currentStep < calmingSteps.Length)
        {
            messageText.text = calmingSteps[currentStep];
            currentStep++;
        }
        else
        {
            // All steps done, hide popup
            HidePopup();
        }
    }

    void PositionPopup()
    {
        popupCanvas.transform.position = playerCamera.position + playerCamera.forward * 2f;
        popupCanvas.transform.rotation = Quaternion.LookRotation(popupCanvas.transform.position - playerCamera.position);
    }

    public void HidePopup()
    {
        popupCanvas.SetActive(false);
        isPopupVisible = false;
    }
}
