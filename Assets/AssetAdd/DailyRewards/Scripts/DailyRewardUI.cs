/***************************************************************************\
Project:      Daily Rewards
Copyright (c) Niobium Studios.
Author:       Guilherme Nunes Barbosa (gnunesb@gmail.com)
\***************************************************************************/
using UnityEngine;
using UnityEngine.UI;

/* 
 * Daily Reward Object UI representation
 */
namespace com.niobiumstudios.dailyrewards
{
    public class DailyRewardUI : MonoBehaviour
    {
        public Text txtDay;
        public Text txtReward;
        public Image imgReward;
        public Image imgBackground;

        public int day;
        public string reward;
        public bool isClaimed;
        public bool isAvailable;

        public Image border;
        public Color availableColor;
        public Color claimedColor;

        public void Refresh()
        {
            border.gameObject.SetActive(false);
            txtDay.text = "DAY " + day.ToString();
            txtReward.text = reward.ToString();
            imgReward.SetNativeSize();

            if (isAvailable)
            {
                imgBackground.color = availableColor;
                border.gameObject.SetActive(true);
            }

            if (isClaimed)
            {
                imgBackground.color = claimedColor;
                border.gameObject.SetActive(false);
                txtReward.text = "CLAIMED";
            }
        }
    }
}