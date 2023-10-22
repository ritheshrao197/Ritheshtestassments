using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MatchGame.Data;
using MatchGame.Core;
using System;

namespace MatchGame.View
{
    using UnityEngine;
    using UnityEngine.UI;

   
    public class CardView : View
    {
        [SerializeField] private Sprite _firstMaterial;
        [SerializeField] private Sprite _secondMaterial;
        [SerializeField] private Image _cardImage;
        [SerializeField] private Image _cardBack;
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI _indexTxt;
        private Quaternion _currentRotation;

        [HideInInspector] public bool _revealed = false;
        public int cardIndex;
        public int cardID;
        private Card cardData;
        private CardMatchSystem _cardManager;

        private GameDataContainer _gameDataContainer;


        public void SetIndex(int index, int id) {
            base.Init();
            cardData = new Card(index, id);
            cardID = id;
            cardIndex = index;
                _indexTxt.text =""+ id;
            _gameDataContainer = ServiceLocator.Instance.Get<GameDataContainer>();
            _cardManager = ServiceLocator.Instance.Get<CardMatchSystem>();
            {
                _cardImage.sprite = _gameDataContainer.Sprites[id];
            }
            //catch(Exception e)
            //{

            //}
          
            button = GetComponent<Button>();
            // Attach the OnClick event handler to the button.
            button.onClick.AddListener(OnCardClick);
            eventHandlerSystem.AddListener(GameEventKeys.FlipCards, ApplyFirstMaterial);
            eventHandlerSystem.AddListener(GameEventKeys.CardMatched, CardMatched);



        }
        private void OnDisable()
        {
            eventHandlerSystem.RemoveListener(GameEventKeys.FlipCards, ApplyFirstMaterial);
            eventHandlerSystem.RemoveListener(GameEventKeys.CardMatched, CardMatched);
            //button.onClick.RemoveListener(OnCardClick);


        }

        private void CardMatched()
        {
            if (cardData.isMatched)
            {
                StartCoroutine(DeactivateCoroutine());
            }
        }

        private void OnCardClick()
        {
            if (!cardData.isFlipped && !cardData.isMatched)
            {
                cardData.Flip();
                ApplySecondMaterial();
                // Flip the card's visual representation (e.g., show the card face)
                // You'll need to implement the UI and card flipping animations in Unity.

                _cardManager.CheckForMatch(cardData);
            }
        }
        public Card GetIndex() { return cardData; }
        public void ApplyFirstMaterial()
        {
            FlipImageHorizontally();
            if (_gameDataContainer.FlipCards.id == cardData.id &&
                _gameDataContainer.FlipCards.index == cardData.index)
            {
                cardData = _gameDataContainer.FlipCards;

                _cardBack.sprite = _firstMaterial;

                _cardImage.gameObject.SetActive(false);
            }
          

        }

        public void ApplySecondMaterial()
        {
            FlipImageHorizontally();
            _cardBack.sprite = _secondMaterial;
            _cardImage.gameObject.SetActive(true);

        }

        private Color initialColor;
        private Color targetColor = new Color(0f, 0f, 0f, 0f); // Target color with alpha set to 0.

        private IEnumerator DeactivateCoroutine()
        {
            //Revealed = false;
            float elapsedTime = 0f;
            initialColor = _cardBack.color;
            while (elapsedTime < 1)
            {
                _cardBack.color = Color.Lerp(initialColor, targetColor, elapsedTime / 1);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            _cardImage.gameObject.SetActive(false);

            _cardBack.color = targetColor;
            //yield return new WaitForSeconds(1f);
            //gameObject.SetActive(false);
        }
        private void FlipImageHorizontally()
        {
            // Get the current local scale
            Vector3 currentScale = _cardImage.transform.localScale;

            // Define the target scale for the flip
            Vector3 targetScale = currentScale;
            targetScale.x *= -1;

            // Duration of the flip animation
            float duration = 0.20f; // Adjust this to control the speed of the flip

            // Use a Coroutine to animate the flip over time
            StartCoroutine(AnimateFlip(currentScale, targetScale, duration));
        }
        private IEnumerator AnimateFlip(Vector3 startScale, Vector3 targetScale, float duration)
        {
            float startTime = Time.time;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                // Calculate the interpolation factor
                float t = elapsedTime / duration;

                // Lerp between startScale and targetScale
                _cardImage. transform.localScale = Vector3.Lerp(startScale, targetScale, t);

                elapsedTime = Time.time - startTime;

                yield return null;
            }

            // Ensure the final scale is exactly the target scale
            _cardImage. transform.localScale = targetScale;
        }
    }
}