using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 作者: Foldcc
/// QQ: 1813547935
/// </summary>
public class Bomb : MonoBehaviour {
	//爆炸特效
	public GameObject boom;
	//爆炸范围
	private int bombScope;
	//引爆时间
	private float bombFireTime;

	public void initBomb(int scope ,float fireTime){
		bombScope = scope;
		bombFireTime = fireTime;
		StartCoroutine (startBoom());
	}

	void OnTriggerExit2D( Collider2D other ){
		this.GetComponent<BoxCollider2D> ().isTrigger = false;
	}
	void newBoom(Vector2 ve){
		

		for (int i = 1; i <= bombScope; i++) {
			if (MapController.isSuperWallPos((Vector2)transform.position + ve*i)) {
				break;
			} else {
				Instantiate (boom , (Vector2)transform.position + ve*i , Quaternion.identity);
			}

		}
	}

	IEnumerator startBoom(){
		yield return new WaitForSeconds (bombFireTime);
		Instantiate (boom, transform.position, Quaternion.identity);
		newBoom (Vector2.up);
		newBoom (Vector2.down);
		newBoom (Vector2.left);
		newBoom (Vector2.right);
        GameObject.FindGameObjectWithTag("Respawn").GetComponent<AudioSource>().Play();
		Destroy (this.gameObject);
	}
}
