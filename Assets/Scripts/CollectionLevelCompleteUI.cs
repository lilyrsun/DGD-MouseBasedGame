using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;             
using UnityEngine.UI;

public class CollectionLevelCompleteUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject panelRoot;
    public TextMeshProUGUI titleText;      // "Still Hungry..." or "Level Complete!"
    public Button nextLevelButton;         // the Next Level button

    bool allCheeseCollected = false;

    void Start()
    {
        if (CollectionCheeseManager.Instance != null)
            CollectionCheeseManager.Instance.OnCheeseCountChanged += HandleCheeseChange;

        if (panelRoot != null)
            panelRoot.SetActive(false);
    }

    public void Show()
    {
        if (panelRoot != null)
            panelRoot.SetActive(true);

        // Disable player + pause
        var mouse = Object.FindObjectOfType<MouseController>();
        if (mouse != null)
        {
            var col = mouse.GetComponent<Collider2D>();
            if (col != null) col.enabled = false;
            mouse.enabled = false;
        }

        // 🔹 Ask the manager directly if all cheese is collected
        bool hasAllCheese = false;
        if (CollectionCheeseManager.Instance != null)
            hasAllCheese = CollectionCheeseManager.Instance.AllCheeseCollected();

        allCheeseCollected = hasAllCheese;

        if (!hasAllCheese)
        {
            titleText.text = "Still Hungry...";
            nextLevelButton.interactable = false;
        }
        else
        {
            titleText.text = "Level Complete!";
            nextLevelButton.interactable = true;
        }

        Time.timeScale = 0f;
    }

    public void OnAllCheeseCollected()
    {
        allCheeseCollected = true;

        // If overlay is already up, update it live
        if (panelRoot != null && panelRoot.activeSelf)
        {
            titleText.text = "Level Complete!";
            nextLevelButton.interactable = true;
        }
    }

    public void OnNextLevel()
    {
        if (!allCheeseCollected) return; // just in case

        Time.timeScale = 1f;
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex + 1);
    }

    public void OnRetry()
    {
        Time.timeScale = 1f;
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }
    
    void OnDestroy()
    {
        if (CollectionCheeseManager.Instance != null)
            CollectionCheeseManager.Instance.OnCheeseCountChanged -= HandleCheeseChange;
    }

    void HandleCheeseChange()
    {
        if (CollectionCheeseManager.Instance != null &&
            CollectionCheeseManager.Instance.AllCheeseCollected())
        {
            OnAllCheeseCollected();
        }
    }

}