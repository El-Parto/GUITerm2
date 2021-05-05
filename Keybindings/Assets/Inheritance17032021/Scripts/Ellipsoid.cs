using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ellipsoid : Sphere
{
    // Start is called before the first frame update
    protected override void Start()
    {
        //Currently uses Splash   
    }

    public override float CalculateArea()
    {
        float ab = Mathf.Pow(height * width, 1.6f);
        float ac = Mathf.Pow(width * depth, 1.6f);
        float bc = Mathf.Pow(height * depth, 1.6f);
        return 4 * Mathf.PI * Mathf.Pow((ab + ac + bc) / 3, 1 / 1.6f);

    }

    public override string GetShapeName()
    {
        return "Ellipsoid";
    }
    
}
