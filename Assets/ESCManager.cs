using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ESCManager : MonoBehaviour {
    public GameObject EscapeMenu;
    public Selectable firstSelected;
    private EventSystem system;
    private void Start() {
        system = EventSystem.current;
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (EscapeMenu.activeInHierarchy) {
                closeMenu(EscapeMenu);
            }
            else {
                GameManager.PauseGame();
                EscapeMenu.SetActive(true);
                firstSelected.GetComponent<Button>().Select();
            }
        }
	}
    public void closeMenu(GameObject menu) {
        GameManager.unPauseGame();
        EscapeMenu.SetActive(false);
        menu.SetActive(false);
    }
    public void openMenu(GameObject menu) {
        GameManager.PauseGame();
        menu.SetActive(true);
    }
}
