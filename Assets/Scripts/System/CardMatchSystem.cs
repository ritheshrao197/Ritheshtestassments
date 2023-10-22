using System;
using System.Collections.Generic;
using MatchGame.Core;
using MatchGame.Data;
using UnityEngine;

namespace MatchGame
{
    public class CardMatchSystem : CoreSystem
    {

        EventHandlerSystem _eventHandlerSystem;
        GameDataContainer _gameDataContainer;
        public override void Init()
        {
            base.Init();
            _eventHandlerSystem = ServiceLocator.Instance.Get<EventHandlerSystem>();
            _gameDataContainer = ServiceLocator.Instance.Get<GameDataContainer>();
        }
        public override void AddListener()
        {
            base.AddListener();
            _eventHandlerSystem.AddListener(GameEventKeys.CardDisplayed, CardDisplayed);

        }

        public override void RemoveListener()
        {
            base.RemoveListener();
            _eventHandlerSystem.RemoveListener(GameEventKeys.CardDisplayed, CardDisplayed);

        }
        public override void Finalise()
        {
            base.Finalise();

        }


        private void CardDisplayed()
        {
            //Debug.Log("bfr.CurrentCard. " + _gameDataContainer.CurrentCard.id + _gameDataContainer.CurrentCard.index);
            Debug.Log("bfr.DisplayedCard. " + _gameDataContainer.DisplayedCard.id + _gameDataContainer.DisplayedCard.index);

            if (_gameDataContainer.CurrentCard.Keys.Count == 0)
            {
                _gameDataContainer.CurrentCard.Add(_gameDataContainer.DisplayedCard.id, _gameDataContainer.DisplayedCard.index);
            }
            else
            {
                if (_gameDataContainer.CurrentCard.ContainsKey(_gameDataContainer.DisplayedCard.id))
                {
                    _gameDataContainer.CardMatched = true;

                }
                //else
                {
                    _gameDataContainer.CurrentCard.Clear();
                    _gameDataContainer.DisplayedCard = new Card(-1, -1);
                }
            }


            //if (_gameDataContainer.CurrentCard.index != _gameDataContainer.DisplayedCard.index)
            //{
            //    if (_gameDataContainer.CurrentCard.index >= 0)
            //    {
            //        if (_gameDataContainer.CurrentCard.id == _gameDataContainer.DisplayedCard.id)
            //        {
            //            _gameDataContainer.CardMatched = true;
            //        }
            //        else
            //        {
            //            _gameDataContainer.CurrentCard.id = _gameDataContainer.DisplayedCard.id = -1;
            //        }
            //    }
            //    else 

            //    {
            //        _gameDataContainer.CurrentCard = _gameDataContainer.DisplayedCard;
            //    }
            //}

            //Debug.Log("after.CurrentCard. " + _gameDataContainer.CurrentCard.id + _gameDataContainer.CurrentCard.index);
            Debug.Log("after.DisplayedCard. " + _gameDataContainer.DisplayedCard.id + _gameDataContainer.DisplayedCard.index);


        }

    }

}
