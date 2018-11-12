using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceScript : MonoBehaviour {
    public Vector2 positionXY;
    public int spacePostion;
    public int boardNumber;
    public GameManager gameManager;
    public int currentState = 0;
    private void OnMouseDown() {
        if (currentState == 0 && boardNumber == gameManager.allowedBoardNumber && !GameManager.isGamePaused) {
            var boardScript = transform.parent.gameObject.GetComponent<BoardScript>();
            currentState = gameManager.currentPlayer;
            
            transform.GetChild(currentState - 1).gameObject.SetActive(true);
            transform.root.GetChild(boardNumber).gameObject.GetComponent<BoardScript>().addFilledUnits();
            gameManager.changePlayer();
            Debug.Log("Success " + transform.parent.name);
            if (boardScript.CheckForWin() == 1) {
                boardScript.setBoardState(1);
                gameManager.AddFullBoardNumber(boardNumber);
                gameManager.checkForGameWin();
            }
            else if (boardScript.CheckForWin() == 2) {
                boardScript.setBoardState(2);
                gameManager.AddFullBoardNumber(boardNumber);
                gameManager.checkForGameWin();
            }
            else if(boardScript.CheckForWin() == -1) {
                gameManager.AddFullBoardNumber(boardNumber);
                gameManager.checkForGameWin();
            }
            else;
                //gameManager.SetCamera();
            gameManager.changedAllowedBoardNumber(spacePostion);
        }
        else {
            Debug.Log("Can't put it there.");
        }
    }
}
