using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 作者: Foldcc
/// QQ: 1813547935
/// </summary>
public class Boom : MonoBehaviour {
	public GameObject door;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject , 0.5f);
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			
			other.gameObject.GetComponent<PlayerController> ().playerBUFF (0, -1);	

		}else if (other.gameObject.CompareTag ("Wall")) {
			Destroy (other.gameObject);
			Destroy (this.gameObject);
		}else if(other.gameObject.CompareTag ("Door")){
			
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<MapController>().enemyGenerator(5);

		}else if (other.gameObject.CompareTag ("DoorWall")) {
			
			Instantiate (door , other.gameObject.transform.position , Quaternion.identity);
			Destroy (this.gameObject);
			Destroy (other.gameObject);
		}
        Destroy(this.gameObject);
    }

}
