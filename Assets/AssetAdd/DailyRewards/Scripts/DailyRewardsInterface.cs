/***************************************************************************\
Project:      Daily Rewards
Copyright (c) Niobium Studios.
Author:       Guilherme Nunes Barbosa (gnunesb@gmail.com)
\***************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System;

/* 
 * Daily Rewards Canvas is the User interface to show Daily rewards using Unity 4.6
 */
namespace com.niobiumstudios.dailyrewards
{
    public class DailyRewardsInterface : MonoBehaviour
    {

        public GameObject ParentAll;

        // Prefab containing the daily reward
        public GameObject dailyRewardPrefab;

        // Rewards panel
        public GameObject panelReward;
        public Text txtReward;
        public Image imgReward;

        // Claim Button
        public Button btnClaim;
        public Button btnClose;

        [Header("Animtion Home")]
        public GameObject animationHome;

        // How long until next claim
        public Text txtTimeDue;
        public Text txtTimePersent;

        // The Grid that contains the rewards
        public GridLayoutGroup dailyRewardsGroup;

       
        void Start ()
        {
            ParentAll.SetActive(true);
            ActiveDailyRewards();

            DailyRewards.instance.CheckRewards();

            DailyRewards.instance.onClaimPrize += OnClaimPrize;
            DailyRewards.instance.onPrizeAlreadyClaimed += OnPrizeAlreadyClaimed;

            UpdateUI();
        }
        void OnDestroy()
        {
            DailyRewards.instance.onClaimPrize -= OnClaimPrize;
            DailyRewards.instance.onPrizeAlreadyClaimed -= OnPrizeAlreadyClaimed;
        }

        // Clicked the claim button
        public void OnClaimClick()
        {
            DailyRewards.instance.ClaimPrize(DailyRewards.instance.availableReward);
            UpdateUI();
        }

        public void UpdateUI()
        {
            foreach (Transform child in dailyRewardsGroup.transform)
            {
                Destroy(child.gameObject);
            }

            bool isRewardAvailableNow = false;

            for (int i = 0; i < DailyRewards.instance.rewards.Count; i++)
            {
                int reward = DailyRewards.instance.rewards[i].reward;
                string Namereward = DailyRewards.instance.rewards[i].nameReward;
                Sprite imgReward = DailyRewards.instance.rewards[i].Sprite;
                int day = i + 1;

                GameObject dailyRewardGo = GameObject.Instantiate(dailyRewardPrefab) as GameObject;

                DailyRewardUI dailyReward = dailyRewardGo.GetComponent<DailyRewardUI>();
                dailyReward.transform.SetParent(dailyRewardsGroup.transform);
                dailyRewardGo.transform.localScale = Vector2.one;

                dailyReward.day = day;
                dailyReward.imgReward.sprite = imgReward;

                if (reward == 0)
                {
                    dailyReward.reward = Namereward.ToString();
                }
                else
                {
                    dailyReward.reward = reward.ToString();
                }
                

                dailyReward.isAvailable = day == DailyRewards.instance.availableReward;
                dailyReward.isClaimed = day <= DailyRewards.instance.lastReward;

                if (dailyReward.isAvailable)
                {
                    isRewardAvailableNow = true;
                }

                dailyReward.Refresh();
            }

            btnClaim.gameObject.SetActive(isRewardAvailableNow);
            btnClose.gameObject.SetActive(!isRewardAvailableNow);
            txtTimeDue.gameObject.SetActive(!isRewardAvailableNow);
        }

        void Update()
        {
            if (txtTimeDue.IsActive())
            {
                TimeSpan difference = (DailyRewards.instance.lastRewardTime - DailyRewards.instance.timer).Add(new TimeSpan(0, 24, 0, 0));

                // Is the counter below 0? There is a new reward then
                if (difference.TotalSeconds <= 0)
                {
                    DailyRewards.instance.CheckRewards();
                    UpdateUI();

                    return;
                }

                string formattedTs = string.Format("{0:D2}:{1:D2}:{2:D2}", difference.Hours, difference.Minutes, difference.Seconds);

                //txtTimeDue.text = "Return in " + formattedTs + " to claim your reward";
                txtTimeDue.text = "";
                txtTimePersent.text =  formattedTs;
            }
        }

        // Delegate
        private void OnClaimPrize(int day)
        {
            panelReward.SetActive(true);
            if (DailyRewards.instance.rewards[day - 1].reward == 0)
            {
                //congratulations
                //hien vu khi
                txtReward.text = DailyRewards.instance.rewards[day - 1].nameReward;
                imgReward.sprite = DailyRewards.instance.rewards[day - 1].Sprite;
                imgReward.type = Image.Type.Simple;
                imgReward.SetNativeSize();
                imgReward.type = Image.Type.Sliced;
            }
            else
            {
                //hien coins
                txtReward.text = "+ " + DailyRewards.instance.rewards[day - 1].reward;
                imgReward.sprite = DailyRewards.instance.rewards[day - 1].Sprite;
                imgReward.type = Image.Type.Simple;
                imgReward.SetNativeSize();
                imgReward.type = Image.Type.Sliced;
            }
        }

        // Delegate
        private void OnPrizeAlreadyClaimed(int day)
        {
            // Do Something with the prize already claimed
        }

        // Close the Rewards panel
        public void OnCloseRewardsClick()
        {
            panelReward.SetActive(false);
        }

        // Resets player preferences
        public void OnResetClick()
        {
            DailyRewards.instance.Reset();
            DailyRewards.instance.lastRewardTime = System.DateTime.MinValue;
            DailyRewards.instance.CheckRewards();

            UpdateUI();
        }

        // Simulates the next day
        public void OnAdvanceDayClick()
        {
            DailyRewards.instance.timer = DailyRewards.instance.timer.AddDays(1);
            DailyRewards.instance.CheckRewards();
            UpdateUI();
        }

        // Simulates the next hour
        public void OnAdvanceHourClick()
        {
            DailyRewards.instance.timer = DailyRewards.instance.timer.AddHours(1);
            DailyRewards.instance.CheckRewards();
            UpdateUI();
        }

        public void HideDailyRewards()
        {
            ParentAll.SetActive(false);
            animationHome.SetActive(true);
        }

        public void ActiveDailyRewards()
        {
            ParentAll.SetActive(true);
        }

        private void OnEnable()
        {
            animationHome.SetActive(false);
        }
    }
}