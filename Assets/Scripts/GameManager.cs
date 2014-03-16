using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public int coinsCollected = 0;
	public GUIText scoreText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CoinCollected(Coin coin) {
		//Debug.Log("Coin collected");
		coin.gameObject.audio.Play();
		Destroy (coin.gameObject, coin.gameObject.audio.clip.length);
		coin.gameObject.renderer.enabled = false;
		coinsCollected++;
		if(scoreText != null) {
			scoreText.text = coinsCollected.ToString();
		}
	}
}
