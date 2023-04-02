using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Asyncoroutine;
public class PlayableDirectorScene : MonoBehaviour
{
    public static PlayableDirectorScene Instance;
    public PlayableDirector GoToMenu;
    public PlayableDirector GoToGame;
    public PlayableDirector ShowGame;
    public PlayableDirector NotShowGame;
    private void Awake() {
        Instance = this;
    }
    private async void Start() {
        await new WaitForSeconds(1.1f);
        GoToMenuScene();
    }
    public void GoToGameScene () 
    {
        GoToGame.Play();
        ShowGame.Play();
    }
    public void GoToMenuSceneFromGame () 
    {
        GoToMenu.Play();
        NotShowGame.Play();
    }
    public void GoToMenuScene () => GoToMenu.Play();
    public void ShowGameScene () => ShowGame.Play();
   
}
