using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class KillGuard : MonoBehaviour
{
    public float killDistance = 5f;
    public KeyCode killKey = KeyCode.E;
    public LayerMask aiLayer;
    public GameObject gravePrefab;

    // Update is called once per frame
    void Update()
    {
        CheckKill();
    }

    private void CheckKill()
    {
        // input
        if (Input.GetKeyDown(killKey))
        {
            // array of the guards
            GameObject[] guards = GameObject.FindGameObjectsWithTag("Character");

            // loop
            foreach (GameObject guard in guards)
            {
                // check if actually AI and not the player (same tag)
                if(guard.transform.GetComponent<BehaviorTree>() != null)
                {
                    // distance check
                    if(Vector3.Distance(this.transform.position, guard.transform.position) <= killDistance)
                    {

                        if (!IsSeenByGuard(guard))
                        {
                            Debug.Log("AI Killed");
                            KillAIObject(guard);
                        }
                    }
                }
            }
        }
    }

    private bool IsSeenByGuard(GameObject AI)
    {
        if (AI.GetComponent<GuardViewCone>().CanSeePlayer()) // custom viewcone func
        {
            return true;
        }

        return false;
    }

    private void KillAIObject(GameObject AI)
    {
        Destroy(AI.gameObject);

        Instantiate(gravePrefab, transform.position, transform.rotation); // grave
        

        Debug.Log("AI is dead");
    }
}
