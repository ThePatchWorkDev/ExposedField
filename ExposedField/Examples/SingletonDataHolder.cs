using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PWH.ExposedField.Examples
{
    public class SingletonDataHolder : MonoBehaviour
    {
        public static SingletonDataHolder instance;

        private void Awake()
        {
            if (instance) { Destroy(this); } else { instance = this; }
        }

        public PlayerData playerData = new PlayerData(3, 0);
        public MarketCostsData marketCostsData = new MarketCostsData(100, 1);
    }

    [System.Serializable]
    public struct PlayerData
    {
        [ExposedField("Player Lives")]
        public int lives;
        [ExposedField("Player Cash")]
        public int money;

        public PlayerData(int lives, int money)
        {
            this.lives = lives;
            this.money = money;
        }
    }

    [System.Serializable]
    public struct MarketCostsData
    {
        [ExposedField("Cost of Bitcoin")]
        public int bitcoinCost;
        [ExposedField("Cost of Dogecoin")]
        public int dogecoinCost;

        public MarketCostsData(int bitcoinCost, int dogecoinCost)
        {
            this.bitcoinCost = bitcoinCost;
            this.dogecoinCost = dogecoinCost;
        }
    }
}