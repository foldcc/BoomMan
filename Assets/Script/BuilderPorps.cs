using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 作者: Foldcc
/// QQ: 1813547935
/// </summary>
public class BuilderPorps : MonoBehaviour {

	public Sprite[] propsSprite;

	private bool isActive = false;

	private int propsID; //0 生命值buff  1 移动速度buff 2 炸弹CDbuff  3 爆炸范围  
	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Boom") && isActive == false) {
			
			isActive = true;
			//随机此道具功能
			propsID = Random.Range (0, propsSprite.Length); 
			//开启isTrigger
			this.GetComponent<BoxCollider2D> ().isTrigger = true;
			//更改对应道具的sprite
			this.GetComponent<SpriteRenderer> ().sprite = propsSprite [propsID];

			//播放道具的动画效果
			StartCoroutine(porpsAnimation());

		} else if (other.CompareTag ("Player") && isActive) {
			switch (propsID) {
			case 0:
				other.GetComponent<PlayerController> ().playerBUFF (0 , 1);
				break;
			case 1:
				other.GetComponent<PlayerController> ().playerBUFF (1 , 1);
				break;
			case 2:
				other.GetComponent<PlayerController> ().playerBUFF (2 , -0.7f);
				break;
			case 3:  
				other.GetComponent<PlayerController> ().playerBUFF (4 , 1);
				break;
			case 4:
				UIController.gameTime += 25;
				break;
			}

			Destroy (this.gameObject);
		}
	}

	IEnumerator porpsAnimation(){
		SpriteRenderer s = this.GetComponent<SpriteRenderer> ();
		for (int i = 0; i <= 5; i++) {
			s.color = Color.yellow;
			yield return new WaitForSeconds (0.2f);
			s.color = Color.white;
			yield return new WaitForSeconds (0.2f);
		}
		yield return 0;
	}
}
