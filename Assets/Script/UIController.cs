using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 作者: Foldcc
/// QQ: 1813547935
/// </summary>
public class UIController : MonoBehaviour {
	public GameObject[] UIview;

    public AudioClip startBackgroundAudio;

    public AudioClip backgroundAudio;

	public AudioClip loadAudio;

	public AudioClip lossAudio;

	public static int gameLevel = 0;

	public static int enemyCount = 0;

	public static int playerHp = 0;

	public static int gameTime = 0;

	public Text levelText;
	public Text enemyCountText;
	public Text playerHPText;
	public Text gameTimeText;
	public void StartViewWithIndex(int index){
		for(int i = 0 ; i< UIview.Length ; i++){
			UIview [i].SetActive (false);
		}
		UIview [index].SetActive (true);
        if (index == 0)
        {
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().clip = startBackgroundAudio;

        }else if (index == 1) {
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource> ().clip = backgroundAudio;
            
        } else if (index == 2) {
			GetComponent<AudioSource> ().clip = loadAudio;
		} else if (index == 3) {
            GetComponent<AudioSource>().loop = false;
            GetComponent<AudioSource> ().clip = lossAudio;
           
		}
		GetComponent<AudioSource> ().Stop ();
		GetComponent<AudioSource> ().Play ();

	}

	void OnGUI(){
		levelText.text = "Level: " + gameLevel;
		enemyCountText.text ="Enemy: " + enemyCount;
		playerHPText.text ="HP: " + playerHp;
		gameTimeText.text = "Time: " + gameTime;
		if (gameTime < 15)
			gameTimeText.color = Color.red;
		else
			gameTimeText.color = Color.white;
	}

}
