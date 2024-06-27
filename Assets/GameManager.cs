using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Animator waitressAnimator; // Reference to the animator of the waitress

    // Arrays to hold GameObjects to disable/enable for each state
    public GameObject[] helloDisable;
    public GameObject[] helloEnable;
    public GameObject[] iceDisable;
    public GameObject[] iceEnable;
    public GameObject[] coffeeDisable;
    public GameObject[] coffeeEnable;
    public GameObject[] holdOnDisable;
    public GameObject[] holdOnEnable;
    public GameObject[] thankYouDisable;
    public GameObject[] thankYouEnable;

    public bool IsHelloDetected { get; set; }
    public bool IsIceDetected { get; set; }
    public bool IsCoffeeDetected { get; set; }
    public bool IsThankYouDetected { get; set; }


    private enum GameState
    {
        WaitForHelloGesture,
        RespondToHelloGesture,
        WaitForIceGesture,
        WaitForCoffeeGesture,
        RespondToHoldOnGesture,
        DeliverCoffee,
        WaitForThankYouGesture,
        RespondToThankYouGesture,
        Completed
    }

    private GameState currentState = GameState.WaitForHelloGesture;

    void Start()
    {
        // Initial setup
        DisableAllExcept(helloEnable); // Initially enable only things related to WaitForHelloGesture
    }

    void Update()
    {
        // Check for gestures or triggers based on current state
        switch (currentState)
        {
            case GameState.WaitForHelloGesture:
                // Check for gesture for "hello"
                if (IsHelloDetected)
                {
                    currentState = GameState.RespondToHelloGesture;
                    waitressAnimator.Play("HelloSign");
                    Debug.Log("Hello gesture detected");
                    DisableAllExcept(helloDisable);
                    EnableAllExcept(helloEnable);
                }
                break;

            case GameState.RespondToHelloGesture:
                // Wait for animation to finish responding to "hello" gesture
                // Optionally, transition state based on animation event or time

                // For simplicity, simulate wait time (you can replace with actual logic)
                Invoke("ProceedToIceGestureState", 1f); // Example: Wait 1 second before proceeding
                break;

            case GameState.WaitForIceGesture:
                // Check for gesture for "ICE"
                if (IsIceDetected)
                {
                    currentState = GameState.WaitForCoffeeGesture;
                    DisableAllExcept(iceDisable);
                    EnableAllExcept(iceEnable);
                }
                break;

            case GameState.WaitForCoffeeGesture:
                // Check for gesture for "coffee"
                if (IsCoffeeDetected)
                {
                    currentState = GameState.RespondToHoldOnGesture;
                    Debug.Log("Coffee gesture detected");
                    waitressAnimator.Play("HoldonSign");
                    DisableAllExcept(coffeeDisable);
                    EnableAllExcept(coffeeEnable);
                }
                break;

            case GameState.RespondToHoldOnGesture:
                // Wait for animation to finish responding to "hold on" gesture
                // Optionally, transition state based on animation event or time

                // For simplicity, simulate wait time (you can replace with actual logic)
                Invoke("DeliverCoffeeState", 1.5f); // Example: Wait 1.5 seconds before proceeding
                break;

            case GameState.DeliverCoffee:
                // Perform actions for delivering coffee (e.g., moving an object, triggering animations)
                // This state could include any necessary gameplay logic for delivering coffee
                // After delivering coffee, proceed to next state
                currentState = GameState.WaitForThankYouGesture;
                waitressAnimator.Play("ServeTea");
                DisableAllExcept(holdOnDisable);
                EnableAllExcept(holdOnEnable);
                break;

            case GameState.WaitForThankYouGesture:
                // Check for gesture for "thank you"
                if (IsThankYouDetected)
                {
                    currentState = GameState.RespondToThankYouGesture;
                    waitressAnimator.Play("WelcomeSign");
                    DisableAllExcept(thankYouDisable);
                    EnableAllExcept(thankYouEnable);
                }
                break;

            case GameState.RespondToThankYouGesture:
                // Wait for animation to finish responding to "you're welcome" gesture
                // Optionally, transition state based on animation event or time

                // For simplicity, simulate wait time (you can replace with actual logic)
                Invoke("CompleteState", 1f); // Example: Wait 1 second before completing
                break;

            case GameState.Completed:
                // Game or sequence has completed
                break;

            default:
                break;
        }
    }

    // Methods to transition between states (replace with actual logic if needed)
    void ProceedToIceGestureState()
    {
        currentState = GameState.WaitForCoffeeGesture;
        DisableAllExcept(iceDisable);
        EnableAllExcept(iceEnable);
    }

    void DeliverCoffeeState()
    {
        currentState = GameState.DeliverCoffee;
        // Perform actions to deliver coffee (animation, object movement, etc.)
        // For simplicity, just moving to the next state after delay
        DisableAllExcept(coffeeDisable);
        EnableAllExcept(coffeeEnable);
    }

    void CompleteState()
    {
        currentState = GameState.Completed;
        // Perform any final actions or cleanup
        DisableAllExcept(thankYouDisable);
        EnableAllExcept(thankYouEnable);
    }

    // Method to disable all GameObjects except those in the given array
    void DisableAllExcept(GameObject[] toKeepEnabled)
    {
        // Disable everything
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        // Enable specified GameObjects
        foreach (GameObject obj in toKeepEnabled)
        {
            obj.SetActive(true);
        }
    }

    // Method to enable all GameObjects except those in the given array
    void EnableAllExcept(GameObject[] toDisable)
    {
        // Enable everything
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

        // Disable specified GameObjects
        foreach (GameObject obj in toDisable)
        {
            obj.SetActive(false);
        }
    }
}
