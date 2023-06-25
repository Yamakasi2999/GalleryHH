using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Text loadingPercente;
    [SerializeField] Image loadingImageProgressbar;

    public static SceneLoader instance;

    [SerializeField] GameObject sceneTransition;
    [SerializeField] GameObject buttonGallery;

    private AsyncOperation loadingSceneOperation;
    private bool isLoadRequested = false;
    private bool isDelayStarted = false;

    void Start()
    {
        instance = this;
        buttonGallery.SetActive(true);
    }
    void Update()
    {
        if (isLoadRequested)
        {
            loadingPercente.text = Mathf.RoundToInt(loadingSceneOperation.progress * 100) + "%";
            loadingImageProgressbar.fillAmount = loadingSceneOperation.progress;

            if (loadingSceneOperation.progress >= 0.9f && !isDelayStarted)
            {
                isDelayStarted = true;
                StartCoroutine(DelayBeforeSceneActivation());
            }
        }
    }
    private void LoadSceneWithDelay(string sceneName)
    {
        loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        loadingSceneOperation.allowSceneActivation = false;
        isLoadRequested = true;
    }
    private IEnumerator DelayBeforeSceneActivation()
    {
        yield return new WaitForSeconds(2f);

        loadingSceneOperation.allowSceneActivation = true;
        isLoadRequested = false;
        isDelayStarted = false;
    }
    public void SwitchUI()
    {
        sceneTransition.SetActive(true);
        buttonGallery.SetActive(false);
        LoadSceneWithDelay("Gallery");
    }
}
