using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class BoardScriptNetworked : NetworkBehaviour {

    int[, ,] UnitStates;
    int[,] BoardStates;
    public GameManagerNetworked gameManager;
    private void Start() {
        UnitStates = new int[9, 3, 3];
        BoardStates = new int[3, 3];
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerNetworked>();

    }
    private void Update() {
    }
    public int CheckForWin(int[,] inputArray) {
        if (CheckRowsForWin(inputArray) == 1 || CheckColumsForWin(inputArray) == 1 || CheckLeftDiagonalForWin(inputArray) == 1 || CheckRightDiagonalForWin(inputArray) == 1) {
            return 1;
        }
        else if (CheckRowsForWin(inputArray) == 2 || CheckColumsForWin(inputArray) == 2 || CheckLeftDiagonalForWin(inputArray) == 2 || CheckRightDiagonalForWin(inputArray) == 2) {
            return 2;
        }
        else return 0;
    }
    public int CheckRowsForWin(int[,] inputArray) {
        Debug.Log("Checking Rows for Win...");
        for (int i = 0; i < 3; i++) {
            int player1Score = 0;
            int player2Score = 0;
            for (int j = 0; j < 3; j++) {
                if (inputArray[i, j] == 1) {
                    player1Score++;
                }
                else if (inputArray[i, j] == 2) {
                    player2Score++;
                }
                if (player1Score == 3) {
                    return 1;
                }
                else if (player2Score == 3) {
                    return 2;
                }
            }
        }
        return 0;
    }
    public int CheckColumsForWin(int[,] inputArray) {
        Debug.Log("Checking Colums For Win ...");
        for (int i = 0; i < 3; i++) {
            int player1Score = 0;
            int player2Score = 0;
            for (int j = 0; j < 3; j++) {
                if (inputArray[j, i] == 1) {
                    player1Score++;
                }
                else if (inputArray[j, i] == 2) {
                    player2Score++;
                }
                if (player1Score == 3) {
                    return 1;
                }
                else if (player2Score == 3) {
                    return 2;
                }
            }
        }
        return 0;
    }
    public int CheckLeftDiagonalForWin(int[,] inputArray) {
        Debug.Log("Checking Left Diagonal For Win ...");
        int player1score = 0;
        int player2score = 0;
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (i == j) {
                    if (inputArray[i, j] == 1) {
                        player1score++;
                    }
                    else if (inputArray[i, j] == 2) {
                        player2score++;
                    }
                }
            }
        }
        if (player1score == 3) {
            return 1;
        }
        else if (player2score == 3) {
            return 2;
        }
        else {
            return 0;
        }
    }
    public int CheckRightDiagonalForWin(int[,] inputArray) {
        Debug.Log("Checking Right Diagonal For Win ...");
        int player1Score = 0;
        int player2Score = 0;
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (i + j == 2) {
                    if (inputArray[i, j] == 1) {
                        player1Score++;
                    }
                    else if (inputArray[i, j] == 2) {
                        player2Score++;
                    }
                }
            }
        }
        if (player1Score == 3) {
            return 1;
        }
        else if (player2Score == 3) {
            return 2;
        }
        else {
            return 0;
        }
    }
    public void setBoardState(int BoardNumber, int State) {
        Debug.Log("Setting Board State on Server");
        int i = 0;
        while(BoardNumber >= 3) {
            i++;
            BoardNumber -= 3;
        }
        BoardStates[i, BoardNumber] = State;
    }
    public void AddMove(int BoardNumber, int UnitNumber, int PlayerNumber) {
        Debug.Log("Adding Move inside the Server ...");
        int i = 0;
        while(UnitNumber >= 3) {
            i++;
            UnitNumber -= 3;
        }
        Debug.Log("i is " + i + " UnitNumber is " + UnitNumber);
        if (UnitStates[BoardNumber, i, UnitNumber] == 0) {
            UnitStates[BoardNumber, i, UnitNumber] = PlayerNumber;
            if(CheckForWin(ConvertTo2D(UnitStates, BoardNumber)) != 0) {
                setBoardState(BoardNumber, CheckForWin(ConvertTo2D(UnitStates, BoardNumber)));
                gameManager.RpcChangeBoardStatesOnClients(BoardNumber, PlayerNumber);
                if(CheckForWin(BoardStates) != 0) {
                    gameManager.RpcDeclareWinner(CheckForWin(BoardStates));
                }
            }
        }
    }
    public int[,] ConvertTo2D(int[, ,] input, int Row) {
        Debug.Log("Converting Array to 2D");
        int[,] result = new int[3, 3];
        for(int i = 0; i < 3; i++) {
            for(int j = 0; j < 3; j++) {
                result[i, j] = input[Row, i, j];
            }
        }
        return result;
    }
}