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
            _cardBack.sprite = _secondMaterial;
            _cardImage.gameObject.SetActive(true);

        }

        //private void OnEnable()
        //{
        //    //Init();
        //}
        //public override void Init()
        //{
        //    base.Init();
        //    _clicked = false;
        //    _currentRotation = gameObject.transform.rotation;

        //    _gameDataContainer  = ServiceLocator.Instance.Get<GameDataContainer>();
        //    _cardBack = GetComponent<Image>();
        //    // Get a reference to the Button component.
        //    button = GetComponent<Button>();
        //    _indexTxt = GetComponentInChildren<TextMeshProUGUI>();
        //    // Attach the OnClick event handler to the button.
        //    //button.onClick.AddListener(OnCardClick);
        //    //eventHandlerSystem.AddListener(GameEventKeys.FlipCards, FlipBack);
        //    //eventHandlerSystem.AddListener(GameEventKeys.CardMatched, OnCardMatched);
        //}

        //public override void Finalise()
        //{
        //    base.Finalise();
        //    //eventHandlerSystem.RemoveListener(GameEventKeys.FlipCards, FlipBack);
        //    //eventHandlerSystem.RemoveListener(GameEventKeys.CardMatched, OnCardMatched);


        //    button.onClick.RemoveAllListeners();
        //}

        //private void OnCardMatched()
        //{
        //   if(_gameDataContainer.DisplayedCard.id==_card.id)
        //    {
        //        Debug.Log("OnCardMatched " + _card.id + "  " + _card.index);
        //        _cardBack.color = Color.blue;
        //        //_cardImage.gameObject.SetActive(false);
        //        _matched = true;
        //        //Deactivate();
        //    }
        //}

        //private void Start()
        //{

        //}


        //private void OnCardClick()
        //{
        //    if (!_card.isFlipped && !_card.isMatched)
        //    {
        //        _card.Flip();
        //        // Flip the card's visual representation (e.g., show the card face)
        //        // You'll need to implement the UI and card flipping animations in Unity.

        //        //cardManager.CheckForMatch(cardData);
        //        _gameDataContainer.DisplayedCard = _card;

        //    }//if (!_clicked)
        //    //{
        //    //    //_pictureManager.CurrentPuzzleState = PictureManager.PuzzleState.PuzzleRotating;

        //    //    //if (!GameSettings.Instance.IsSoundEffectMutedPermanently())
        //    //    //_audio.Play();
        //    //    StartCoroutine(Rotate(45, false));
        //    //    _clicked = true;
        //    //}
        //    ////else
        //    ////{
        //    ////    FlipBack();
        //    ////    _clicked = false;

        //    ////}
        //}

        //public void FlipBack()
        //{
        //    //if (_clicked)
        //    {
        //        //_pictureManager.CurrentPuzzleState = PictureManager.PuzzleState.PuzzleRotating;
        //        _clicked = false;
        //        _revealed = false;
        //        //if (!GameSettings.Instance.IsSoundEffectMutedPermanently())
        //        //_audio.Play();
        //        if (!_matched)
        //            StartCoroutine(Rotate(45, true));
        //            //_clicked = false;

        //    }
        //}

        //private IEnumerator Rotate(float angle, bool FirstMat)
        //{
        //    if (!FirstMat)
        //    {
        //        if (!_revealed)
        //        {
        //            _revealed = true;
        //            _gameDataContainer.DisplayedCard = _card;

        //        }
        //        ApplySecondMaterial();
        //        //_pictureManager.CheckPicture();
        //    }
        //    else
        //    {
        //        ApplyFirstMaterial();  //_pictureManager.CurrentPuzzleState = PictureManager.PuzzleState.CanRotate;
        //        //_pictureManager.PuzzleRevealedNumber = PictureManager.RevealedState.NoRevealed;
        //    }
        //    yield return null;

        //    //float rot = 0f;
        //    //const float dir = 1f;
        //    //const float rotSpeed = 180;
        //    ////const float rotSpeed1 = 45;
        //    //bool assigned = false;
        //    //float startAngle = angle;

        //    ////if (FirstMat)
        //    ////{
        //    //while (rot < angle)
        //    //{
        //    //    float step = Time.deltaTime * rotSpeed;
        //    //    gameObject.GetComponent<Transform>().Rotate(new Vector3(0, 2, 0) * step * dir);
        //    //    if (rot >= (startAngle - 2) && !assigned)
        //    //    {
        //    //        ApplyFirstMaterial();
        //    //        assigned = true;
        //    //    }
        //    //    rot += (1 * step * dir);
        //    //    yield return null;
        //    //}
        //    //}
        //    //else
        //    //{

        //    //    while (angle > 0)
        //    //    {
        //    //        float step = Time.deltaTime * rotSpeed;
        //    //        gameObject.GetComponent<Transform>().Rotate(new Vector3(0, 2, 0) * step * dir);
        //    //        angle -= (1 * step * dir);
        //    //        yield return null;
        //    //    }
        //    //}


        //    //if (!FirstMat)
        //    //{
        //    //    if (!_revealed)
        //    //    {
        //    //        _revealed = true;
        //    //    }
        //    //    ApplySecondMaterial();
        //    //    //_pictureManager.CheckPicture();
        //    //}
        //    //else
        //    //{
        //    //    ApplyFirstMaterial();  //_pictureManager.CurrentPuzzleState = PictureManager.PuzzleState.CanRotate;
        //    //    //_pictureManager.PuzzleRevealedNumber = PictureManager.RevealedState.NoRevealed;
        //    //}
        //    gameObject.GetComponent<Transform>().rotation = _currentRotation;

        //    ////_clicked = false;
        //}

        ////public void SetFirstMaterial(Material mat, string texturePath)
        ////{
        ////    _firstMaterial = mat;
        ////    //_firstMaterial.mainTexture = Resources.Load(texturePath, typeof(Texture2D)) as Texture2D;
        ////}

        ////public void SetSecondMaterial(Material mat, string texturePath)
        ////{
        ////    _secondMaterial = mat;
        ////    _secondMaterial.mainTexture = Resources.Load(texturePath, typeof(Texture2D)) as Texture2D;
        ////}
        //public void SetCardImage(Sprite sprite)
        //{
        //    _cardImage.sprite = sprite;
        //}

        //public void Deactivate()
        //{
        //    StartCoroutine(DeactivateCoroutine());
        //}
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
    }
}