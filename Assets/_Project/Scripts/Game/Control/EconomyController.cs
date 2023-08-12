using System;
using System.Linq;
using GSS.Combat;
using TMPro;
using UnityEngine;

namespace GSS.Control
{
    public class EconomyController : MonoBehaviour
    {
        [SerializeField] private ItemController itemController;
        [SerializeField] private TMP_Text myGold_label;
        [SerializeField] private TMP_Text kingsdGold_label;
        [SerializeField] private float kingsGold;
        [SerializeField] private AudioSource source;

        private float myGold;

        private void Start()
        {
            kingsdGold_label.text = kingsGold.ToString();
        }

        public void Checkout()
        {
            // storing CharacterGenerator into a variable
            var characterGenerator = CharacterGenerator.Instance;

            if (itemController.counterItems.Count <= 0) return;
            if (itemController.gold > characterGenerator.GetSelected().baseGold) return;

            source.Play();

            myGold += itemController.gold;
            myGold_label.text = myGold.ToString();

            foreach (var counterItem in itemController.counterItems)
            {
                var count = itemController.counterItems.Count(item => item.id == counterItem.id);
                if (count > counterItem.buyLimit) return;
            }

            itemController.goldTxt.text = itemController.gold.ToString();

            if (myGold > kingsGold)
            {
                ReferenceUI.transitor.LoadScene(0);
                print("You Defeated the king");
                return;
            }

            // Calling next character
            characterGenerator.StartCoroutine(characterGenerator.NextCharacter());
        }
    }
}