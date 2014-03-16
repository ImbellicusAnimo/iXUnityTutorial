using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private GameManager gameManager;
	// Use this for initialization
	void Start () {
		this.gameManager = FindObjectOfType<GameManager>(); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Coin")) {
			//Debug.Log("CharacterCollision Coin");
			if(gameManager != null) {
				gameManager.SendMessage("CoinCollected", other.GetComponent<Coin>());
			}
		}
	}
}
