using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    [SerializeField] private GameObject fullHeart;
    [SerializeField] private GameObject halfHeart;
    [SerializeField] private GameObject EmptyHeart;
    private HeartState prevState;
    private HeartState currentState;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public enum HeartState
        {
        Full,
        Half,
        Empty,
    }

    public void SetHeart(HeartState state)
    {
        switch (state)
        {
            case HeartState.Full:
                fullHeart.SetActive(true);
                halfHeart.SetActive(false);
                EmptyHeart.SetActive(false);
                currentState = HeartState.Full;
                PlayFillAnimation();
                prevState = HeartState.Full;
                break;
            case HeartState.Half:
                halfHeart.SetActive(true);
                fullHeart.SetActive(false);
                EmptyHeart.SetActive(false);
                currentState = HeartState.Half;
                PlayFillAnimation();
                prevState = HeartState.Half;
                break;
            case HeartState.Empty:
                EmptyHeart.SetActive(true);
                fullHeart.SetActive(false);
                halfHeart.SetActive(false);
                currentState = HeartState.Empty;
                prevState = HeartState.Empty;
                break;
        }
    }

    private void PlayFillAnimation()
    {
        if (prevState != HeartState.Full)
        {
            anim.SetTrigger("Fill");
        }
    }

    public HeartState GetState()
    {
        return currentState;
    }
}
