using UnityEngine;
using UnityEngine.SceneManagement;
public class Main_menu : MonoBehaviour
{
   public void Play()
   {
        SceneManager.LoadSceneAsync(1);
   }

   public void FUCKYOU(){
        Application.Quit();
   }
}
