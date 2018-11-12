using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnImageUI : MonoBehaviour {
    public Sprite OTexture;
    public Sprite XTexture;
    public GameManager gameManager;
    private Image TurnImage;
	// Use this for initialization
	void Start () {
        TurnImage = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if(gameManager.currentPlayer == 1) {
            TurnImage.sprite = OTexture;
        }
        else {
            TurnImage.sprite = XTexture;
        }
	}
}
