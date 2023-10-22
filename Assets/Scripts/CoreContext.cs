using System;
using System.Collections;
using MatchGame.Data;
using MatchGame.View;
using UnityEngine;
namespace MatchGame.Core
{
    public class CoreContext : MonoBehaviour
    {

        private static CoreContext instance;

        public static CoreContext Instance
        {
            get
            {

                return instance;
            }
        }
        // Reference to various managers, such as input manager, UI manager, etc.
        AudioSystem _audioSystem;
        ServiceLocator _serviceLocator;
        EventHandlerSystem _eventHandlerSystem;
        //core systems
        SpriteProviderSystem _spriteProviderSystem;

        CardGenerator _cardGenerator;
        CardMatchSystem _cardMatchSystem;
        TimerSystem _timerSystem;
        ScoringSystem _scoringSystem;

        GameDataContainer _gameDataContainer = new();
        AudioDataContainer _audioDataContainer;
        SettingsDataContainer _settingsDataContainer;

        //public SpriteContainer spriteContainer; // Reference to the ScriptableObject containing sprites.

        //views
        [SerializeField] GamePlayView _gamePlayView;
        [SerializeField] MainMenuView _mainMenuView;
        [SerializeField] LevelSelectionView _levelSelectionView;
        [SerializeField] PauseScreenView _pauseScreenView;

        [SerializeField] GridLayoutHandler _gridLayoutHandler;
    

        private void Awake()
        {
            if (instance == null)
                instance = this;
            // Initialize your managers and other components here.
            SystemInitialisation();
            DataContainerRegistration();
            // Set initial game state
            //isGamePaused = false;
            //isGameOver = false;
            CoreSystemInitialisation();
            ViewInitialisation();
        }

        private void ViewInitialisation()
        {
            _gamePlayView.Init();
            _mainMenuView.Init();
            _levelSelectionView.Init();
            _pauseScreenView.Init();
        }

        private void CoreSystemInitialisation()
        {

            _spriteProviderSystem = new();
            _spriteProviderSystem.Init();
            _spriteProviderSystem.AddListener();

            _cardGenerator = new();
            _cardGenerator.Init();
            _cardGenerator.AddListener();


            _cardMatchSystem = new();
            _cardMatchSystem.Init();
            _serviceLocator.Register(_cardMatchSystem);

            _timerSystem = new();
            _timerSystem.Init();
            _serviceLocator.Register(_timerSystem);




            _scoringSystem = new();
            _scoringSystem.Init();
            _serviceLocator.Register(_scoringSystem);

        }

        private void SystemInitialisation()
        {
            _serviceLocator = new();
            _eventHandlerSystem = new();
            //_audioSystem = new();

            _serviceLocator.Register(_eventHandlerSystem);
            _serviceLocator.Register(_audioSystem);



        }

        private void DataContainerRegistration()
        {
            _serviceLocator.Register(_gameDataContainer);
            _serviceLocator.Register(_audioDataContainer);
            _serviceLocator.Register(_settingsDataContainer);
        }
      
       
    }
}