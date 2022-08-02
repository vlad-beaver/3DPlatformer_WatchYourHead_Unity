using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Infrastructure.Abstractables;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScene : PuzzleComponent
{
    void OnTriggerEnter(Collider other)
    {
        DOTween.KillAll();
        if (PuzzlePlayer.Instance.HeadDead)
        {
            SceneManager.LoadScene("SecondEnding");
        }
        else
        {
            SceneManager.LoadScene("Ending");
        }
    }
    void OnTriggerStay(Collider other)
    {

    }
    void OnTriggerExit(Collider other)
    {
        
    }
}
