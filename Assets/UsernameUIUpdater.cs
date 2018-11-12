using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsernameUIUpdater : MonoBehaviour {
    Text textField;
    private void Start() {
        textField = GetComponent<Text>();
    }
    private void Update() {
        textField.text = "Username: " + CrossSceneInformation.Logged_In_Username;
    }
}
