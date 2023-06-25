using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Photo : MonoBehaviour
{
    public string MyURL;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => BigImage());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BigImage() 
    {
        ImageDownloader.instance.SetURL(MyURL);
        SceneManager.LoadScene("View");
    }
}
