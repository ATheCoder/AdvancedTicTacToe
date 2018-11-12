//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Networking;

//public class SpaceScriptNetworked : NetworkBehaviour {
//    public Vector2 positionXY;
//    public int spacePostion;
//    public int boardNumber;
//    public GameManagerNetworked gameManager;
//    public int currentState = 0;
//    private BoardScriptNetworked boardManager;
//    private void Start() {
//        if (isServer) {
//            boardManager = GameObject.Find("BoardManager").GetComponent<BoardScriptNetworked>();
//        }
//        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerNetworked>();
//    }
//    private void OnTriggerEnter2D(Collider2D collision) {
//        if (isServer && !Network.isClient && gameManager.allowedBoardNumber == boardNumber) {
//            if(collision.gameObject.GetComponent<PlayerNetworked>().playerNumber == gameManager.currentPlayer && currentState == 0) {
//                RpcMakeChange(collision.gameObject);
//                makeChangeOnTheServer(collision.gameObject);
//                boardManager.AddMove(boardNumber, spacePostion, collision.gameObject.GetComponent<PlayerNetworked>().playerNumber);
//            }
//        }
//        else if(isServer && Network.isClient && gameManager.allowedBoardNumber == boardNumber) {
//            if (collision.gameObject.GetComponent<PlayerNetworked>().playerNumber == gameManager.currentPlayer && currentState == 0) {
//                RpcMakeChange(collision.gameObject);
//                boardManager.AddMove(boardNumber, spacePostion, collision.gameObject.GetComponent<PlayerNetworked>().playerNumber);
//            }
//        }
//        else if(collision.gameObject.GetComponent<PlayerNetworked>().isLocalPlayer && gameManager.allowedBoardNumber == boardNumber) {
//            MakeChange(collision.gameObject);
//        }
//    }
//    [ClientRpc]
//    public void RpcMakeChange(GameObject player) {
//        currentState = player.GetComponent<PlayerNetworked>().playerNumber;
//        //player.GetComponent<PlayerNetworked>().gameManager.CmdChangePlayer();
//        if(!player.GetComponent<PlayerNetworked>().isLocalPlayer || isServer)
//            gameManager.changedAllowedBoardNumber(spacePostion);
//    }
//    public void MakeChange(GameObject player) {
//        var boardScript = transform.parent.gameObject.GetComponent<BoardScript>();
//        currentState = player.GetComponent<PlayerNetworked>().playerNumber;
//        gameManager.changedAllowedBoardNumber(spacePostion);
//    }
//    public void makeChangeOnTheServer(GameObject player) {
//        currentState = player.GetComponent<PlayerNetworked>().playerNumber;
//        gameManager.changeAllowedBoardNumberOnTheServer(spacePostion);
//        gameManager.ChangePlayer();
//    }
//}
