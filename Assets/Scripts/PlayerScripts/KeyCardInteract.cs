using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyCardInteract : MonoBehaviour
{
    [SerializeField] private GameObject keyCardManagerObject;
    [SerializeField] private List<GameObject> keycards;
    [SerializeField] private Transform escapeDoor;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private float distance = 5f;
    private KeyCardManager manager;

    void Start()
    {
        manager = keyCardManagerObject.GetComponent<KeyCardManager>();
        if (manager == null) Debug.LogError("KeyCardManager component not found on keyCardManagerObject!");
    }

    void Update()
    {
        // Get input
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Temp keycards
            List<GameObject> collectedKeycards = new List<GameObject>();

            // loop
            foreach (var keycard in keycards)
            {
                // distance check
                if (Vector3.Distance(keycard.transform.position, this.transform.position) <= distance)
                {

                    // logic
                    Debug.Log($"Player is within distance to pick up keycard: {keycard.name}");
                    manager.AddKeyCardCount();
                    Debug.Log($"KeyCard collected. Total count: {manager.GetKeyCardCount()}");
                    collectedKeycards.Add(keycard);
                }
            }

            // Remove and destroy collected keycards
            foreach (var keycard in collectedKeycards)
            {
                keycards.Remove(keycard);
                Destroy(keycard);
            }
        }


        // keycard door stuff
        if(manager.GetKeyCardCount() > 2)
        {
            if(Vector3.Distance(this.transform.position, escapeDoor.position) < distance)
            {
                winScreen.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
