using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour {
    GameObject[,] array2Da;
    public int boardState = 0;
    public int boardNumber;
    public int filledUnits = 0;
    public GameManager gameManager;
    private void Start() {
        array2Da = new GameObject[3, 3] { { transform.GetChild(0).gameObject, transform.GetChild(1).gameObject, transform.GetChild(2).gameObject },
            { transform.GetChild(3).gameObject, transform.GetChild(4).gameObject, transform.GetChild(5).gameObject },
            { transform.GetChild(6).gameObject, transform.GetChild(7).gameObject, transform.GetChild(8).gameObject } };
    }
    private void OnMouseDown() {
        if (gameManager.allowedBoardNumber == -1 && !GameManager.isGamePaused) {
            gameManager.changedAllowedBoardNumber(boardNumber);
        }
    }
    public int CheckForWin() {
        if (CheckRowsForWin() == 1 || CheckColumsForWin() == 1 || CheckLeftDiagonalForWin() == 1 || CheckRightDiagonalForWin() == 1) {
            return 1;
        }
        else if (CheckRowsForWin() == 2 || CheckColumsForWin() == 2 || CheckLeftDiagonalForWin() == 2 || CheckRightDiagonalForWin() == 2) {
            return 2;
        }
        else if (filledUnits == 9) {
            setBoardState(-1);
            return -1;
        }
        else
            return 0;
    }
    public int CheckRowsForWin() {
        for (int i = 0; i < 3; i++) {
            int player1Score = 0;
            int player2Score = 0;
            for (int j = 0; j < 3; j++) {
                if (array2Da[i, j].GetComponent<SpaceScript>().currentState == 1) {
                    player1Score++;
                }
                else if (array2Da[i, j].GetComponent<SpaceScript>().currentState == 2) {
                    player2Score++;
                }
                if(player1Score == 3) {
                    return 1;
                }
                else if(player2Score == 3) {
                    return 2;
                }
            }
        }
        return 0;
    }
    public int CheckColumsForWin() {
        for (int i = 0; i < 3; i++) {
            int player1Score = 0;
            int player2Score = 0;
            for (int j = 0; j < 3; j++) {
                if (array2Da[j, i].GetComponent<SpaceScript>().currentState == 1) {
                    player1Score++;
                }
                else if (array2Da[j, i].GetComponent<SpaceScript>().currentState == 2) {
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
    public int CheckLeftDiagonalForWin() {
        int player1Score = 0;
        int player2Score = 0;
        for(int i = 0; i < 3; i++) {
            for(int j = 0; j < 3; j++) {
                if (i == j) {
                    if (array2Da[i, j].GetComponent<SpaceScript>().currentState == 1) {
                        player1Score++;
                    }
                    else if(array2Da[i, j].GetComponent<SpaceScript>().currentState == 2) {
                        player2Score++;
                    }
                }
            }
        }
        if(player1Score == 3) {
            return 1;
        }
        else if(player2Score == 3) {
            return 2;
        }
        else {
            return 0;
        }
    }
    public int CheckRightDiagonalForWin() {
        int player1Score = 0;
        int player2Score = 0;
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (i + j == 2) {
                    if (array2Da[i, j].GetComponent<SpaceScript>().currentState == 1) {
                        player1Score++;
                    }
                    else if (array2Da[i, j].GetComponent<SpaceScript>().currentState == 2) {
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
    public void setBoardState(int state) {
        for(int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        boardState = state;
        if(state == -1) {
            transform.GetChild(9).gameObject.SetActive(true);
            transform.GetChild(10).gameObject.SetActive(true);
            return;
        }
        transform.GetChild(state + 8).gameObject.SetActive(true);
    }
    public void addFilledUnits() {
        filledUnits++;
    }
}
