using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageDownloader : MonoBehaviour
{
    public static string selectedImageUrl;
    public static ImageDownloader instance;
    
    private void Start()
    {
        instance = this;
    }
    public IEnumerator LoadImage(string url, Image image)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            image.sprite = sprite;
        }
        else
        {
            Debug.Log("Failed to load image: " + www.error);
        }
    }
    public void SetURL(string url) 
    {
        selectedImageUrl = url;
    }
}
