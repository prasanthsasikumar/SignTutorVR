using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool I { get; set; }
    public bool C { get; set; }
    public bool E { get; set; }

    //Start function to set all to false
    void Start()
    {
        I = false;
        C = false;
        E = false;
    }

}
