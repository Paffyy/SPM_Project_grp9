using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    [SerializeField] private GameObject fullHeart;
    [SerializeField] private GameObject halfHeart;
    [SerializeField] private GameObject EmptyHeart;
    private HeartState currentState;

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
                break;
            case HeartState.Half:
                halfHeart.SetActive(true);
                fullHeart.SetActive(false);
                EmptyHeart.SetActive(false);
                currentState = HeartState.Half;
                break;
            case HeartState.Empty:
                EmptyHeart.SetActive(true);
                fullHeart.SetActive(false);
                halfHeart.SetActive(false);
                currentState = HeartState.Empty;
                break;
        }
    }

    public HeartState GetState()
    {
        return currentState;
    }
}
