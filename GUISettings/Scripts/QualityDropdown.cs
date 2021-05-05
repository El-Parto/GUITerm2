using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(TMP_Dropdown))]

public class QualityDropdown : MonoBehaviour
{
    //have access
    private TMP_Dropdown dropdown;


    // Start is called before the first frame update
    void Start()
    {
        //gets drop down here.
        dropdown = gameObject.GetComponent<TMP_Dropdown>();

        //set up options bassd on the quality settings
        SetupOptions();


        // make out OnOptionsChanged finction listen for the value to change on the dropdown
        dropdown.onValueChanged.AddListener(OnOptionChanged);
        //                      listen to it, then when event gets triggered and goes through every function in list
    }
    private void SetupOptions()
    {

        //don't need options that are there, as we want to generate our own settings based on quality of project
        dropdown.ClearOptions();

        //adds list for dropdowns
        List<TMP_Dropdown.OptionData> newOptions = new List<TMP_Dropdown.OptionData>();

        // loop through all quality settings
        foreach(string quality in QualitySettings.names)
        {
            //Create a new option based on the name and add it to the dropdown.
            TMP_Dropdown.OptionData option/*local variable*/ = new TMP_Dropdown.OptionData(quality);
            newOptions.Add(option);
        }
        // add alll the new options to the dropdown
        dropdown.AddOptions(newOptions);
        // Set the current value to the current quality level and show the correct value.
        dropdown.value = QualitySettings.GetQualityLevel();
        dropdown.RefreshShownValue();
    }
    // the above is all you need for setting your options

    // apply the Quality setting to the game based on the nbew value of the dropdown
    private void OnOptionChanged(int _option) => QualitySettings.SetQualityLevel(_option);


}
