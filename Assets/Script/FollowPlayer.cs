using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 作者: Foldcc
/// QQ: 1813547935
/// </summary>
public class FollowPlayer : MonoBehaviour {
	public static Transform player;
	public static int xitm = 0;
	public static int yitm = 0;
	// Update is called once per frame
	void Update () {
		if (player != null) {
			float x = Mathf.Lerp (this.transform.position.x , player.position.x , 0.2f);
			float y = Mathf.Lerp (this.transform.position.y , player.position.y , 0.2f);
			this.transform.position = new Vector3 (x , y , this.transform.position.z);
		}
		border();

	}
	void border(){

		if ((xitm - 6) <= 0 || (yitm - 3) <= 0) {
			transform.position = new Vector3 (0, 0, transform.position.z);
		}else{
			if (this.transform.position.x > (xitm-6)) {
				transform.position = new Vector3 (xitm-6 , transform.position.y , transform.position.z);
			}
			if (this.transform.position.x < -(xitm-6)) {
				transform.position = new Vector3 (-(xitm-6) , transform.position.y , transform.position.z);
			}
			if (this.transform.position.y > (yitm-3)) {
				transform.position = new Vector3 (transform.position.x , yitm-3  , transform.position.z);
			}
			if (this.transform.position.y < -(yitm-3)) {
				transform.position = new Vector3 (transform.position.x , -(yitm-3) , transform.position.z);
			}
		}
	}
}
