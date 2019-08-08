using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardsUI : MonoBehaviour
{
    [SerializeField] private Text goldText;

    [SerializeField] private Image xpBarGraphic;
    [SerializeField] private Text xpText;

    [SerializeField] private float lerpSpeed;

    private Actor player;
    public int battleEndXP { private get; set; }
    public int battleEndGold { private get; set; }

    private void OnEnable() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>();
    }

    private void Update() {
        UpdateBars();
    }

    private int GetNextLevelXP() {
        int answer;

        float formula = (player.characterData.level) / 0.1f;
        answer = Mathf.FloorToInt(Mathf.Pow(formula, 2));

        return answer;
    }

    private void UpdateBars() {
        if (battleEndGold != Inventory.instance.gold) {
            battleEndGold = (int)Mathf.Lerp(battleEndGold, Inventory.instance.gold, Time.deltaTime * lerpSpeed);
            goldText.text = battleEndGold.ToString();
        }

        if (battleEndXP != player.characterData.experiencePoints) {
            int finalValue = GetNextLevelXP();
            battleEndXP = (int)Mathf.Lerp(battleEndXP, player.characterData.experiencePoints, Time.deltaTime * lerpSpeed);
            xpText.text = string.Format("XP: {0}/{1}", battleEndXP, finalValue);
            xpBarGraphic.fillAmount = (float)battleEndXP / (float)finalValue;
        }
    }
}
