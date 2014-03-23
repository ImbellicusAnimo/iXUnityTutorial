using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	GameManager gameManager;

	ThirdPersonCameraController cameraController;
	public Texture2D luckyFaceTexture;
	public Texture2D sadFaceTexture;
	public Texture2D normalFaceTexture;

	Transform face;

	// Use this for initialization
	void Start () {
		this.gameManager = FindObjectOfType<GameManager>();

		// Face-Geometrie suchen und normales Gesicht setzen
		face = this.transform.FindChild("Face");
		face.renderer.sharedMaterial.mainTexture = normalFaceTexture;
	}

	// Änderung der Gesichtstextur bei Spielende
	// Wird vom Gamemanager aufgerufen
	public void Finished(int score) {		
		this.GetComponent<ThirdPersonController>().enabled = false;
		
		// Wenn Ziel erreicht, also Punkte gemacht wurden -> glückliches Gesicht
		if(score > 0) {
			face.renderer.sharedMaterial.mainTexture = luckyFaceTexture;
		} else { // z.B wenn die Zeitabgelaufen ist
			face.renderer.sharedMaterial.mainTexture = sadFaceTexture;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Coin")) {
			//Debug.Log("CharacterCollision Coin");
			if(gameManager != null) {
				gameManager.SendMessage("CoinCollected",other.GetComponent<Coin>());
			}
			
		} else if(other.CompareTag("Finish")) {
			Debug.Log("CharacterCollision Finish");
			if(gameManager != null) {
				gameManager.SendMessage("FinishReached");
			}
		}
	}
}
