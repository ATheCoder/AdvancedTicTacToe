using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public int currentPlayer;
    public int allowedBoardNumber;
    public CameraScript cameraScript;
    public MainBoardScript mainBoardScript;
    public List<int> usedMiniBoards;
    public GameObject WinPopUp;
    public Text WinnerText;
    public static bool isGamePaused = false;
    // Use this for initialization
    void Start () {
        currentPlayer = Random.Range(1, 3);
        allowedBoardNumber = Random.Range(0, 9);
        Debug.Log("Allowed Board is: " + allowedBoardNumber);
        SetCamera();
        usedMiniBoards = new List<int>();
    }
	
	// Update is called once per frame
	void Update () {

    }
    public void changePlayer() {
        if(currentPlayer == 1) {
            currentPlayer = 2;
        }
        else {
            currentPlayer = 1;
        }
        GetComponent<TurnTimer>().ResetTimer();
    }
    public void SetCamera() {
        if(allowedBoardNumber != -1)
        StartCoroutine(zoomOutZoomIn());
        //cameraScript.setCameraPos(allowedBoardNumber);
    }
    IEnumerator zoomOutZoomIn() {
        cameraScript.pickMode(false);
        cameraScript.setCameraPos(4);
        yield return new WaitForSeconds(0.5f);
        cameraScript.setCameraPos(allowedBoardNumber);
        cameraScript.pickMode(true);
    }
    public void changeLittleBoardState(int boardNumber, int winningPlayer) {
        AddFullBoardNumber(boardNumber);
    }
    public void changedAllowedBoardNumber(int _changedTo) {
        if (usedMiniBoards.Contains(_changedTo)) {
            allowedBoardNumber = -1;
            choosePhase();
        }
        else if(allowedBoardNumber == -1) {
            allowedBoardNumber = _changedTo;
            cameraScript.setCameraPos(allowedBoardNumber);
            cameraScript.pickMode(true);
        }
        else {
            allowedBoardNumber = _changedTo;
            SetCamera();
        }
    }
    public void AddFullBoardNumber(int add) {
        usedMiniBoards.Add(add);
    }
    public void choosePhase() {
        cameraScript.pickMode(false);
        cameraScript.setCameraPos(4);
    }
    public void checkForGameWin() {
        if(mainBoardScript.CheckForWin() == 1) {
            cameraScript.GameEnd();
            Time.timeScale = 0;
            Debug.Log("Player O is the game Winner");
            WinnerText.text = "Player O is the game Winner";
            WinPopUp.SetActive(true);
        }
        else if (mainBoardScript.CheckForWin() == 2) {
            cameraScript.GameEnd();
            Time.timeScale = 0;
            Debug.Log("Player X is the game Winner");
            WinnerText.text = "Player X is the game Winner";
            WinPopUp.SetActive(true);
        }
        //TODO: Winning Mechanism
    }
    public void ChangeScene(int number) {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(number);
    }
    public static void PauseGame() {
        isGamePaused = true;
        Time.timeScale = 0;
    }
    public static void unPauseGame() {
        isGamePaused = false;
        Time.timeScale = 1;
    }
    public static void ExitGame() {
        Application.Quit();
    }
}
