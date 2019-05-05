using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadnext : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject LoadingScreenObj;
    public Slider slider;
    AsyncOperation async;

    public void load()
    {
        StartCoroutine(LoadingScreen());
        SceneManager.LoadScene(1);
    }

    IEnumerator LoadingScreen()
    {
        LoadingScreenObj.SetActive(true);
        async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;
        while (async.isDone == false)
        {
            slider.value = async.progress;
            Debug.Log(async.progress);
            if( async.progress == 0.9f)
            {
                slider.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }

    }
}
