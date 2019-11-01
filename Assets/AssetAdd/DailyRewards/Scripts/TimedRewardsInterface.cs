/***************************************************************************\
Project:      Daily Rewards
Copyright (c) Niobium Studios.
Author:       Guilherme Nunes Barbosa (gnunesb@gmail.com)
\***************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

/* 
 * Timed Rewards Canvas is the User interface to show Timed rewards
 */
namespace com.niobiumstudios.dailyrewards
{
    public class TimedRewardsInterface : MonoBehaviour
    {
        public LuckySpinController lucky;
        public Button btnClaim;
        public Text txtTimer;

        // Rewards panel
        public GameObject panelReward;

        // Needed Constants
        private const string TIMED_REWARDS_TIME = "TimedRewardsTime";
        private const string FMT = "O";

        void Start ()
        {
            TimedRewards.instance.onCanClaim += OnCanClaim;

            Reset();
        }

        void Update()
        {
            // Updates the timer UI
            TimeSpan timer = TimedRewards.instance.timer;
            if (timer.TotalSeconds > 0)
            {
                btnClaim.enabled = false;
                lucky.TurnWheelByTime();
                txtTimer.text = string.Format("{0:D2}:{1:D2}",  timer.Minutes, timer.Seconds);
            }
        }

        // This where you add the player coins/cash/money/prize, etc.
        public void OnClaimClick()
        {
            TimedRewards.instance.Claim();
            Reset();

            if (panelReward)
            {
                panelReward.SetActive(true);
            }
            
        }

        // Resets player preferences. Debug Purposes
        public void OnResetClick()
        {
            Reset();
            btnClaim.enabled = false;
            TimedRewards.instance.Reset();
        }

        // Closes the Reward Panel
        public void OnCloseRewardClick()
        {
            if (panelReward)
            {
                panelReward.SetActive(false);
            }
            
        }

        private void Reset()
        {
            txtTimer.text = "";
            btnClaim.interactable = false;
        }

        // Delegate
        // Updates the UI
        private void OnCanClaim()
        {
            lucky.TurnWheelByTime2();

            btnClaim.enabled = true;
            btnClaim.interactable = true;
            txtTimer.text = "FREE";
        }

    }
}