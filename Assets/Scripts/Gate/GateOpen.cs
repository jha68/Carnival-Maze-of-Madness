using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpen : MonoBehaviour
{
    private Animator gate;

    // Start is called before the first frame update
    void Start()
    {
        gate = GetComponent<Animator>();
        gate.SetBool("Near", false);
    }
    public void OpenTheGate()
    {
        gate.SetBool("Near", true);
    }
}
