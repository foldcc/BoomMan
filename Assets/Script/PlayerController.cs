using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 作者: Foldcc
/// QQ: 1813547935
/// 玩家控制器
/// </summary>
public class PlayerController : MonoBehaviour {
	//炸弹
	public GameObject bomb;
	//玩家移动速度
	public float speed;
	//玩家生命值
	public int playerHP;
	//玩家放置炸弹的最大CD时间
	public float bombCDMax;

	//爆炸范围
	public int bombScope;
	//引爆时间
	private float bombFireTime;

	//主角动画
	private Animator playerAnimator;

	//玩家当前放置炸弹剩余CD时间
	public float bombCD;

	private Rigidbody2D rb2D;
    
    private float moveX , moveY;
	void Start() {
		rb2D = GetComponent<Rigidbody2D>();
		playerAnimator = GetComponent<Animator> ();
	}
	void FixedUpdate() {
		if (bombCD > 0) {
			bombCD -= Time.fixedDeltaTime;
		}
	}

	void Update(){
		playerMove ();
        fire();
    }


	IEnumerator invincible(int waitTime){
		this.gameObject.tag = "Untagged";
		for (int i = waitTime; i > 0; i--) {
			this.gameObject.GetComponent<SpriteRenderer> ().color = new Color(1 , 1 , 1 , 0);
			yield return new WaitForSeconds (0.25f);
			this.gameObject.GetComponent<SpriteRenderer> ().color = new Color(1 , 1 , 1 , 1);
			yield return new WaitForSeconds (0.25f);
			this.gameObject.GetComponent<SpriteRenderer> ().color = new Color(1 , 1 , 1 , 0);
			yield return new WaitForSeconds (0.25f);
			this.gameObject.GetComponent<SpriteRenderer> ().color = new Color(1 , 1 , 1 , 1);
			yield return new WaitForSeconds (0.25f);
		}
		this.gameObject.tag = "Player";
	}
	//碰撞检测
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Door")) {
			if (UIController.enemyCount <= 0) {
				//游戏通关
				GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GameContorller> ().StartGame();
			}
		}else if(other.gameObject.CompareTag ("ENEMY")){
			playerBUFF (0 , -1);
		}
	}

    //主角初始化
    public void initPlayer(float speedValue, int HPValue, int bombCDmaxValue, int bombscope){
		speed = speedValue;
		playerHP = HPValue;
		bombCDMax = bombCDmaxValue;
		bombScope = bombscope;
		bombFireTime = 2;
		UIController.playerHp = playerHP;
	}

	void fire(){
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (bombCD <= 0)
            {
                //播放放下炸弹音效
                this.GetComponent<AudioSource>().Play();

                GameObject boom = Instantiate(bomb, new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)), transform.rotation) as GameObject;
                //激活炸弹
                boom.GetComponent<Bomb>().initBomb(bombScope, bombFireTime);
                //重置CD
                bombCD = bombCDMax;
            }
        }
	}

	//玩家Buff控制方法 count：0 生命值buff  1 移动速度buff 2 炸弹CDbuff  3 炸弹伤害 4 爆炸范围  5 引爆时间
	public void playerBUFF(int count , float value){
		switch (count) {
			case 0:
				playerHP += (int)value;
				UIController.playerHp = playerHP;
				//玩家死亡
				if (playerHP <= 0) {
					GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GameContorller> ().gameOver ();
					Destroy (this.gameObject);
				} else {
					StartCoroutine (invincible(2));
				}
				break;
			case 1:
				speed += value;
				if (speed >= 8) {
					speed = 8;
				}
				break;
			case 2:
				bombCDMax += value;
				if (bombCDMax < 0.5f) {
					bombCDMax = 0.5f;
				}
				break;
//			case 3:
//				bombATK += value;
//				break;
		case 4:
			bombScope += (int)value;
			Debug.Log ("bombscope"+bombScope);
				break;
			case 5:
				bombFireTime += (int)value;
				if (bombFireTime < 0.5f) {
					bombFireTime = 0.5f;
				}
				break;
		}
	}


	//玩家移动方法
	void playerMove(){
        moveX = 0;
        moveY = 0;
		if (Input.GetKey(KeyCode.W)) {
            moveY = 1;
            playerAnimator.SetBool("ToLeft", false);
            playerAnimator.SetBool("ToDown", false);
            playerAnimator.SetBool("ToRight", false);
            playerAnimator.SetBool ("ToUp", true);
		}
        else if (Input.GetKey(KeyCode.A)) {
            moveX = -1;
            playerAnimator.SetBool("ToRight", false);
            playerAnimator.SetBool("ToUp", false);
            playerAnimator.SetBool("ToDown", false);
            playerAnimator.SetBool ("ToLeft", true);
        }
        else if (Input.GetKey(KeyCode.S)) {
            moveY = -1;
            playerAnimator.SetBool("ToRight", false);
            playerAnimator.SetBool("ToUp", false);
            playerAnimator.SetBool("ToLeft", false);
            playerAnimator.SetBool ("ToDown", true);
		}
        else if (Input.GetKey(KeyCode.D)) {
            moveX = 1;
            playerAnimator.SetBool("ToUp", false);
            playerAnimator.SetBool("ToLeft", false);
            playerAnimator.SetBool("ToDown", false);
            playerAnimator.SetBool("ToRight", true);
        }
        else
        {
			playerAnimator.SetBool ("ToRight", false);
            playerAnimator.SetBool("ToUp", false);
            playerAnimator.SetBool("ToLeft", false);
            playerAnimator.SetBool("ToDown", false);
        }

        rb2D.MovePosition((Vector2)transform.position + new Vector2(moveX  , moveY) * speed * Time.deltaTime);
    }
}
