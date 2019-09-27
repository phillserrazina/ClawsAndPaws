using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Actor))]
public class Stats : MonoBehaviour {

	// VARIABLES

	public GameObject conditionTab;

	private float maxHealthPoints;
	public float currentHealthPoints { get; private set; }
	public float healthDecimalPercentage { get { return currentHealthPoints / maxHealthPoints; } }

	private float maxStaminaPoints;
	public float currentStaminaPoints { get; private set; }
	public float staminaDecimalPercentage { get { return currentStaminaPoints / maxStaminaPoints; } }

	private float sp = 0;
	public float speedPoints { get { return GetComponent<Attributes>().agilityPoints + sp; } set { sp = value; } }
	private float ap = 0;
	public float attackPoints { get { return GetComponent<Attributes>().strengthPoints + ap; } set { ap = value; } }
	private float dp = 0;
	public float defensePoints { get { return 1 + dp; } set { dp = value; dp = Mathf.Clamp(dp, 0, 9); } }
	private float ip = 0;
	public float intimidationPoints { get { return GetComponent<Attributes>().intimidationPoints + ip; } set { ip = value; } }

	private Stack<ConditionSO> currentConditions = new Stack<ConditionSO>();
	private Stack<ConsumableSO> currentEffects = new Stack<ConsumableSO>();

	private Actor actor;

	// METHODS

	public void Initialize() {
		actor = GetComponent<Actor>();

		maxHealthPoints = 90 + (actor.characterData.healthPoints * 10);
		maxStaminaPoints = 90 + (actor.characterData.staminaPoints * 10);

		currentHealthPoints = maxHealthPoints;
		currentStaminaPoints = maxStaminaPoints;
	}

	public void TakeDamage(float damage, bool trueDamage=false) {
		if (currentHealthPoints <= 0) return;

		if (actor.combat.isDefending && !trueDamage) {
			damage /= 2;
			actor.combat.isDefending = false;
		}

		damage /= Mathf.Log10(defensePoints*10);
		currentHealthPoints -= damage;
		if (currentHealthPoints <= 0) currentHealthPoints = 0;
	}

	#region Condition Tab

	private void ClearConditionTab() {
		for (int i = 0; i < conditionTab.transform.childCount; i++) {
			Transform child = conditionTab.transform.GetChild(i);
			Destroy(child.gameObject);
		}
	}

	private void AddToConditionTab(ConditionSO condition) {
		GameObject conditionObject = new GameObject();
		Image img = conditionObject.AddComponent<Image>();
		img.sprite = condition.icon;
		img.preserveAspect = true;
		
		GameObject go = Instantiate(conditionObject) as GameObject;
		go.transform.SetParent(conditionTab.transform);
		go.transform.localScale = Vector3.one;
	}

	public void AddToConditionTab(ConsumableSO.Effect effect) {
		GameObject effectObject = new GameObject();
		Image img = effectObject.AddComponent<Image>();
		img.sprite = effect.icon;
		img.preserveAspect = true;
		
		GameObject go = Instantiate(effectObject) as GameObject;
		go.transform.SetParent(conditionTab.transform);
		go.transform.localScale = Vector3.one;
	}

	#endregion

	#region Conditions

	public void AddCondition(ConditionSO condition) {
		condition = ScriptableObject.Instantiate(condition);
		currentConditions.Push(condition);
		AddToConditionTab(condition);
	}

	public void ApplyConditions() {
		ApplyEffects();
		if (currentConditions.Count <= 0) return;

		var newStack = new Stack<ConditionSO>(currentConditions);
		currentConditions.Clear();

		while (newStack.Count > 0) {
			ConditionSO c = newStack.Pop();
			ExecuteConditions(c);
			AddToConditionTab(c);
		}
	}

	private void ExecuteConditions(ConditionSO cond) {
		switch (cond.condition)
		{
			case ConditionSO.Conditions.Poison:
				TakeDamage(cond.strength, true);
				break;
			
			case ConditionSO.Conditions.Sleep:
				
				break;
			
			case ConditionSO.Conditions.Increase_Defense:
				defensePoints += cond.strength;
				break;
			
			case ConditionSO.Conditions.Reduce_Defense:
				defensePoints -= cond.strength;
				break;
			
			case ConditionSO.Conditions.Increase_Speed:
				speedPoints += cond.strength;
				break;
			
			case ConditionSO.Conditions.Reduce_Speed:
				speedPoints -= cond.strength;
				break;
			
			default:
				break;
		}

		cond.duration--;
		if (cond.duration > 0) currentConditions.Push(cond);
	}

	#endregion

	public void AddEffect(ConsumableSO item) {
		item = Instantiate(item);
		foreach (var effect in item.effects) {
			if (effect.infinite == false)
				AddToConditionTab(effect);
		}
		
		ExecuteEffects(item);
	}

	public void ApplyEffects() {
		ClearConditionTab();
		if (currentEffects.Count <= 0) return;

		var newStack = new Stack<ConsumableSO>(currentEffects);
		currentEffects.Clear();

		while (newStack.Count > 0) {
			ConsumableSO c = newStack.Pop();
			ExecuteEffects(c);
			foreach (var effect in c.effects) {
				if (effect.infinite == false)
					AddToConditionTab(effect);
			}
		}
	}

	private void ExecuteEffects(ConsumableSO item) {
		item.Use(actor);

		for (int i = 0; i < item.effects.Length; i++) {
			item.effects[i].duration--;
			if (item.effects[i].duration > -1) currentEffects.Push(item);
		}
	}

	#region Stamina and Health

	public void DepleteStamina(float value) {
		if (currentStaminaPoints < value) return;

		currentStaminaPoints -= value;
		if (currentStaminaPoints <= 0) currentStaminaPoints = 0;
	}

	public void RestoreHealth(float value) {
		if (currentHealthPoints >= maxHealthPoints) return;
		currentHealthPoints += value;
		currentHealthPoints = Mathf.Clamp(currentHealthPoints, 0, maxHealthPoints);
	}

	public void RestoreStamina(float value) {
		if (currentStaminaPoints >= maxStaminaPoints) return;
		currentStaminaPoints += value;
		currentStaminaPoints = Mathf.Clamp(currentStaminaPoints, 0, maxStaminaPoints);
	}

	#endregion
}
