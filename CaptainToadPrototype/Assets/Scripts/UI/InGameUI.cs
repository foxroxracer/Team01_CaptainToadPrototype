using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour {
    public int CurrentLives;
    public int CurrentCoins;
    public int CollectedGems;

    public Sprite PowerGemSprite;

    public Sprite[] CoinSprites = new Sprite[10];

    private Image[] _coinImages = new Image[3];
    private Image[] _livesImages = new Image[3];
    private Image[] _powerGemImages = new Image[3];

    private void Start()
    {
        //Initialize Arrays For Coins and Lives
        _coinImages[0] = GameObject.Find("CurrentCoins_01").GetComponent<Image>();
        _coinImages[1] = GameObject.Find("CurrentCoins_02").GetComponent<Image>();
        _coinImages[2] = GameObject.Find("CurrentCoins_03").GetComponent<Image>();

        _livesImages[0] = GameObject.Find("LivesCount_01").GetComponent<Image>();
        _livesImages[1] = GameObject.Find("LivesCount_02").GetComponent<Image>();
        _livesImages[2] = GameObject.Find("LivesCount_03").GetComponent<Image>();

        _powerGemImages[0] = GameObject.Find("PowerGem_01").GetComponent<Image>();
        _powerGemImages[1] = GameObject.Find("PowerGem_02").GetComponent<Image>();
        _powerGemImages[2] = GameObject.Find("PowerGem_03").GetComponent<Image>();
    }

    private void Update()
    {
        UpdateUi(CurrentCoins,_coinImages);
        UpdateUi(CurrentLives, _livesImages);


    }

    private void UpdateUi(int current,Image[] images)
    {

        //Update Coins/Lives with current amount
        int NumberAmount = current.ToString().Length;

        for (int i = 0; i < images.Length; i++) 
        {
            //Enable & Deactivate unnecessary images
            if(i>=NumberAmount && i < images.Length)
            {
                images[i].enabled = false;
            }
            else
            {
                images[i].enabled = true;
            }

        }
        for (int i = 0; i < NumberAmount; i++) //Numberamount will be 1,2,3
        {
            images[i].sprite = CoinSprites[(int.Parse)(current.ToString().Substring(i, 1))];

        }
    }

    public void UpdatePowerGems()
    {
        CollectedGems++;
        for (int i = 0; i < CollectedGems; i++)
        {
            _powerGemImages[i].sprite = PowerGemSprite;
        }
    }
}
