using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAds : MonoBehaviour {

	public Button showRewardBtn, showFullBtn;
	public Text desTxt;
	// Use this for initialization
	void Start () {
		showRewardBtn.onClick.AddListener(ShowReward);
		showFullBtn.onClick.AddListener(ShowFull);
	}
	
	void ShowReward(){
		desTxt.text = "Start Show RW";
		ManagerAds.Ins.ShowRewardedVideo(OnRewardComplete);
	}

	void OnRewardComplete(bool isComplete){
		if(isComplete){
			desTxt.text = "Complete RW";
		}else{
			desTxt.text = "Request RW Fail";
		}
	}

	void ShowFull(){
		desTxt.text = "Start Show Full";
		ManagerAds.Ins.ShowInterstitial(OnCloseFull);
	}

	void OnCloseFull(){
		desTxt.text = "Close Full";
	}

}
