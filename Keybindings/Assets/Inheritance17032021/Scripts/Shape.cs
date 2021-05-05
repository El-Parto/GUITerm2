using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]
//[RequireComponent(typeof(Image))]
public class Shape : MonoBehaviour
{
    [SerializeField]
    protected Image image;
    [SerializeField]
    protected Sprite sprite;
    [SerializeField, Min(0.1f)]
    protected float width = 0.1f;
    [SerializeField, Min(0.1f)]
    protected float height = 0.1f;

    // virtual means anything inheriting this class can change the functionality
    // of the virtual method/ property
    // Protected means that anything inheriting this class can access it, but nothing outside the
    // " family " 
    // Start is called before the first frame update
    protected virtual void Start() // 
    {
        // Set the image component's sprite to the sprite of our object.
        image = gameObject.GetComponent<Image>();
        image.sprite = sprite;
        image.type = Image.Type.Sliced;


    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Debug.Log($"{GetShapeName()} : {CalculateArea()}"); // $ a formattable string anything that's in {} (must use {} per callable areas
       // anything outside of the {} is still a string

        // change size of the object based on width and height we set.
        RectTransform rTransform = gameObject.GetComponent<RectTransform>();
        rTransform.sizeDelta = new Vector2(width * 100, height * 100);


    }


    /// <summary>
    /// Uses simple euations o calculate the area of whatever shape the type is
    /// By defaiult, it returns width * hieght
    /// </summary>
    public virtual float CalculateArea()
    {
        return width * height;
    }



    /// <summary>
    /// Returns the name of this shape for displaying in messages
    /// By Default, it is "Shape"
    /// </summary>
    public virtual string GetShapeName()
    {
        return "Shape";
    }

}
