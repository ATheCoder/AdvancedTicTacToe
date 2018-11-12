using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class POSTScript : MonoBehaviour {
    public GameObject ErrorPanel;
    public GameObject[] Errors;
    public InputField usernameField;
    public InputField passwordField;
    public InputField captchaField;
    public Image img;
    private bool isErrorOpen = false;
    private string CaptchaKey;
    // Use this for initialization
    void Start() {
        CrossSceneInformation.setSessionID(CrossSceneInformation.createSessionID());
        StartCoroutine(loadImage());
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Return) && !isErrorOpen) {
            onPress();
        }
        else if (isErrorOpen && Input.GetKeyDown(KeyCode.Return)) {
            CloseErrorMessage();
        }
    }
    public void onPress() {
        sendLogin(usernameField, passwordField);

    }
    public void sendLogin(InputField username, InputField password) {
        string url = "http://127.0.0.1:8000/polls/dank/" + "1" + "/";
        Debug.Log(url);
        WWWForm form = new WWWForm();
        form.AddField("username", username.text);
        form.AddField("password", password.text);
        form.AddField("sID", CrossSceneInformation.SessionID);
        form.AddField("captcha_0", CaptchaKey);
        form.AddField("captcha_1", captchaField.text);
        Debug.Log("Username is: " + username.text + " Password is: " + password.text);
        WWW www = new WWW(url, form);
        StartCoroutine(sendLoginRequest(www, username.text));
        StartCoroutine(loadImage());
    }
    IEnumerator sendLoginRequest(WWW www, string username) {
        yield return www;
        if (www.text == "") {
            OpenErrorMessage(0);
        }
        else if(www.text == "Captcha Failed") {
            OpenErrorMessage(1);
        }
        else if(www.text == "Incorrent User Pass") {
            OpenErrorMessage(2);
        }
        else if(www.text == "Success") {
            CrossSceneInformation.Logged_In_Username = username;
            Debug.Log("Login Successful");
            Debug.Log(CrossSceneInformation.SessionID);
            SceneManager.LoadSceneAsync(1);
            GameManager.unPauseGame();

        }
        else {
            OpenErrorMessage(3);
            Debug.Log("Unknown Error Occurd.");
        }
        Debug.Log(www.text);
        //if (www.text == "Success") {
        //    CrossSceneInformation.Logged_In_Username = username;
        //    Debug.Log("Login Successful");
        //    Debug.Log(CrossSceneInformation.SessionID);
        //    SceneManager.LoadSceneAsync(1);
        //    GameManager.unPauseGame();
        //    Debug.Log("success !!!" + www.text);
        //}
        //else {
        //    Debug.Log(www.text);
        //}
    }
    IEnumerator loadImage() {
        string url = "http://127.0.0.1:8000/polls/dark/";
        WWW www = new WWW(url);
        yield return www;
        Debug.Log("Yellow");
        CaptchaKey = www.text;
        string imageURL = "http://127.0.0.1:8000/captcha/image/" + CaptchaKey + "/";
        Debug.Log(imageURL);
        WWW captchaWWW = new WWW(imageURL);
        yield return captchaWWW;
        img.overrideSprite = Sprite.Create(captchaWWW.texture, new Rect(0, 0, captchaWWW.texture.width, captchaWWW.texture.height), new Vector2(0, 0));
    }
    public void OnOffline() {
        SceneManager.LoadSceneAsync(2);
    }
    public void OpenErrorMessage(int errorCode) {
        ErrorPanel.SetActive(true);
        isErrorOpen = true;
        usernameField.text = "";
        passwordField.text = "";
        captchaField.text = "";
        if(errorCode == 0) {
            Errors[0].SetActive(true);
        }
        else if(errorCode == 1) {
            Errors[1].SetActive(true);
        }
        else if(errorCode == 2) {
            Errors[2].SetActive(true);
        }
        else if(errorCode == 3) {
            Errors[3].SetActive(true);
        }
    }
    public void CloseErrorMessage() {
        EventSystem.current.SetSelectedGameObject(usernameField.gameObject);
        isErrorOpen = false;
        ErrorPanel.SetActive(false);
        for(int i = 0; i < Errors.Length; i++) {
            Errors[i].SetActive(false);
        }
    }
}
