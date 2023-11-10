using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : View
{

    //Y'a rien a voir ! Degage !
    public Sprite _coinsWhite;
    public Sprite _coinsBlack;
    public Image _coins;

    public void ActiveCoin()
    {
        _coins.sprite = _coinsWhite;
    }

    public void DesactiveCoin()
    {
        _coins.sprite = _coinsBlack;
    }

}
