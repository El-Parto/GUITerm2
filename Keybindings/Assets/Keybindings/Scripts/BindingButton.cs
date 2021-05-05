using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BindingButton : MonoBehaviour
{
    [SerializeField]
    private string bindingToMap;
    [SerializeField]
    private Button button;
    [SerializeField]
    private TextMeshProUGUI buttonText;
    [SerializeField]
    private TextMeshProUGUI bindingName;


    private bool isRebinding = false;


    /// <summary>
    /// Sets up the rebinding button with the passed value.
    /// </summary>
    /// <param name="_tomap">the binding that requires mapping</param>
    public void Setup(string _tomap)
    {
        bindingToMap = _tomap;

        // Automatically set the onclick function and change the name text the binding
        button.onClick.AddListener(OnClick);
        bindingName.text = _tomap;


        // bupdate the buton text with the binding's value and make the GO active
        BindingUtils.UpdateTextWithBinding(bindingToMap, buttonText);
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        //checks if string is null or empty, checking bindingtomap
        //have we set bindingToMap variable
        if (string.IsNullOrEmpty(bindingToMap))
        {
            // we haven't so turn this gameObject off
            gameObject.SetActive(false);
            return;
        }

        // We have actually se the binding so set this script up!
        Setup(bindingToMap);
    }



    // Update is called once per frame
    void Update()
    {
        // Are we rebinding the key?
        if (isRebinding)
        {
            // try to get any key in the input if it was successful
            KeyCode pressed = BindingUtils.GetAnyKeyPressedKey();
            if(pressed != KeyCode.None)
            {
                // Rebind the Key an update the button text
                BindingManager.Rebind(bindingToMap, pressed);
                BindingUtils.UpdateTextWithBinding(bindingToMap, buttonText);

                // reset the isRebinding flag as we have now rebound.
                isRebinding = false;

            }
        }
    }

    private void OnClick()
    {
        isRebinding = true;
    }

}

