using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginErrorUI : MonoBehaviour {
    public Text errorText;
    public void changeErrorText(string _input) {
        errorText.text = _input;
    }
    public void Start() {
        StartCoroutine(decreaseTransprensy());
    }
    IEnumerator decreaseTransprensy() {
        while(errorText.color.a > 0.01) {
            errorText.color = new Color(errorText.color.r, errorText.color.g, errorText.color.b, errorText.color.a - 0.05f);
            yield return null;
        }
    }
}
