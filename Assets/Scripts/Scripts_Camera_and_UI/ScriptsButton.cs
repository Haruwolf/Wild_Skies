using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScriptsButton : MonoBehaviour
{
    public void RestartGame() //Para reiniciar o jogo
    {
        //print("testerein");

        //D� unload em todas as variaveis est�ticas da cena e carrega a cena novamente.
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
