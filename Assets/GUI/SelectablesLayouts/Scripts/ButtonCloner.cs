using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCloner : MonoBehaviour
{

    [SerializeField]
    private GameObject toClone;
    [SerializeField]
    private Transform content;
    [SerializeField]
    private int count;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject cloned = Instantiate(toClone, content);
            cloned.SetActive(true);

        }
    }


    public void LogMessage()
    {
        Debug.Log("clicked");
    }
}
