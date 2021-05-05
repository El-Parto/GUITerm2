using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadManager : MonoBehaviour
{
    [SerializeField]
    private Image loadingBar; // this will show the progress of the loading.

    [SerializeField]
    private string sceneToLoad; // this is the scene that we will be loading dynamically

    [SerializeField]
    private GameObject loadingBarBackground; //referencing this as well
    [SerializeField]
    private GameObject loadButton; //referncing this
    [SerializeField]
    private GameObject canvasCamera; // this will be disabled whenthe new scene is loaded.

    public void OnClickLoadGame()
    {
        // swap out the object being shown on the screen
        loadingBarBackground.SetActive(true);
        loadButton.SetActive(false);

        StartCoroutine(LoadSceneAsync());
    }



    // Start is called before the first frame update
    void Start()
    {
        loadingBarBackground.SetActive(false);
        loadButton.SetActive(true);
        canvasCamera.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //asyncronous = parrallel 
    private IEnumerator LoadSceneAsync()
    {
        // Reset the loading bar incase it isn't done.
        loadingBar.fillAmount = 0;

        // start the load of the scene and tell to activate when done

        AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        //moment it's done, run
        sceneLoadOperation.allowSceneActivation = true;

        // loop continuously until the operation is complete
        while (!sceneLoadOperation.isDone)
        {
            //Updae the progressbar and wait until the next frame
            loadingBar.fillAmount = sceneLoadOperation.progress;
            yield return null;
        }

        // update the loadingbar to full and wait half a second
        loadingBar.fillAmount = 1;
        yield return new WaitForSeconds(1f);

        //find and activate the character controller loops through entire scene, will find first instance of GO that has component and returns that component on that GO
        CharController_Motor motor = FindObjectOfType<CharController_Motor>();
        motor.Initialise();

        //disable the camera and the loading bar
        canvasCamera.SetActive(false);
        loadingBarBackground.SetActive(false);
    
    }
}
