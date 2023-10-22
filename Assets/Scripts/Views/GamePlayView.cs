
using System;
using MatchGame.Core;
using MatchGame.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MatchGame.View
{
    public class GamePlayView : View
    {
     [SerializeField]   private Button _pause;
        GameDataContainer _gameDataContainer;
        public CanvasGroup _screen;

        public override void Init()
        {
            base.Init();
            _screen = GetComponent<CanvasGroup>();  // Get the CanvasGroup component from the GameObject.

            _gameDataContainer = ServiceLocator.Instance.Get<GameDataContainer>();

            _pause.onClick.AddListener(() => OnPause());
            eventHandlerSystem.AddListener(GameEventKeys.GameStateUpdated, ShowGamePlay);
        }

        private void OnPause()
        {

            _gameDataContainer.GameState = State.Pause;
        }
        public override void Finalise()
        {
            base.Finalise();
            _pause.onClick.RemoveAllListeners();
            eventHandlerSystem.RemoveListener(GameEventKeys.GameStateUpdated, ShowGamePlay);

        }

        private void ShowGamePlay()
        {
            if (_gameDataContainer.GameState == State.Play)
            {
                EnableScreen ();
            }
            else if (_gameDataContainer.GameState == State.MainMenu)

            {
                DisableScreen();
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