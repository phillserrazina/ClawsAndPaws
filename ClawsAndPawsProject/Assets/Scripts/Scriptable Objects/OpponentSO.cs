using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Actors/Custom Opponent", fileName="New Opponent")]
public class OpponentSO : CharacterSO {

	public int goldReward;
	public int xpReward { get; private set; }
	public List<ItemSO> itemRewards = new List<ItemSO>();

	public void CreateRandom(int playerExperiencePoints) {
		TextAsset file = Resources.Load("RandomCatNames") as TextAsset;
		string[] nameList = file.text.Split(',');

		actorName = nameList[Random.Range(0, nameList.Length)];
		experiencePoints = Random.Range(playerExperiencePoints-2000, playerExperiencePoints+2000);
		experiencePoints = Mathf.Clamp(experiencePoints, 0, 20000);

		strengthPoints = Random.Range(0, 10);
		agilityPoints = Random.Range(0, 10);
		healthPoints = Random.Range(0, 10);
		staminaPoints = Random.Range(0, 10);
		intimidationPoints = Random.Range(0, 10);

		goldReward = (int)(experiencePoints * Random.Range(0.1f, 0.3f));
		if (goldReward < 50) goldReward = (int)(50 * Random.Range(0.5f, 1f));

		xpReward = (int)(experiencePoints * Random.Range(0.2f, 0.4f));
		if (xpReward < 100) goldReward = (int)(100 * Random.Range(0.3f, 1f));

		ItemListSO rewardList = Resources.Load("Reward Items") as ItemListSO;

		int itemLevel = Mathf.FloorToInt((experiencePoints + 1000) / 1000);
		itemLevel -= Random.Range(-2, 2);
		itemLevel = Mathf.Clamp(itemLevel, 1, 20);

		itemRewards.Add(rewardList.items[itemLevel]);
	}
}
