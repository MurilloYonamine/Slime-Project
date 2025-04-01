using UnityEngine;

namespace MAGIC_PLANTS.FIG_TREE {
    public class FigTree : MonoBehaviour {
        [SerializeField] private GameObject magicSeed;
        [SerializeField] private GameObject magicParent;
        [SerializeField] private GameObject seedSpawn;

        private bool seedHasDropped = false;
        public void DropMagicSeed() {
            if (seedHasDropped) return;

            GameObject seed = Instantiate(magicSeed, seedSpawn.transform.position, Quaternion.identity, magicParent.transform);

            if (!seed.GetComponent<MagicSeed>()) seed.AddComponent<MagicSeed>();

            seedHasDropped = true;
        }
    }
}