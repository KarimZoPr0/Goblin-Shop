using System.Collections.Generic;
using DG.Tweening;
using GSS.Combat;
using GSS.Control;
using UnityEngine;

namespace GSS.Movement {
	public class Move : MonoBehaviour {
		[Header("Combat Settings")] [SerializeField]
		private CombatManager combatManager;

		[Header("Movement/Motion Settings")] [SerializeField]
		private Vector3[] scales;

		[SerializeField] private float[] duration;

		public void MoveToTarget(Fighter fighter, Fighter target, List<Fighter> attackers) {
			var seq = DOTween.Sequence();

			seq
				.Append(transform.DOScale(scales[0], .5f))
				.AppendInterval(.2f)
				.Append(transform.DOMove(target.transform.position, duration[0])
					.OnStepComplete(() => combatManager.Attack(fighter, target)))
				.Append(transform.DOMove(transform.position, duration[1]))
				.Append(transform.DOScale(scales[1], .5f))
				.OnComplete(() => combatManager.CheckGameState(attackers));
		}
	}
}