using System;
using System.Collections;
using DG.Tweening;
using GSS.Combat;
using UnityEngine;

namespace GSS.Movement
{
    public class Move : MonoBehaviour
    {
        [Header("Combat Settings")] [SerializeField]
        private CombatManager combatManager;

        [Header("Movement/Motion Settings")] [SerializeField]
        private Vector3[] scales;

        [SerializeField] private float[] duration;

        public static event Action<Fighter, Fighter> AttackEvent;

        public IEnumerator InitiateAttackAnimation(Fighter attacker, Fighter target)
        {
            var seq = DOTween.Sequence();

            seq
                .Append(transform.DOScale(scales[0], .5f))
                .AppendInterval(.2f)
                .Append(transform.DOMove(target.transform.position, duration[0])
                    .OnStepComplete(() => AttackEvent?.Invoke(attacker, target)))
                .Append(transform.DOMove(transform.position, duration[1]))
                .Append(transform.DOScale(scales[1], .5f));

            yield return seq.WaitForCompletion();
        }
    }
}