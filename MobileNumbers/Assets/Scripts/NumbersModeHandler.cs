using Assets.Scripts.Helpers;
using UnityEngine;
using UnityEngine.UI;
using static _GameManager;

namespace Assets.Scripts
{
    class NumbersModeHandler : MonoBehaviour
    {

        #region Difficulty Settings
        [Header("Difficulty Settings")]

        [Header("Easy Values")]
        public int EASY_NUMBERS_RANGE;
        public int EASY_NUMBERS_MAX;        

        [Header("Normal Values")]
        public int NORMAL_NUMBERS_RANGE;
        public int NORMAL_NUMBERS_MAX;        

        [Header("Hard Values")]
        public int HARD_NUMBERS_MAX;
        public int HARD_NUMBERS_RANGE;        

        #endregion

        #region Private fields

        private int buttonClickCounter = 0;
        private int userInputSum = 0;
        private int firstSubtractNumber = 0;
        private int secondSubtractNumber = 0;
        private float TimeLeft = 60f;
        private static int score = 0;
        private int pointToGive = 0;
        private float bonustTime = 0;

        [SerializeField]
        Animator resultAnimator;

        [SerializeField]
        Animator bonusTimeAnimator;

        #endregion        

        public void Start()
        {
            switch (_instance.CurrentDiffulty)
            {
                case GameDifficulty.Easy:
                    EASY_NUMBERS_RANGE = 10;
                    EASY_NUMBERS_MAX = 20;
                    pointToGive = 2;
                    bonustTime = 2;
                    break;
                case GameDifficulty.Normal:
                    NORMAL_NUMBERS_RANGE = 30;
                    NORMAL_NUMBERS_MAX = 50;
                    pointToGive = 5;
                    bonustTime = 3;
                    break;
                case GameDifficulty.Hard:
                    HARD_NUMBERS_RANGE = 100;
                    HARD_NUMBERS_MAX = 300;
                    pointToGive = 10;
                    bonustTime = 4;
                    break;
                default:
                    break;
            }
            TimeLeft = 60f;
            _SoundManager._instance.NumbersBackgroundMusic.Play();

            _instance.CurrentNumbersGameMode = (NumbersOperation)Random.Range(0, 2);
            FillAdditionModeNumbers();
            FillSubtractionModeNumbers();
            AssignButtonListeners();
        }

        #region Fill Random Numbers after each answer
        private void FillAdditionModeNumbers()
        {
            System.Random rnd = new System.Random();

            // Dicculty Setting
            if (_instance.CurrentDiffulty == GameDifficulty.Easy)
            {
                _GUIManager._instance.NumberToFind.text = Random.Range(1, EASY_NUMBERS_RANGE).ToString();                
            }

            else if (_instance.CurrentDiffulty == GameDifficulty.Normal)
            {
                _GUIManager._instance.NumberToFind.text = Random.Range(10, NORMAL_NUMBERS_RANGE).ToString();
            }

            else
            {
                _GUIManager._instance.NumberToFind.text = Random.Range(35, HARD_NUMBERS_RANGE).ToString();
            }



            // Get the two correct numbers
            int correctNumberOne = rnd.Next(int.Parse(_GUIManager._instance.NumberToFind.text));
            int correctNumberTwo = int.Parse(_GUIManager._instance.NumberToFind.text) - correctNumberOne;

            // Add random number to all buttons
            foreach (var button in _GUIManager._instance.NumericAnswers)
            {
                // Get children(text)
                Text buttonText = button.GetComponentInChildren<Text>();
                button.interactable = true;

                buttonText.text = Random.Range(1, int.Parse(_GUIManager._instance.NumberToFind.text)).ToString();
            }

            // Insert the two correct answers into the list
            Text buttonOneText = _GUIManager._instance.NumericAnswers[0].GetComponentInChildren<Text>();
            buttonOneText.text = correctNumberOne.ToString();
            Text buttonTwoText = _GUIManager._instance.NumericAnswers[1].GetComponentInChildren<Text>();
            buttonTwoText.text = correctNumberTwo.ToString();

            // Randomize the list
            ListHelpers.RandomizeList(_GUIManager._instance.NumericAnswers);
        }

        private void FillSubtractionModeNumbers()
        {
            System.Random rnd = new System.Random();

            int correctNumberOne;
            int correctNumberTwo;

            // Dicculty Setting
            if (_instance.CurrentDiffulty == GameDifficulty.Easy)
            {
                _GUIManager._instance.NumberToFind.text = Random.Range(1, EASY_NUMBERS_RANGE).ToString();

                // Get the two correct numbers
                correctNumberOne = Random.Range(int.Parse(_GUIManager._instance.NumberToFind.text), EASY_NUMBERS_MAX);
                //int correctNumberOne = rnd.Next(maxReference);
                correctNumberTwo = correctNumberOne - int.Parse(_GUIManager._instance.NumberToFind.text);
            }

            else if (_instance.CurrentDiffulty == GameDifficulty.Normal)
            {
                _GUIManager._instance.NumberToFind.text = Random.Range(1, NORMAL_NUMBERS_RANGE).ToString();

                // Get the two correct numbers
                correctNumberOne = Random.Range(int.Parse(_GUIManager._instance.NumberToFind.text), NORMAL_NUMBERS_MAX);
                //int correctNumberOne = rnd.Next(maxReference);
                correctNumberTwo = correctNumberOne - int.Parse(_GUIManager._instance.NumberToFind.text);
            }

            else
            {
                _GUIManager._instance.NumberToFind.text = Random.Range(1, HARD_NUMBERS_RANGE).ToString();

                // Get the two correct numbers
                correctNumberOne = Random.Range(int.Parse(_GUIManager._instance.NumberToFind.text), HARD_NUMBERS_MAX);
                //int correctNumberOne = rnd.Next(maxReference);
                correctNumberTwo = correctNumberOne - int.Parse(_GUIManager._instance.NumberToFind.text);
            }

            // Add random number to all buttons
            foreach (var button in _GUIManager._instance.NumericAnswers)
            {
                // Get children(text)
                Text buttonText = button.GetComponentInChildren<Text>();
                button.interactable = true;

                buttonText.text = Random.Range(1, int.Parse(_GUIManager._instance.NumberToFind.text)).ToString();
            }

            // Insert the two correct answers into the list
            Text buttonOneText = _GUIManager._instance.NumericAnswers[0].GetComponentInChildren<Text>();
            buttonOneText.text = correctNumberOne.ToString();
            Text buttonTwoText = _GUIManager._instance.NumericAnswers[1].GetComponentInChildren<Text>();
            buttonTwoText.text = correctNumberTwo.ToString();

            // Randomize the list
            ListHelpers.RandomizeList(_GUIManager._instance.NumericAnswers);
        }

        #endregion

        #region Listeners Logic for correct answers

        private void AdditionButtonListener(string message, Button btn)
        {
            btn.interactable = false;
            _SoundManager._instance.BubbleSound.Play();

            userInputSum += int.Parse(message);
            buttonClickCounter += 1;

            // Lose/Win Condition
            if (buttonClickCounter == 2 && userInputSum != int.Parse(_GUIManager._instance.NumberToFind.text))
            {
                resultAnimator.SetTrigger("playAnimation");
                bonusTimeAnimator.SetTrigger("playAnimation");

                _GUIManager._instance.ShowResult.text = "WRONG!";
                _SoundManager._instance.WrongAnswer.Play();
                                
                _GUIManager._instance.ShowResult.color = Color.red;
                _GUIManager._instance.BonusTime.text = "- " + bonustTime.ToString();
                _GUIManager._instance.BonusTime.color = Color.red;

                int r = Random.Range(0, 2);
                NextNumber((NumbersOperation)r); // This will be random
                buttonClickCounter = 0;
                userInputSum = 0;
                TimeLeft -= bonustTime;
            }
            else if (buttonClickCounter == 2 && userInputSum == int.Parse(_GUIManager._instance.NumberToFind.text))
            {
                resultAnimator.SetTrigger("playAnimation");
                bonusTimeAnimator.SetTrigger("playAnimation");

                _GUIManager._instance.ShowResult.text = "CORRECT!";
                _SoundManager._instance.CorrectAnswer.Play();
                
                _GUIManager._instance.ShowResult.color = new Color(0.4859577f, 1f, 0f, 1f);
                _GUIManager._instance.BonusTime.text = "+ " + bonustTime.ToString() + "!";
                _GUIManager._instance.BonusTime.color = new Color(0.4859577f, 1f, 0f, 1f);

                int r = Random.Range(0, 2);
                NextNumber((NumbersOperation)r); // This will be random
                buttonClickCounter = 0;
                userInputSum = 0;
                TimeLeft += bonustTime;
                score += pointToGive;
                _GUIManager._instance.PlayerScoreValue.text = score.ToString();


            }
        }

        private void SubtractionButtonListener(string message, Button btn)
        {
            btn.interactable = false;
            _SoundManager._instance.BubbleSound.Play();

            buttonClickCounter += 1;

            if (buttonClickCounter == 1)
                firstSubtractNumber += int.Parse(message);

            if (buttonClickCounter == 2)
            {
                secondSubtractNumber = int.Parse(message);
                userInputSum = firstSubtractNumber - secondSubtractNumber;
            }

            // Lose/Win Condition
            if (buttonClickCounter == 2 && userInputSum != int.Parse(_GUIManager._instance.NumberToFind.text))
            {
                resultAnimator.SetTrigger("playAnimation");
                bonusTimeAnimator.SetTrigger("playAnimation");

                _GUIManager._instance.ShowResult.text = "WRONG!";
                _SoundManager._instance.WrongAnswer.Play();
                
                _GUIManager._instance.ShowResult.color = Color.red;
                _GUIManager._instance.BonusTime.text = "- " + bonustTime.ToString();
                _GUIManager._instance.BonusTime.color = Color.red;

                int r = Random.Range(0, 2);
                NextNumber((NumbersOperation)r); // This will be random
                buttonClickCounter = 0;
                userInputSum = 0;
                firstSubtractNumber = 0;
                secondSubtractNumber = 0;
                TimeLeft -= bonustTime;
            }
            else if (buttonClickCounter == 2 && userInputSum == int.Parse(_GUIManager._instance.NumberToFind.text))
            {
                resultAnimator.SetTrigger("playAnimation");
                bonusTimeAnimator.SetTrigger("playAnimation");

                _GUIManager._instance.ShowResult.text = "CORRECT!";
                _SoundManager._instance.CorrectAnswer.Play();
                
                _GUIManager._instance.ShowResult.color = new Color(0.4859577f, 1f, 0f, 1f );
                _GUIManager._instance.BonusTime.text = "+ " + bonustTime.ToString() + "!";
                _GUIManager._instance.BonusTime.color = new Color(0.4859577f, 1f, 0f, 1f);

                int r = Random.Range(0, 2);
                NextNumber((NumbersOperation)r); // This will be random
                buttonClickCounter = 0;
                userInputSum = 0;
                firstSubtractNumber = 0;
                secondSubtractNumber = 0;
                TimeLeft += bonustTime;
                score += pointToGive;
                _GUIManager._instance.PlayerScoreValue.text = score.ToString();

            }
        }

        #endregion

        private void TimerCountDown()
        {
            TimeLeft -= Time.deltaTime;
            int rounded = Mathf.RoundToInt(TimeLeft);
            _GUIManager._instance.TimeLeftText.text = rounded.ToString();
        }

        private void NextNumber(NumbersOperation numbersMode)
        {
            switch (numbersMode)
            {
                case NumbersOperation.Addition:
                    _instance.CurrentNumbersGameMode = NumbersOperation.Addition;
                    FillAdditionModeNumbers();
                    AssignButtonListeners();
                    break;
                case NumbersOperation.Subtraction:
                    _instance.CurrentNumbersGameMode = NumbersOperation.Subtraction;
                    FillSubtractionModeNumbers();
                    AssignButtonListeners();
                    break;
                case NumbersOperation.Multiply:
                    break;
                default:
                    break;
            }
        }

        private void AssignButtonListeners()
        {
            foreach (var button in _GUIManager._instance.NumericAnswers)
            {
                // Get children(text)
                Text text = button.GetComponentInChildren<Text>();

                // Check Numbers Game Mode and assign the appropriate listener
                switch (_instance.CurrentNumbersGameMode)
                {
                    case NumbersOperation.Addition:
                        button.onClick.RemoveAllListeners();
                        button.onClick.AddListener(delegate { AdditionButtonListener(text.text, button); });
                        break;
                    case NumbersOperation.Subtraction:
                        button.onClick.RemoveAllListeners();
                        button.onClick.AddListener(delegate { SubtractionButtonListener(text.text, button); });
                        break;
                    case NumbersOperation.Multiply:
                        break;
                    default:
                        break;
                }
            }
        }

        private void Update()
        {
            switch (_instance.CurrentNumbersGameMode)
            {
                case NumbersOperation.Addition:
                    _GUIManager._instance.GameModeText.text = "ADD!";
                    break;
                case NumbersOperation.Subtraction:
                    _GUIManager._instance.GameModeText.text = "SUBTRACT!";
                    break;
                case NumbersOperation.Multiply:
                    break;
                default:
                    break;
            }

            TimerCountDown();
        }
    }
}
