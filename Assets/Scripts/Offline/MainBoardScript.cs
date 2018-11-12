using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBoardScript : MonoBehaviour {
    GameObject[,] array2Da;
    public GameManager gameManager;
    // Use this for initialization
    void Start () {
        array2Da = new GameObject[3, 3] { { transform.GetChild(0).gameObject, transform.GetChild(1).gameObject, transform.GetChild(2).gameObject },
            { transform.GetChild(3).gameObject, transform.GetChild(4).gameObject, transform.GetChild(5).gameObject },
            { transform.GetChild(6).gameObject, transform.GetChild(7).gameObject, transform.GetChild(8).gameObject } };
    }
	
	// Update is called once per frame
	void Update () {
        if(gameManager.allowedBoardNumber == -1) {
            changeColliders(true);
        }
        else {
            changeColliders(false);
        }
	}
    public void changeMiniBoardState(int boardNumber, int winningPlayer) {
        int row = 0;
        while(boardNumber > 2) {
            boardNumber -= 3;
            row++;
        }
        var boardScript = array2Da[row, boardNumber].GetComponent<BoardScript>();

    }
    public void changeColliders(bool state) {
        for(int i = 0; i < 3; i++) {
            for(int j = 0; j < 3; j++) {
                var childComps = array2Da[i, j].GetComponentsInChildren<Collider2D>();
                foreach(Collider2D component in childComps) {
                    component.enabled = !state;
                }
                array2Da[i, j].GetComponent<Collider2D>().enabled = state;
            }
        }
    }






    public int CheckForWin() {
        if (CheckRowsForWin() == 1 || CheckColumsForWin() == 1 || CheckLeftDiagonalForWin() == 1 || CheckRightDiagonalForWin() == 1) {
            return 1;
        }
        else if (CheckRowsForWin() == 2 || CheckColumsForWin() == 2 || CheckLeftDiagonalForWin() == 2 || CheckRightDiagonalForWin() == 2) {
            return 2;
        }
        else return 0;
    }
    public int CheckRowsForWin() {
        for (int i = 0; i < 3; i++) {
            int player1Score = 0;
            int player2Score = 0;
            int drawBoards = 0;
            for (int j = 0; j < 3; j++) {
                if (array2Da[i, j].GetComponent<BoardScript>().boardState == 1) {
                    player1Score++;
                }
                else if (array2Da[i, j].GetComponent<BoardScript>().boardState == 2) {
                    player2Score++;
                }
                else if (array2Da[i, j].GetComponent<BoardScript>().boardState == -1) {
                    drawBoards++;
                }
                if (player1Score + drawBoards >= 3 && player1Score > 0) {
                    return 1;
                }
                else if (player2Score + drawBoards >= 3 && player2Score > 0) {
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
            int drawBoards = 0;
            for (int j = 0; j < 3; j++) {
                if (array2Da[j, i].GetComponent<BoardScript>().boardState == 1) {
                    player1Score++;
                }
                else if (array2Da[j, i].GetComponent<BoardScript>().boardState == 2) {
                    player2Score++;
                }
                else if (array2Da[j, i].GetComponent<BoardScript>().boardState == -1) {
                    drawBoards++;
                }
                if (player1Score + drawBoards >= 3 && player1Score > 0) {
                    return 1;
                }
                else if (player2Score + drawBoards >= 3 && player2Score > 0) {
                    return 2;
                }
            }
        }
        return 0;
    }
    public int CheckLeftDiagonalForWin() {
        int player1Score = 0;
        int player2Score = 0;
        int drawBoards = 0;
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (i == j) {
                    if (array2Da[i, j].GetComponent<BoardScript>().boardState == 1) {
                        player1Score++;
                    }
                    else if (array2Da[i, j].GetComponent<BoardScript>().boardState == 2) {
                        player2Score++;
                    }
                    else if (array2Da[i, j].GetComponent<BoardScript>().boardState == -1) {
                        drawBoards++;
                    }
                }
            }
        }
        if (player1Score + drawBoards >= 3 && player1Score > 0) {
            return 1;
        }
        else if (player2Score + drawBoards >= 3 && player2Score > 0) {
            return 2;
        }
        else {
            return 0;
        }
    }
    public int CheckRightDiagonalForWin() {
        int player1Score = 0;
        int player2Score = 0;
        int drawBoards = 0;
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (i + j == 2) {
                    if (array2Da[i, j].GetComponent<BoardScript>().boardState == 1) {
                        player1Score++;
                    }
                    else if (array2Da[i, j].GetComponent<BoardScript>().boardState == 2) {
                        player2Score++;
                    }
                    else if (array2Da[i, j].GetComponent<BoardScript>().boardState == -1) {
                        drawBoards++;
                    }
                }
            }
        }
        if (player1Score + drawBoards >= 3 && player1Score > 0) {
            return 1;
        }
        else if (player2Score + drawBoards >= 3 && player2Score > 0) {
            return 2;
        }
        else {
            return 0;
        }
    }





}
