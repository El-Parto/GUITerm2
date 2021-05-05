using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// namespaces are not inheritable
public class Circle : Shape
{
    // Start is called before the first frame update
    protected override void Start() // 
    {
        base.Start(); // we want to do everything in start() in the Shape script 
        image.type = UnityEngine.UI.Image.Type.Simple;
    }

    public override float CalculateArea()
    {
        return Mathf.PI * base.CalculateArea(); // base means access parent class in this case base is accessing "Shape"
    }

    public override string GetShapeName()
    {
        return "Ellipse";
    }

}
