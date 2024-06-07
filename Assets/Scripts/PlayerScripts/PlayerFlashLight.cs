using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashLight : MonoBehaviour
{

    [SerializeField] private Light flashLight;  

    void Start() => flashLight.enabled = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            flashLight.enabled = !flashLight.enabled;
    }
}
