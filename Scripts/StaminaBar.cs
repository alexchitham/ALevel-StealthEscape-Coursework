using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider slider; // Makes a variable of type Slider called slider

    public void SetStamina(float stamina) // Defines a procedure called SetStamina that requires a float pararmeter and names it locally as stamina
    {
        slider.value = stamina; // The value on the slider is set to the parameter that the procedure received
    }
    public void SetMaxStamina(int MaxStamina) // Defines a procedure called SetMaxStamina that requires an integer pararmeter and names it locally as MaxStamina
    {
        slider.maxValue = MaxStamina; // Sets the maximum value of the slider to the parameter that was passed into the procedure
        slider.value = MaxStamina; // Sets the current slider value to the maximum as you need to start with maximum stamina

    }
}
