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
    public float battleEndXP { private get; set; }
    public float battleEndGold { private get; set; }

    private void OnEnable() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>();

        int finalValue = GetNextLevelXP();
        xpText.text = string.Format("XP: {0}/{1}", battleEndXP, finalValue);
        xpBarGraphic.fillAmount = (float)battleEndXP / (float)finalValue;

        goldText.text = battleEndGold.ToString();
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
            battleEndGold = Mathf.Lerp(battleEndGold, Inventory.instance.gold, Time.deltaTime * lerpSpeed);
            goldText.text = battleEndGold.ToString("F0");
        }

        if (battleEndXP != player.characterData.experiencePoints) {
            int finalValue = GetNextLevelXP();
            battleEndXP = Mathf.Lerp(battleEndXP, player.characterData.experiencePoints, Time.deltaTime * lerpSpeed);
            xpText.text = string.Format("XP: {0}/{1}", battleEndXP.ToString("F0"), finalValue);
            xpBarGraphic.fillAmount = battleEndXP / (float)finalValue;
        }
    }
}
