using UnityEngine;

namespace Game.Managers
{
    public class CurrencyManager : Singleton<CurrencyManager>
    {
        public delegate void UpdateCurrency(int currency);
        public static event UpdateCurrency OnUpdateCurrency;

        [SerializeField] private int _currencyAmount;

        public int CurrencyAmount
        {
            get
            {
                return _currencyAmount;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            Load();
        }

        public void IncreaseCurrency(int value) 
        {
            _currencyAmount += value;
            OnUpdateCurrency?.Invoke(_currencyAmount);
            Save();
        }

        public bool DecreaseCurrency(int value) 
        {
            if (value > _currencyAmount)
                return false;

            _currencyAmount -= value;
            OnUpdateCurrency?.Invoke(_currencyAmount);
            Save();
            return true;
        }

        private void Save()
        {
            PlayerPrefs.SetInt("Currency", _currencyAmount);
            PlayerPrefs.Save();
        }

        private void Load()
        {
            if (PlayerPrefs.HasKey("Currency"))
                _currencyAmount = PlayerPrefs.GetInt("Currency");
            else
                _currencyAmount = 0;
            
            OnUpdateCurrency?.Invoke(_currencyAmount);
        }
    }
}
