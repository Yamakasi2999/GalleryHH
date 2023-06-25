using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ViewImage : MonoBehaviour
{
    public Image image;
    
    void Start()
    {
        string imageUrl = ImageDownloader.selectedImageUrl;
        StartCoroutine(ImageDownloader.instance.LoadImage(imageUrl, image));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 deltaPosition = Input.GetTouch(0).deltaPosition;
            float swipeThreshold = 100f; //Порог свайпа

            if (Mathf.Abs(deltaPosition.y) > swipeThreshold && Mathf.Abs(deltaPosition.x) < swipeThreshold)
            {
                //свайп влево
                if (deltaPosition.x < 0)
                {
                    Back();
                }
            }
        }
    }

    public void Back() 
    {
        SceneManager.LoadScene("Gallery");
    }
}
