using System.Collections;
using MatchGame.Core;
using MatchGame.Data;
using UnityEngine;
namespace MatchGame
{
    public class TimerSystem : CoreSystem
    {
        private float startTime =0.0f;
        private bool isRunning;

        EventHandlerSystem _eventHandlerSystem;
        GameDataContainer _gameDataContainer;

        public override void Init()
        {
            base.Init();
            _eventHandlerSystem = ServiceLocator.Instance.Get<EventHandlerSystem>();
            _gameDataContainer = ServiceLocator.Instance.Get<GameDataContainer>();
            _eventHandlerSystem.AddListener(GameEventKeys.GameStateUpdated, Start);
            _eventHandlerSystem.AddListener(GameEventKeys.GameOver, Stop);

        }
        public override void Finalise()
        {
            base.Finalise();
            _eventHandlerSystem.RemoveListener(GameEventKeys.GameStateUpdated, Start);
            _eventHandlerSystem.RemoveListener(GameEventKeys.GameOver, Stop);
        }
        public TimerSystem()
        {
            ResetTimer();
        }

        public void Start()
        {
            if (_gameDataContainer.GameState == State.Play)
            {


                isRunning = true;
                _gameDataContainer.CurrentTime = startTime;

                CoreContext.Instance.StartCoroutine(Countdown());
            }
            else if (_gameDataContainer.GameState == State.Pause)

            {
                isRunning = false;

            }


        }

        public void Stop()
        {
            isRunning = false;
        }

        public void ResetTimer()
        {
            _gameDataContainer.CurrentTime = startTime;
            Stop();
        }

        public bool IsRunning
        {
            get { return isRunning; }
        }

        public float CurrentTime
        {
            get { return _gameDataContainer.CurrentTime; }
        }
       
        private IEnumerator Countdown()
        {
            while (isRunning)
            {
                yield return new WaitForSeconds(Time.deltaTime);
                _gameDataContainer.CurrentTime += Time.deltaTime;

            }
        }
        
    }
}
