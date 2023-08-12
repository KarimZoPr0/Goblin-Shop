using System.Collections;
using GSS.Combat;
using UnityEngine;
using UnityEngine.Events;

namespace GSS.Core
{
    public class DayNightManager : MonoBehaviour
    {
        [Header("Combat References")] [SerializeField]
        private CombatManager combatManager;

        [SerializeField] private GameObject combatBoard;
        [SerializeField] private GameObject defaultCharacter;

        [Header("Day/Night References")] [SerializeField]
        private AudioSource source;

        [SerializeField] private AudioClip dayClip;
        [SerializeField] private AudioClip nightClip;

        [Header("Events")] public UnityEvent dayShoppingEvent;
        public UnityEvent nightShoppingEvent;
        public UnityEvent combatEvent;

        private GameState gameState = GameState.DayShopping;
        private int middleWareMultiplier = -1;

        private void Start()
        {
            HandleGameState();
            CharacterGenerator.Instance.CombatEvent += Co_CombatTransition;
            combatManager.CombatCompleteEvent += HandleCombatComplete;
        }

        private void OnDestroy()
        {
            CharacterGenerator.Instance.CombatEvent -= Co_CombatTransition;
            combatManager.CombatCompleteEvent -= HandleCombatComplete;
        }

        private IEnumerator Co_CombatTransition()
        {
            if (ReferenceUI.transitor != null)
            {
                ReferenceUI.transitor.Fade();
            }

            defaultCharacter.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            combatBoard.SetActive(true);
            source.Stop();
            yield return new WaitForSeconds(1f);
            gameState = GameState.Combat;
            HandleGameState();
        }


        private IEnumerator Co_ShoppingTransition()
        {
            if (ReferenceUI.transitor != null)
            {
                ReferenceUI.transitor.Fade();
            }

            yield return new WaitForSeconds(0.5f);
            CharacterGenerator.Instance.SpawnCharacters();
            combatBoard.SetActive(false);
            defaultCharacter.SetActive(true);
            if (gameState == GameState.DayShopping)
            {
                dayShoppingEvent?.Invoke();
            }
            else
            {
                nightShoppingEvent?.Invoke();
            }
        }

        private void HandleGameState()
        {
            switch (gameState)
            {
                case GameState.Combat:
                    StartCoroutine(combatManager.Co_ExecuteCombatSequence());
                    combatEvent?.Invoke();
                    break;
                case GameState.DayShopping:
                    StartCoroutine(Co_ShoppingTransition());
                    combatManager.SetTarget(combatManager.heroes, combatManager.monsters);
                    source.clip = dayClip;
                    source.Play();
                    break;
                case GameState.NightShopping:
                    StartCoroutine(Co_ShoppingTransition());
                    combatManager.SetTarget(combatManager.monsters, combatManager.heroes);
                    source.clip = nightClip;
                    source.Play();
                    break;
                default:
                    Debug.Log("GameState was not found");
                    break;
            }
        }

        private void HandleCombatComplete()
        {
            RewardAttackers();
            
            middleWareMultiplier *= -1;
            gameState = middleWareMultiplier == 1 ? GameState.NightShopping : GameState.DayShopping;
            HandleGameState();
        }

        private void RewardAttackers()
        {
            foreach (var attacker in combatManager.GetAttackers())
            {
                attacker.character.baseGold += 125;
            }
        }

        private enum GameState
        {
            DayShopping,
            Combat,
            NightShopping,
        }
    }
}