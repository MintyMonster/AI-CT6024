using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Transform door1;
    private Transform door2;

    private float openSpeed = 2f;
    private float openDistance = .8f;
    private float triggerDistance = 6f;

    private Vector3 door1Closed;
    private Vector3 door2Closed;
    private Vector3 door1Open;
    private Vector3 door2Open;

    private List<GameObject> characters;

    // Start is called before the first frame update
    void Start()
    {
        characters = new List<GameObject>(GameObject.FindGameObjectsWithTag("Character"));

        door1 = transform.GetChild(1);
        door2 = transform.GetChild(2);

        door1Closed = door1.position;
        door2Closed = door2.position;

        door1Open = door1Closed + door1.right * openDistance;
        door2Open = door2Closed - door2.right * openDistance;
    }

    // Update is called once per frame
    void Update()
    {

        bool characterNearby = false;

        foreach(GameObject character in characters)
        {
            if(character != null)
            {
                if (Vector3.Distance(character.transform.position, this.transform.position) < triggerDistance)
                {
                    characterNearby = true;
                    break;
                }
            }
        }

        if(characterNearby)
        {
            door1.position = Vector3.Lerp(door1.position, door1Open, Time.deltaTime * openSpeed);
            door2.position = Vector3.Lerp(door2.position, door2Open, Time.deltaTime * openSpeed);
        }
        else
        {
            door1.position = Vector3.Lerp(door1.position, door1Closed, Time.deltaTime * openSpeed);
            door2.position = Vector3.Lerp(door2.position, door2Closed, Time.deltaTime * openSpeed);
        }
        
    }
}
