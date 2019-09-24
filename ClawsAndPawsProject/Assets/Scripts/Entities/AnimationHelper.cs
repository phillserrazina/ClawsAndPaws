using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationHelper : MonoBehaviour
{
    private Actor actor;
    private Combat combat;
    private Animator animator;

    private void Start() {
        actor = GetComponentInParent<Actor>();
        combat = GetComponentInParent<Combat>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (SceneManager.GetActiveScene().name != "Fight Scene"  && SceneManager.GetActiveScene().name != "Tournament Scene")
            return;
        
        animator.SetBool("Defending", combat.isDefending);
    }

    private void OpponentTakeDamage() {
		ExecuteAttack();
        FindObjectOfType<UIManager>().UpdateUI();

        string animToPlay;
        animToPlay = actor.opponent.stats.currentHealthPoints <= 0 ? "Dead" : "Take Damage";
		actor.opponent.GetComponentInChildren<Animator>().Play(animToPlay);

        StartCoroutine(ScreenShake(0.05f, 0.1f));
	}

    private void ExecuteAttack() {
		actor.stats.DepleteStamina(combat.currentAttack.staminaCost);
		float damage = combat.currentAttack.damagePoints + actor.stats.attackPoints;
		actor.opponent.stats.TakeDamage(damage);

		if (combat.currentAttack.conditions != null) {
			foreach (ConditionSO condition in combat.currentAttack.conditions) {
				if (condition.targetSelf) actor.stats.AddCondition(condition);
				else actor.opponent.stats.AddCondition(condition);
			}
		}
	}

    private void Dead() {
        FindObjectOfType<TurnManager>().NextState();
    }

    private IEnumerator ScreenShake(float duration, float magnitude)
	{
        GameObject go = Camera.main.gameObject;
		Vector3 originalPosition = go.transform.localPosition;
		float elapsed = 0.0f;

		while (elapsed < duration)
		{
			float x = Random.Range (-1f, 1f) * magnitude;
			float y = Random.Range (-1f, 1f) * magnitude;

			elapsed += Time.deltaTime;

			go.transform.localPosition = new Vector3 (x, y, originalPosition.z);

			yield return null;
		}

		go.transform.localPosition = originalPosition;
	}

    private void PlaySound(string soundName) {
        FindObjectOfType<AudioManager>().Play(soundName);
    }
}
