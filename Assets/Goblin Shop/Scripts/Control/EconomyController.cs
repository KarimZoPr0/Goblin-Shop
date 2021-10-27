using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GSS.Combat;
using GSS.Control;

namespace GSS.con
{
    public class EconomyController : MonoBehaviour
    {
        public ItemController itemController;
        public void Checkout()
        {
            // storing CharacterGenerator into a variable
            var characterGenerator = CharacterGenerator.instance;
            
            if (itemController.counterItems.Count <= 0) return;
            if(itemController.gold > characterGenerator.GetSelected().baseGold) return;

            // checking if it count is higher than item limit
            foreach (var counterItem in itemController.counterItems)
            {
                var count = 0;
                foreach (var item in itemController.counterItems)
                    if (item.id == counterItem.id) count++;
                if (count > counterItem.buyLimit) return;
                // counterItem.GetComponent<RectTransform>().position = counterItem.objectSettings.HomePos;
            }

            // Calling next character
            characterGenerator.StartCoroutine(characterGenerator.NextCharacter());
        }
    }   
}