using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 作者: Foldcc
/// QQ: 1813547935
/// </summary>
public class DoorWall : MonoBehaviour {
	
	void OnTirggerEnter2D(Collider2D other){
		Debug.Log (other.tag);
		if (other.gameObject.CompareTag ("Player")) {
			if (UIController.enemyCount == 0) {
				//游戏通关
				//GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GameContorller> ().loadLevel();
				Debug.Log ("游戏通关");
			}
		}
	}
}
