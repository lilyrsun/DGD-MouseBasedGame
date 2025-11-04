using UnityEngine;
using TMPro;

public class CheeseManager : MonoBehaviour
{
    public static CheeseManager Instance { get; private set; }

    [Header("Cheese Goal")]
    public bool autoCountCheeseInScene = true;
    public int requiredCheese = 0;          // auto-filled if autoCount is true

    [Header("UI")]
    public TextMeshProUGUI cheeseText;      // assign in inspector

    int collectedCheese = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        if (autoCountCheeseInScene)
        {
            // Count all CheesePickup objects placed in the level
            requiredCheese = FindObjectsOfType<CheesePickup>().Length;
        }

        UpdateUI();
    }

    public void AddCheese(int amount)
    {
        collectedCheese += amount;
        if (collectedCheese > requiredCheese)
            collectedCheese = requiredCheese;

        UpdateUI();

        // Check win condition
        if (requiredCheese > 0 && collectedCheese >= requiredCheese)
        {
            // If you have the LevelManager from earlier, call it:
            if (LevelManager.Instance != null)
                LevelManager.Instance.CompleteLevel();
            else
                Debug.Log("Cheese goal reached!");
        }
    }

    void UpdateUI()
    {
        if (cheeseText != null)
        {
            cheeseText.text = $"Cheese: {collectedCheese} / {requiredCheese}";
        }
    }
}