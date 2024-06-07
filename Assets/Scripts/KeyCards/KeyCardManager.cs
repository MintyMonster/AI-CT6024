using UnityEngine;

public class KeyCardManager : MonoBehaviour
{
    private int keyCardCount = 0;

    public int GetKeyCardCount()
    {
        //Debug.Log($"GetKeyCardCount called. Current count: {keyCardCount}");
        return keyCardCount;
    }

    public void AddKeyCardCount()
    {
        keyCardCount++;
        //Debug.Log($"AddKeyCardCount called. New count: {keyCardCount}");
    }
}
