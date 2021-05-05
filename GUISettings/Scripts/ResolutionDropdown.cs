using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(TMP_Dropdown))]


public class ResolutionDropdown : MonoBehaviour
{
    private TMP_Dropdown dropdown;
    
    //takes the array and converts to list.
    private List<Resolution> resolutions;


    // Start is called before the first frame update
    void Start()
    {
        // displays res' based on your monitorys
        dropdown = gameObject.GetComponent<TMP_Dropdown>();

        // * Get all the  resolution supported by the current monitor.
        resolutions = new List<Resolution>(Screen.resolutions);

        SetupOptions();
        dropdown.onValueChanged.AddListener(OnResolutionChanged);
    }
    private void SetupOptions()
    {

        //don't need options that are there, as we want to generate our own settings based on quality of project
        dropdown.ClearOptions();

        //adds list for dropdowns
        List<TMP_Dropdown.OptionData> newOptions = new List<TMP_Dropdown.OptionData>();

        // loop through all resolution settings // VIVA LAS RESOLUTION
        foreach (Resolution resolution in resolutions)
        {
            //Create a new option based on the name and add it to the dropdown.
            TMP_Dropdown.OptionData option/*local variable*/ = new TMP_Dropdown.OptionData(resolution.ToString());// returns width x height
            newOptions.Add(option);
        }
        // add alll the new options to the dropdown
        dropdown.AddOptions(newOptions);
        // Set the current value to the current quality level and show the correct value.
        
        dropdown.value = resolutions.IndexOf(Screen.currentResolution);
        dropdown.RefreshShownValue();
    }

    private void OnResolutionChanged(int _resolution)
    {
        Resolution resolution = resolutions[_resolution];

        Screen.SetResolution(
            resolution.width,
            resolution.height,
            Screen.fullScreen,
            resolution.refreshRate);
    }
    
}
