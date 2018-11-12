using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptchaUI : MonoBehaviour {
    public Image img;

    // The source image
    public void Start() {
        img = GetComponent<Image>();
        StartCoroutine(loadImage());
    }
    IEnumerator loadImage() {
        string url = "http://127.0.0.1:8000/polls/dark/";
        WWW www = new WWW(url);
        yield return www;
        img.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    }
}
