using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers
{
    public class CurrencyManager : Singleton<CurrencyManager>
    {
        private int _currencyAmount;

        protected override void Awake()
        {
            base.Awake();

            Load();
        }

        public void IncreaseCurrency(int value) 
        {
            _currencyAmount += value;
            Save();
        }

        public bool DecreaseCurrency(int value) 
        {
            if (value > _currencyAmount)
                return false;

            _currencyAmount -= value;
            Save();
            return true;
        }

        private void Save()
        {
            // TODO: add a saving system for the currency amount
        }

        private void Load()
        {
            // TODO: add a loading system for the currency amount
        }
    }
}
