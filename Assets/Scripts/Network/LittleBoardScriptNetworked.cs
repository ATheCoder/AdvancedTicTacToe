//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Networking;

//public class LittleBoardScriptNetworked : NetworkBehaviour {
//    public int boardState = 0;
//    public int boardNumber;
//    public GameManagerNetworked gameManager;

//    private void OnTriggerEnter2D(Collider2D collision) {
//        if (isServer && !Network.isClient) {
//            if (gameManager.allowedBoardNumber == -1 && collision.gameObject.GetComponent<PlayerNetworked>().playerNumber == gameManager.currentPlayer) {
//                RpcChooseBoard(collision.gameObject);
//                ChooseBoard(collision.gameObject);
//            }
//        }
//        else if (isServer && Network.isClient) {
//            if (gameManager.allowedBoardNumber == -1 && collision.gameObject.GetComponent<PlayerNetworked>().playerNumber == gameManager.currentPlayer)
//                RpcChooseBoard(collision.gameObject);
//        }
//        else if (collision.gameObject.GetComponent<PlayerNetworked>().isLocalPlayer) {
//            if (gameManager.allowedBoardNumber == -1 && collision.gameObject.GetComponent<PlayerNetworked>().playerNumber == gameManager.currentPlayer)
//                ChooseBoard(collision.gameObject);
//        }
//        if(gameManager.allowedBoardNumber == -1)
//            collision.gameObject.GetComponent<PlayerNetworked>().ResetPlayerPos();
//    }
//    [ClientRpc]
//    public void RpcChooseBoard(GameObject player) {
//        player.GetComponent<PlayerNetworked>().ResetPlayerPos();
//        gameManager.changedAllowedBoardNumber(boardNumber);
//    }
//    public void ChooseBoard(GameObject player) {
//        player.GetComponent<PlayerNetworked>().ResetPlayerPos();
//        gameManager.changedAllowedBoardNumber(boardNumber);
//    }
//}
