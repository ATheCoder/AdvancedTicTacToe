using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBoardStateNetworked : MonoBehaviour {
    public int boardState = 0;
    public int boardNumber;
    public GameManagerNetworked gameManager;

    public void setBoardState(int state) {
        GetComponent<SpriteRenderer>().enabled = false;
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(state + 8).gameObject.SetActive(true);
        boardState = state;
    }
}
