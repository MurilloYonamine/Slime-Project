using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SYSTEM {
    [Serializable] 
    public class LifeHUDSystem {
        [SerializeField] private List<Image> lifeList;

        // O método ChangeLifeHUD atualiza a exibição dos ícones de vida no HUD. 
        // Se o número de vidas atual for igual ao tamanho da lista, ele define todos os ícones como visíveis (alpha = 1).
        // Caso contrário, ele define o alpha do ícone correspondente à vida perdida como 0.2, indicando que foi perdida.
        public void ChangeLifeHUD(int currentLife) {
            if (currentLife == lifeList.Count) {
                foreach (var life in lifeList)
                    life.GetComponent<CanvasGroup>().alpha = 1f;
                return;
            }

            if (currentLife >= 0 && currentLife < lifeList.Count)
                lifeList[currentLife].GetComponent<CanvasGroup>().alpha = 0.2f;
        }

        // O método GetLifeSize retorna a quantidade total de vidas disponíveis no HUD.
        public int GetLifeSize() => lifeList.Count;
    }
}