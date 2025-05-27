using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SYSTEM {
    [Serializable] 
    public class LifeHUDSystem {
        [SerializeField] private List<Image> lifeList;

        // O m�todo ChangeLifeHUD atualiza a exibi��o dos �cones de vida no HUD. 
        // Se o n�mero de vidas atual for igual ao tamanho da lista, ele define todos os �cones como vis�veis (alpha = 1).
        // Caso contr�rio, ele define o alpha do �cone correspondente � vida perdida como 0.2, indicando que foi perdida.
        public void ChangeLifeHUD(int currentLife) {
            if (currentLife == lifeList.Count) {
                foreach (var life in lifeList)
                    life.GetComponent<CanvasGroup>().alpha = 1f;
                return;
            }

            if (currentLife >= 0 && currentLife < lifeList.Count)
                lifeList[currentLife].GetComponent<CanvasGroup>().alpha = 0.2f;
        }

        // O m�todo GetLifeSize retorna a quantidade total de vidas dispon�veis no HUD.
        public int GetLifeSize() => lifeList.Count;
    }
}