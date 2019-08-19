using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Actors/Custom Opponent", fileName="New Opponent")]
public class OpponentSO : CharacterSO {

	public int goldReward { get; private set; }
	public int xpReward { get; private set; }
	public List<ItemSO> itemRewards = new List<ItemSO>();

	public void Create() {
		goldReward = (int)(experiencePoints * Random.Range(0.1f, 0.3f));
		if (goldReward < 50) goldReward = (int)(50 * Random.Range(0.5f, 1f));

		xpReward = (int)(experiencePoints * Random.Range(0.2f, 0.4f));
		if (xpReward < 100) xpReward = (int)(100 * Random.Range(0.3f, 1f));
	}

	public void CreateRandom() {
		// === NAME ===
		TextAsset file = Resources.Load("RandomCatNames") as TextAsset;
		string[] nameList = file.text.Split(',');
		actorName = nameList[Random.Range(0, nameList.Length)];

		visualIndex = Random.Range(0, 4);

		// === EXPERIENCE POINTS AND LEVEL

		CharacterSO cCharacter = FindObjectOfType<CurrentCharacterManager>().currentCharacter;
		TournamentSO cTournament = FindObjectOfType<TournamentTracker>().currentTournament;

		int tLevel = cTournament == null ? cCharacter.level : cTournament.requiredLevel;
		experiencePoints = Random.Range(tLevel-(100*tLevel), tLevel+(100*tLevel));
		experiencePoints = Mathf.Clamp(experiencePoints, 0, 20000);
		
		level = Mathf.FloorToInt(0.1f * Mathf.Sqrt(experiencePoints)) + 1;

		// === ATTRIBUTES ===
		strengthPoints = 1;
		agilityPoints = 1;
		healthPoints = 1;
		staminaPoints = 1;
		intimidationPoints = 1;

		int availablePoints = 4 * level;

		while (availablePoints > 0) {
			PointDistributionHelper();
			availablePoints--;
		}

		// === REWARDS ===
		goldReward = (int)(experiencePoints * Random.Range(0.1f, 0.3f));
		if (goldReward < 50) goldReward = (int)(50 * Random.Range(0.5f, 1f));

		xpReward = (int)(experiencePoints * Random.Range(0.2f, 0.4f));
		if (xpReward < 100) xpReward = (int)(100 * Random.Range(0.3f, 1f));

		ItemListSO rewardList = Resources.Load("Reward Items") as ItemListSO;

		int itemLevel = level;
		itemLevel -= Random.Range(-2, 2);
		itemLevel = Mathf.Clamp(itemLevel, 1, 20);

		itemRewards.Add(rewardList.items[itemLevel]);
	}

	private void PointDistributionHelper() {
		float val = Random.Range(0, 1000);

		if (val < 200) strengthPoints++;
		else if (val < 400) agilityPoints++;
		else if (val < 600) healthPoints++;
		else if (val < 800) staminaPoints++;
		else if (val <= 1000) intimidationPoints++;
	}
}
