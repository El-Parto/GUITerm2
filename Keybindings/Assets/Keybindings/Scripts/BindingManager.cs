using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//standard to order statics first
public class BindingManager : MonoBehaviour
{
    /// <summary>
    /// Attempts to retrieve the Held state of the key.
    /// </summary>
    /// <param name="_key">the binding we are trying to check.</param>
    /// <returns>Whether not the binding is held</returns>
    public static bool BindingHeld(string _key)
    {
        // attempt to retrieve the binding
        Binding binding = GetBinding(_key);
        if (binding != null)
        {
            // we got the binding so get its pressed state
            return binding.Held();
        }
        // No binding matches the passed key so log a message and return false
        Debug.LogWarning("No binding matches the presed key" + _key);
        return false;
    }


    /// <summary>
    /// Attempts to retrieve the released state of the key.
    /// </summary>
    /// <param name="_key">the binding we are trying to check.</param>
    /// <returns>Whether not the binding is released</returns>
    public static bool BindingReleased(string _key)
    {
        // attempt to retrieve the binding
        Binding binding = GetBinding(_key);
        if (binding != null)
        {
            // we got the binding so get its pressed state
            return binding.Released();
        }
        // No binding matches the passed key so log a message and return false
        Debug.LogWarning("No binding matches the presed key" + _key);
        return false;
    }


    /// <summary>
    /// Attempts to retrive the presed state of the key.
    /// </summary>
    /// <param name="_key">the binding we are trying to check.</param>
    /// <returns>Whether not the binding is pressed</returns>
    public static bool BindingPressed(string _key)
    {
        // attempt to retrieve the binding
        Binding binding = GetBinding(_key);
        if(binding != null)
        {
            // we got the binding so get its pressed state
            return binding.Pressed();
        }
        // No binding matches the passed key so log a message and return false
        Debug.LogWarning("No binding matches the presed key" + _key);
        return false;
    }


    /// <summary>
    /// updates the respective bindings value to the newly passed one.
    /// </summary>
    /// <param name="name"> The binding we are trying to remap</param>
    /// <param name="value">The new value f the binding</param>
    public static void Rebind(string _name, KeyCode _value)
    {
        // Attempt to get the corresponding binding.
        Binding binding = GetBinding(_name);

        if(binding != null)
        {
            //we retrived it so rebind the key
            binding.Rebind(_value);
        }
    }


    /// <summary>
    /// Gets all the bindings in the BindingManager
    /// </summary>
    public static List<Binding> GetBindings()
    {
        return instance.bindingsList;
    }

    /// <summary>
    /// Attempts to get the corresponding binding from the system
    /// </summary>
    /// <param name="_key"> the key of the binding we are trying to get</param>
    /// <returns>Returns the found binding if it exists, otherwise null</returns>
    public static Binding GetBinding(string _key)
    {
        //first we see if the binding exists in the system
        if (instance.bindingsMap.ContainsKey(_key))
        {
            //if so return it
            return instance.bindingsMap[_key];
        }


        //no binding matched so return null.
        return null;

    }

    //static = no matter how many you have, this variable will be the same across all of them

    //the singletone instance that will refer to the binding manager
    private static BindingManager instance = null;
    //instance will be related to the name of the class.

    // Used to actual access the bindings by their names when handling input. 
    private Dictionary<string, Binding> bindingsMap = new Dictionary<string, Binding>();
    //contains all bindings for easy iteration over all over time
    private List<Binding> bindingsList = new List<Binding>();

    //start by creating dictionaries


    [SerializeField]
    private List<Binding> defaultBindings = new List<Binding>();//shows in inspector

    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        //first check if instance isn't already set
        if(instance == null)
        {
            // it isn't so make it this instance of binding manager
            instance = this;
        }
        //the instance is already set, but is it this instance of the Binding Manager?
        else if (instance != this)
        {
            // it isn't so destry this GameObject and return, because we want only one BindingManager.
            Destroy(gameObject);
            return;
        }
        // Setup the bindings on this manager
        PopulationBindingDictionaries();
        LoadBindings();
    }


    /// <summary>
    /// Loads all of the default bindings set in the inspector into the system's dictionaries and lists.
    /// </summary>

    private void PopulationBindingDictionaries()
    {

        //loop through all te bindings are set in the inspector
        foreach (Binding binding in defaultBindings)
        {
            // if the bindingsMap already contains a bindig with this name
            //ignore this binding
            if (bindingsMap.ContainsKey(binding.Name))
            {
                continue; //goes back to the top of the code and continues.
            }

            bindingsMap.Add(binding.Name, binding);
            bindingsList.Add(binding);
        }
    }

    /// <summary>
    /// Loads the bindings data for each binding in the system
    /// </summary>
    private void LoadBindings()
    {
        foreach (Binding binding in bindingsList)
        {

            binding.Load();
        }
    }

    /// <summary>
    /// Saves the data of everybinding in the buiildings list
    /// </summary>
    private void SaveBindings()
    {
        foreach (Binding binding in bindingsList)
        {
            binding.Save();
        }
    }

}
