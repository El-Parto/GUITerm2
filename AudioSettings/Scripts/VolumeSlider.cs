using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

// everything to do with audio is within the namespace.
// setting up your audio

[RequireComponent(typeof(Slider))] // more sliders required.

public class VolumeSlider : MonoBehaviour
{
    //reference to the audio mixer
    [SerializeField]
    private AudioMixer mixer; // the mixer we want to control.
    [SerializeField]
    private string volumeParameter;


    private Slider slider;
    


    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();

        // setting up values
        slider.minValue = 0;
        slider.maxValue = 1;
        
        slider.value = PlayerPrefs.GetFloat(volumeParameter, 1); ;//stores the value

        //name the parameter                                                       this one was in the Remap function
        slider.onValueChanged.AddListener(_value => mixer.SetFloat(volumeParameter, Remap(_value, 0, 1, -80 ,0)));

        //need a way to convert the value into the correct range
        // not 0-1 but 80 = 0

    }
    // this function is called when the MonoBehaviour will be destroyed,
    private void OnDestroy()
    {
        PlayerPrefs.SetFloat(volumeParameter, slider.value);
        PlayerPrefs.Save();
    }




    /// <summary>
    /// Takes a float and changes its minium and Maximum range to the new one
    /// </summary>
    /// <param name="_value"> the value being remapped</param>
    /// <param name="_oldMin">The old minimum value o the float</param>
    /// <param name="_oldMax">the old maximum value of the float</param>
    /// <param name="_newMin">tbe new minimum value of the float</param>
    /// <param name="_newMax">the new maximum value of te float</param>
    /// <returns></returns>
    private float Remap ( float _value, float _oldMin, float _oldMax, float _newMin, float _newMax)
    {
        //takes the number and changes it
        return (_value - _oldMin) / (_oldMax - _oldMin) * (_newMax + _newMin) + _newMin;
    }

    
}
