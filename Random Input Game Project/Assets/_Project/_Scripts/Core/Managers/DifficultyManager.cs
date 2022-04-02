namespace Game.Managers
{
    public enum Difficulty { Easy, Medium, Hard }

    public class DifficultyManager : Singleton<DifficultyManager>
    {
        public delegate void DifficultyChange();
        public static event DifficultyChange OnDifficultyChange;
        
        public Difficulty currentDifficulty;

        private int _succesess = 0;

        private readonly int _easyLimit = 5;
        private readonly int _mediumLimit = 15;

        public void PlayerMistake(int value)
        {
            if (_succesess <= 0)
                return;

            if (_succesess > _mediumLimit && _succesess - value < _mediumLimit)
            {
                ChangeDifficulty(Difficulty.Medium);
            }
            else if (_succesess > _easyLimit && _succesess - value < _easyLimit)
            {
                ChangeDifficulty(Difficulty.Easy);
            }

            _succesess -= value;
            if (_succesess < 0) 
                _succesess = 0;
        }

        public void PlayerSuccess(int value)
        {
            if (_succesess >= 20)
                return;

            if (_succesess < _easyLimit && _succesess + value >= _easyLimit)
            {
                ChangeDifficulty(Difficulty.Medium);
                
            }
            else if (_succesess < _mediumLimit && _succesess + value >= _mediumLimit)
            {
                ChangeDifficulty(Difficulty.Hard);
            }

            _succesess += value;
            if (_succesess > 20)
                _succesess = 20;
        }

        private void ChangeDifficulty(Difficulty difficulty)
        {
            print("Changing Difficulty to " + difficulty);
            currentDifficulty = difficulty;
            OnDifficultyChange?.Invoke();
        }
    }
}
