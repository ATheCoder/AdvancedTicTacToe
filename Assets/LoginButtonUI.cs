using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginButtonUI : MonoBehaviour {
    public GameObject LoginPanel;
	public void OnPress() {
        if (LoginPanel.activeInHierarchy) {
            LoginPanel.SetActive(false);
            GameManager.unPauseGame();
        }
        else {
            LoginPanel.SetActive(true);
            GameManager.PauseGame();
        }
    }
}
