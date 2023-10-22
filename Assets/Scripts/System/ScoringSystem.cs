using System;
using MatchGame.Core;
using MatchGame.Data;

namespace MatchGame
{
    public class ScoringSystem : CoreSystem
    {

        EventHandlerSystem _eventHandlerSystem;
        GameDataContainer _gameDataContainer;
        TimerSystem _timerSystem;


        public override void Init()
        {
            base.Init();
            _eventHandlerSystem = ServiceLocator.Instance.Get<EventHandlerSystem>();
            _gameDataContainer = ServiceLocator.Instance.Get<GameDataContainer>();
            _timerSystem = ServiceLocator.Instance.Get<TimerSystem>();
        }
        public override void AddListener()
        {
            base.AddListener();
            _eventHandlerSystem.AddListener(GameEventKeys.GameStateUpdated, UpdateGameState);
            _eventHandlerSystem.AddListener(GameEventKeys.CardMatched, UpdateScore);

        }

        public override void RemoveListener()
        {
            base.RemoveListener();
            _eventHandlerSystem.RemoveListener(GameEventKeys.CardMatched, UpdateScore);
            _eventHandlerSystem.RemoveListener(GameEventKeys.GameStateUpdated, UpdateGameState);


        }

        private void UpdateGameState()
        {
            if(_gameDataContainer.GameState==State.Play)
            {
                _timerSystem.Start();
            }
            else if (_gameDataContainer.GameState == State.GameOver)
            {
                _timerSystem.Stop();
            }
        }

        public override void Finalise()
        {
            base.Finalise();

        }


        public void UpdateScore()
        {
            _gameDataContainer.Score += 1;
        }

        public void SubtractPoints(int points)
        {
            _gameDataContainer.Score -= 1;
        }


    }
}