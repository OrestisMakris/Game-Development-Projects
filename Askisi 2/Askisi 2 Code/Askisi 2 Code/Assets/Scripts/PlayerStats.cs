using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static int failures = 0;
    public Text failureText;

    void Update()
    {
        failureText.text = "Failures: " + failures;
    }

    public void AddFailure()
    {
        failures++;
    }
}
