using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 作者: Foldcc
/// QQ: 1813547935
/// 负责控制游戏关卡生成，生成主角 ， 判定胜利和失败
/// </summary>
public class GameContorller : MonoBehaviour {
    // 仅仅用于测试
    public InputField[] test;

	public GameObject player;

	private int gameLevel = 0;

	public int wallCount;

	public int enemyCount;

	public int xitm;

	public int yitm;

	private GameObject gamePlayer;

	public void ExitGame(){
		Application.Quit ();
	}
	void Start(){
		initGame ();
		this.GetComponent<UIController> ().StartViewWithIndex (0);
	}

	// Update is called once per frame
	void Update () {
		
	}
	//开始下一关
	public void StartGame(){
		StartCoroutine(loadLevel ());
	}

	//关卡控制器
	void levelContorller(int levelCount){
		xitm = 6 + 2 *(levelCount/3);
		yitm = 3 + 2 *(levelCount / 3);
		if (xitm > 16) {
			xitm = 16;
		}
		if (yitm > 9) {
			yitm = 9;
		}

		FollowPlayer.xitm = xitm;
		FollowPlayer.yitm = yitm;

		//x表示地图所有空位的百分比 0-1之间 如：0.2 表示 取整个地图20%的空格子
		float x = (0.3f + (levelCount * 0.07f));
		if (x > 0.7f) {
			x = 0.7f;
		}

		wallCount = (int)(xitm * yitm * x) + 5;

		enemyCount = (int)(levelCount*1.25f + 1);

		//调用地图控制器的初始化地图方法
		this.GetComponent<MapController> ().initMap (wallCount , enemyCount , xitm , yitm);

		//初始化猪脚
		initPlayer();

		//关闭计时器
		StopCoroutine ("gameTimeCount");

        //开启计时器
        StartCoroutine ("gameTimeCount", (int)(80 + levelCount * 30f)); 

        //调出游戏UI
        this.GetComponent<UIController> ().StartViewWithIndex (1);
	}

	//加载关卡
	IEnumerator loadLevel(){
		gameLevel++;
		UIController.gameLevel = gameLevel;
		this.GetComponent<UIController> ().StartViewWithIndex (2);
		yield return new WaitForSeconds (2.5f);
		levelContorller (gameLevel);
	}

    public void initPlayer() {
		//生成主角
		if (gamePlayer == null) {
			gamePlayer = Instantiate (player, new Vector2 (-(xitm + 1), yitm + 1), Quaternion.identity) as GameObject;
			//初始化player属性
			gamePlayer.GetComponent<PlayerController> ().initPlayer (4, 3 , 2 , 1);
		} else {
			gamePlayer.transform.position = new Vector2 (-(xitm + 1), yitm + 1);
		}

		FollowPlayer.player = gamePlayer.transform;
	}
	//游戏结束
	public void gameOver(){
		if (gamePlayer != null) {
			Destroy (gamePlayer);
		}
		this.GetComponent<UIController> ().StartViewWithIndex (3);
	}

	//重新开始游戏
	public void gameRese(){
		initGame ();
		StartGame ();
	}
	//初始化属性
	public void initGame(){
		UIController.enemyCount = 0;
		gameLevel = 0;
		if (gamePlayer != null) {
			Destroy (gamePlayer);
		}
	}

	IEnumerator gameTimeCount(int count){
		//为游戏时间赋值
		UIController.gameTime = count;
		//倒计时游戏时间
		while (true) {
			yield return new WaitForSeconds (1);
			UIController.gameTime--;
			if (UIController.gameTime <= 0) {
				//游戏结束
				gameOver ();
				break;
			}
		}
	}
}