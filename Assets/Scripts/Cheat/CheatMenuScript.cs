using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CHEAT {
    public class CheatMenuScript : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI flytext;
        [SerializeField] private TextMeshProUGUI invtext;

        private bool inv = false;
        private bool fly = false;

        public void invname() {
            inv = !inv;
            if (inv) {
                invtext.text = "massa infinita on";
            }
            else {
                invtext.text = "massa infinita off";
            }
        }
        public void flyname() {
            fly = !fly;
            if (fly) {
                flytext.text = "voar on";
            }
            else {
                flytext.text = "voar off";
            }
        }
        


    }
}