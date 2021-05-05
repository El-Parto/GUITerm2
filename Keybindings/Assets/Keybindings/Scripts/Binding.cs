using UnityEngine;
using System; //namespace

[Serializable] //using system makes a class available to be seen in inspector

public class Binding
{
    

    //properties use PascalCasing
    public string Name { get { return name; } }
    public KeyCode Value { get { return value; } }
    public string ValueDisplay { get { return BindingUtils.TranslateKeycode(value); } } // displays as a string.


    [SerializeField]
    private string name;
    [SerializeField]
    private KeyCode value; //want these to be accessable outside the class but not set

    public Binding (string _name, KeyCode _defaultValue)
    {   
        name = _name;
        value = _defaultValue;
    }



    /// <summary>
    /// Saves the keycode into plyerprefs so that the 
    /// binding is persistant between game sessions
    /// </summary>
    public void Save() //player pres ar a way to save persistant data over game sessions, only 3 types area llowed, string int and float
    {
        // Stores the value of this binding to the system under its name
        PlayerPrefs.SetInt(name, (int)value);
        // actuall saves all the values to the system
        PlayerPrefs.Save();
    }


    /// <summary>
    /// loads the stored value of this keybinding, if it is not set, it uses the default value.
    /// </summary>
    public void Load()
    {
        //gets the int version  the keycode from the player prefs and sets our value to it.
        value = (KeyCode)PlayerPrefs.GetInt(name, (int)value);
    }

    /// <summary>
    /// Rebubds the binding to the new keybinding and then saves to the player prefs
    /// </summary>
    /// <param name="_new">The key the binding will now be bound to.</param>
    public void Rebind(KeyCode _new)
    {
        value = _new;
        Save();
    }


    /// <summary>
    /// Returns whether or not the ke this binding is mapped to, was pressed this frame
    /// </summary>
    
    public bool Pressed()
    {
        return Input.GetKeyDown(value);
    }
    /// <summary>
    /// Returns whether or not the key this binding is mapped to, was pressed this frame
    /// </summary>

    public bool Held()
    {
        return Input.GetKey(value);
    }
    /// <summary>
    /// Returns whether or not the key this binding is mapped to, is being released this frame
    /// </summary>

    public bool Released()
    {
        return Input.GetKeyUp(value);
    }

}
