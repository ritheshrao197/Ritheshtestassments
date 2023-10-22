
using System;
using MatchGame.Core;
using MatchGame.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MatchGame.View
{
    public class MainMenuView : View
    {
        [SerializeField] private Button _play;
        [SerializeField] private Button _quit;
        GameDataContainer _gameDataContainer;
        public CanvasGroup _screen;

        public override void Init()
        {
            base.Init();
            //_play.GetComponent<Button>();
            _screen = GetComponent<CanvasGroup>();  // Get the CanvasGroup component from the GameObject.

            _gameDataContainer = ServiceLocator.Instance.Get<GameDataContainer>();

            _play.onClick.AddListener(()=>OnPlayButtonClick());
            eventHandlerSystem.AddListener(GameEventKeys.GameStateUpdated, ShowMenu);
        }

        private void OnPlayButtonClick()
        {

            _gameDataContainer.CurrentLevel = 2;
            _gameDataContainer.GameState = State.Play;
        }
        public override void Finalise()
        {
            base.Finalise();
            _play.onClick.RemoveAllListeners();
            eventHandlerSystem.RemoveListener(GameEventKeys.GameStateUpdated, ShowMenu);

        }

        private void ShowMenu()
        {
           if( _gameDataContainer.GameState == State.Play)
            {
                DisableScreen();
            }
            else if (_gameDataContainer.GameState == State.MainMenu)

            {
                EnableScreen();
            }
        }

        /// <summary>
        /// Enable the screen by making it interactable and visible.
        /// </summary>
        public void EnableScreen()
        {
            _screen.interactable = true;
            _screen.blocksRaycasts = true;
            _screen.alpha = 1;
        }

        /// <summary>
        /// Disable the screen by making it non-interactable and invisible.
        /// </summary>
        public void DisableScreen()
        {
            // Deactivate the current screen.
            _screen.interactable = false;
            _screen.blocksRaycasts = false;
            _screen.alpha = 0;
        }
    }
}