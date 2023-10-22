using System;
using MatchGame.Core;
using MatchGame.Data;
using UnityEngine;
using UnityEngine.UI;

namespace MatchGame.View
{
    public class PauseScreenView : View
    {
        [SerializeField] private Button _resume;
        [SerializeField] private Button _quit;
        GameDataContainer _gameDataContainer;
        public CanvasGroup _screen;

        public override void Init()
        {
            base.Init();
            _screen = GetComponent<CanvasGroup>();  // Get the CanvasGroup component from the GameObject.

            _gameDataContainer = ServiceLocator.Instance.Get<GameDataContainer>();

            eventHandlerSystem.AddListener(GameEventKeys.GameStateUpdated, ShowMenu);
            _resume.onClick.AddListener(() => Resume());
        }

        private void Resume()
        {
            _gameDataContainer.GameState = State.Play;
        }

       

        private void ShowMenu()
        {
            if (_gameDataContainer.GameState == State.Pause)
            {
                EnableScreen();
            }
            else

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
