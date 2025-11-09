using UnityEngine;
using System;

public class CollectionCheeseManager : MonoBehaviour
{
    public static CollectionCheeseManager Instance { get; private set; }

    [Header("Cheese Counts")]
    public int totalCheese = 0;
    public int collectedCheese = 0;

    // optional text hook
    public TMPro.TextMeshProUGUI cheeseText;

    // event so UI can listen for updates
    public event Action OnCheeseCountChanged;

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
        // automatically count all cheese in the scene
        totalCheese = FindObjectsByType<CheesePickup>(FindObjectsSortMode.None).Length;
        UpdateText();
    }

    public void AddCheese(int amount)
    {
        collectedCheese += amount;
        UpdateText();
        OnCheeseCountChanged?.Invoke();
    }

    void UpdateText()
    {
        if (cheeseText != null)
            cheeseText.text = $"Cheese: {collectedCheese} / {totalCheese}";
    }

    public bool AllCheeseCollected() => collectedCheese >= totalCheese;
}