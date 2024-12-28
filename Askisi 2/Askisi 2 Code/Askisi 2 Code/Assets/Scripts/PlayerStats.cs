using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Text failureText; //UI text to display the failures
    
    private int failures = 0;

    void Start()
    {
        // Load failures from PlayerPrefs
        failures = PlayerPrefs.GetInt("PlayerFailures", 0);
        UpdateFailureUI();
    }

    void Update()
    {
        
    }

    public void AddFailure()
    {
        failures++;
        PlayerPrefs.SetInt("PlayerFailures", failures); // Save failures to PlayerPrefs
        PlayerPrefs.Save(); 
        UpdateFailureUI();
    }

    private void UpdateFailureUI()
    {
        failureText.text = "Failures: " + failures;
    }

    // Reset failures for debugging or UI interaction)
    public void ResetFailures()
    {
        failures = 0;
        PlayerPrefs.SetInt("PlayerFailures", failures);
        PlayerPrefs.Save();
        UpdateFailureUI();
    }
}
