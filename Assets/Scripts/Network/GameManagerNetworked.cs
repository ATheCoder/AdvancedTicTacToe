using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerNetworked : NetworkBehaviour {
    [SyncVar] public int currentPlayer;
    [SyncVar] public int allowedBoardNumber;
    [SyncVar] public int currentConnectedPlayers = 0;
    public CameraScript cameraScript;
    public MainBoardScript mainBoardScript;
    public List<int> usedMiniBoards;
    public List<int> PlayerIDs;
    public bool IsChanged = false;
    private GameObject MainTicTacToeBoard;
    public BoardScriptNetworked BoardManager;
    public int bod;
    public Text WinnerText;
    public GameObject NetworkManager;
    // Use this for initialization
    private void Awake() {
        PlayerIDs = new List<int>();
    }
    void Start() {
        MainTicTacToeBoard = GameObject.Find("TicTacToeBoard");
        if (!isServer) {
            SetCamera();
            return;
        }
        currentPlayer = Random.Range(1, 3);
        allowedBoardNumber = Random.Range(0, 9);
        Debug.Log("Allowed Board is: " + allowedBoardNumber);
        SetCamera();
        usedMiniBoards = new List<int>();
        AddFullBoardNumber(2);
    }

    // Update is called once per frame
    void Update() {

    }
    [ClientRpc]
    public void RpcChangePlayer() {
        //if (IsChanged) {
        //    IsChanged = false;
        //    return;
        //}
        if (currentPlayer == 1) {
            currentPlayer = 2;
        }
        else {
            currentPlayer = 1;
        }
    }
    [Command]
    public void CmdChangePlayer() {
        currentPlayer = currentPlayer;
    }
    public void ChangePlayer() {
        if (currentPlayer == 1) {
            currentPlayer = 2;
        }
        else {
            currentPlayer = 1;
        }
    }
    public void SetCamera() {
        if (allowedBoardNumber != -1)
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
            //TODO: Disable all Little Colliders.
        }
        else if (allowedBoardNumber == -1) {
            allowedBoardNumber = _changedTo;
            cameraScript.setCameraPos(allowedBoardNumber);
            cameraScript.pickMode(true);
        }
        else {
            allowedBoardNumber = _changedTo;
            SetCamera();
        }
    }
    public void changeAllowedBoardNumberOnTheServer(int _changedTo) {
        if (usedMiniBoards.Contains(_changedTo)) {
            allowedBoardNumber = -1;
        }
        else if (allowedBoardNumber == -1) {
            allowedBoardNumber = _changedTo;
        }
        else {
            allowedBoardNumber = _changedTo;
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
        if (mainBoardScript.CheckForWin() == 1) {
            Debug.Log("Player 1 is the game Winner");
        }
        else if (mainBoardScript.CheckForWin() == 2) {
            Debug.Log("Player 2 is the game Winner");
        }
        //TODO: Winning Mechanism
    }
    public void addNumberOfPlayers() {
        currentConnectedPlayers++;
    }
    public int GetAllowedPlayer() {
        return currentPlayer;
    }
    //public void ChangeAllowedPlayer(int input) {
    //    currentPlayer = input;
    //}
    //[Command]
    //public void CmdaddMoveToTheServer(int BoardNumber, int UnitNumber, int PlayerNumber) {
    //    if(BoardManager)
    //        BoardManager.CmdAddMove();
    //}
    //[Command]
    //public void CmdaddMoveToTheServer(int BoardNumber, int UnitNumber, int PlayerNumber) {
    //    RpcaddMoveToTheServer(BoardNumber, UnitNumber, PlayerNumber);
    //}
    [ClientRpc]
    public void RpcChangeBoardStatesOnClients(int BoardNumber, int State) {
        if (MainTicTacToeBoard) {
            MainTicTacToeBoard.transform.GetChild(BoardNumber).gameObject.GetComponent<ChangeBoardStateNetworked>().setBoardState(State);
            changeLittleBoardState(BoardNumber, State);
        }
    }
    [ClientRpc]
    public void RpcDeclareWinner(int winner) {
        if (WinnerText) {
            WinnerText.text = WinnerText.text.Replace("X", winner.ToString());
            WinnerText.gameObject.SetActive(true);
        }
    }
    public void DeclareWinner(string winner) {
        if (WinnerText) {
            WinnerText.text = WinnerText.text.Replace("X", winner);
            WinnerText.gameObject.SetActive(true);
        }
    }
    public void ExitGame() {
        Application.Quit();
    }
    public void Logout() {
        SceneManager.LoadSceneAsync(2);
        Destroy(NetworkManager);
    }
}
