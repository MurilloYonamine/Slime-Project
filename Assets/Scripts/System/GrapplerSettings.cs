using System;
using System.Collections.Generic;
using UnityEngine;

namespace SYSTEM {
    [Serializable]
    public class GrapplerSettings {
        [SerializeField] private List<GameObject> grapplerList;

        /* Altera o raio do componente CircleCollider2D de cada objeto na lista 'grapplerList' para o valor especificado em 'distance'.
           Isso permite ajustar dinamicamente a distância de alcance dos grapplers no jogo.*/
        public void ChangeDistance(float distance) {
            foreach (GameObject grappler in grapplerList) {
                var collider = grappler.GetComponentInChildren<CircleCollider2D>();
                if (collider != null) {
                    collider.radius = distance;
                }
            }
        }
    }
}
