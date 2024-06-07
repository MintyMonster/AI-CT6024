using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeDoor : MonoBehaviour
{
    [SerializeField] private GameObject keyCardManagerObject;
    [SerializeField] private GameObject player;
    [SerializeField] private Canvas winScreen;
    private float distance = 10f;
    private KeyCardManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = keyCardManagerObject.GetComponent<KeyCardManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) <= distance)
        {
            Debug.Log("Within distance");
            Debug.Log(manager.GetKeyCardCount());
            if (manager.GetKeyCardCount() >= 1)
            { 
                Debug.Log("Enough keycards");
                winScreen.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
