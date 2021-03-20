using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MonoBeh
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public GameObject titleUI, gameUI, winUI, looseUI;
        public TextMeshProUGUI pelletsCounter, scoreCounter;
        public int score;

        private readonly Dictionary<UITypes, GameObject> _panels = new Dictionary<UITypes, GameObject>();

        private enum UITypes
        {
            Title,
            Game,
            Win,
            Loose
        }

        public void Awake()
        {
            Instance = this;
            _panels[UITypes.Title] = titleUI;
            _panels[UITypes.Game] = gameUI;
            _panels[UITypes.Win] = winUI;
            _panels[UITypes.Loose] = looseUI;
            Reset();
        }

        public void Reset()
        {
            SwitchUI(UITypes.Title);
            score = 0;
            InGame();
        }

        public void InGame()
        {
            SwitchUI(UITypes.Game);
        }

        public void Win()
        {
            SwitchUI(UITypes.Win);
        }

        public void Loose()
        {
            SwitchUI(UITypes.Loose);
        }

        public void AddPoints(int points)
        {
            score += points;
            scoreCounter.text = $"Score : {score}";
        }

        private void SwitchUI(UITypes activeUI)
        {
            foreach (var panel in _panels)
            {
                panel.Value.SetActive(panel.Key == activeUI);
            }
        }
    }
}