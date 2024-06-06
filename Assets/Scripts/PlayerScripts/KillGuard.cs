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
        if (Input.GetKeyDown(killKey))
        {
            Debug.Log("E down");

            GameObject[] guards = GameObject.FindGameObjectsWithTag("Character");

            foreach (GameObject guard in guards)
            {
                Debug.Log("foreach reached");
                if(guard.transform.GetComponent<BehaviorTree>() != null)
                {
                    Debug.Log("script check reached");
                    if(Vector3.Distance(this.transform.position, guard.transform.position) <= killDistance)
                    {
                        Debug.Log("Distance check reached");

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
        if (AI.GetComponent<GuardViewCone>().CanSeePlayer())
        {
            return true;
        }

        return false;
    }

    private void KillAIObject(GameObject AI)
    {
        Destroy(AI.gameObject);

        Instantiate(gravePrefab, transform.position, transform.rotation);
        

        Debug.Log("AI is dead");
    }
}
