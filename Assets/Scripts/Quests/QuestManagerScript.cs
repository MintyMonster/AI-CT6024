using UnityEngine;
using TMPro;

public class QuestManagerScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCount;
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private GameObject keyCardManager;
    private KeyCardManager manager;
    private bool questComplete = false;

    // Pretty self explanatory but this controls the basic UI
    void Start()
    {
        if (textCount == null) Debug.LogError("textCount is not assigned!");
        if (questText == null) Debug.LogError("questText is not assigned!");
        if (keyCardManager == null) Debug.LogError("keyCardManager is not assigned!");

        manager = keyCardManager.GetComponent<KeyCardManager>();
        if (manager == null) Debug.LogError("KeyCardManager component not found on keyCardManager GameObject!");
    }

    void Update()
    {
        if (manager != null)
        {
            int keyCardCount = manager.GetKeyCardCount();
            textCount.text = $"{keyCardCount}/3";

            if (keyCardCount >= 3)
            {
                questComplete = true;
            }

            if (questComplete)
            {
                textCount.gameObject.SetActive(false);
                questText.text = "Find the escape door!";
            }
        }
    }
}
