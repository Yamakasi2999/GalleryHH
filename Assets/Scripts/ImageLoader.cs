using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageLoader : MonoBehaviour
{
    public Transform contentTransform;
    private RectTransform viewportRectTransform;
    private RectTransform contentRectTransform;
    private ScrollRect scrollRect;

    public string imageUrlBase = "http://data.ikppbb.com/test-task-unity-data/pics/";
    public string[] imageUrls = new string[66];

    private int imagesPerLoad = 4;

    private int startIndex = 0;
    private int endIndex = 0;
    public GameObject imagePrefab;
    float counter = 1;


    private int visibleImageCount;
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        viewportRectTransform = scrollRect.viewport;
        contentRectTransform = scrollRect.content;
        float viewportHeight = contentRectTransform.rect.height;
        print(viewportHeight);
        float imageHeight = imagePrefab.GetComponent<RectTransform>().rect.height;
        print(imageHeight);
        visibleImageCount = Mathf.FloorToInt(viewportHeight / imageHeight);
        print(visibleImageCount);

        for (int i = 0; i < imageUrls.Length; i++)
        {
            imageUrls[i] = imageUrlBase + (i + 1) + ".jpg";
        }

        LoadImagesInRange(0, visibleImageCount);
        startIndex = visibleImageCount;

    }

    private void LoadNextImages()
    {
        endIndex = startIndex + imagesPerLoad;
        endIndex = Mathf.Min(endIndex, imageUrls.Length);
        LoadImagesInRange(startIndex, endIndex);
        startIndex = endIndex;

    }
    private void Update()
    {
        float viewportBottom = viewportRectTransform.transform.position.y;
        float contentBottom = contentRectTransform.transform.position.y;
        if (contentBottom > counter * viewportBottom)
        {
            counter += 1;
            LoadNextImages();

        }
    }
    private void LoadImagesInRange(int startIndex, int endIndex)
    {
        for (int i = startIndex; i < endIndex; i++)
        {
            GameObject imageObject = Instantiate(imagePrefab, contentTransform);
            imageObject.GetComponent<Photo>().MyURL = imageUrls[i];
            Image imageComponent = imageObject.GetComponent<Image>();
            StartCoroutine(ImageDownloader.instance.LoadImage(imageUrls[i], imageComponent));

        }
    }
}