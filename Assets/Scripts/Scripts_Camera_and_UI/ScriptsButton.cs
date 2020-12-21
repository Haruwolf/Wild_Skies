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

        //Dá unload em todas as variaveis estáticas da cena e carrega a cena novamente.
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
