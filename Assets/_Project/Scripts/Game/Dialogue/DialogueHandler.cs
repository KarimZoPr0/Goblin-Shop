using GSS.Combat;
using TMPro;
using UnityEngine;

namespace GSS.Dialogue
{
    public class DialogueHandler : MonoBehaviour
    {
        public TextMeshProUGUI test;

        private void Update()
        {
            CharacterGenerator.Instance.GetSelected().GetDialog();
        }
    }
}