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
        [SerializeField] private TextMeshProUGUI _indexTxt;
        private Quaternion _currentRotation;

        [HideInInspector] public bool _revealed = false;

        private bool _clicked = false;
        private bool _matched = false;
        private Card _card;
        private Button button;

        private GameDataContainer _gameDataContainer;
      
        public void SetIndex(int index,int id) {

            _card = new( index,id);
            _indexTxt.text =""+ id;
            _cardImage.sprite = _gameDataContainer.Sprites[_card.id];

            //base.EnableScreen();


        }

        public Card GetIndex() { return _card; }
        private void OnEnable()
        {
            Init();
        }
        public override void Init()
        {
            base.Init();
            _clicked = false;
            _currentRotation = gameObject.transform.rotation;

            _gameDataContainer  = ServiceLocator.Instance.Get<GameDataContainer>();

            // Get a reference to the Button component.
            button = GetComponent<Button>();
            _indexTxt = GetComponentInChildren<TextMeshProUGUI>();
            // Attach the OnClick event handler to the button.
            button.onClick.AddListener(ChangeImage);
            eventHandlerSystem.AddListener(GameEventKeys.FlipCards, FlipBack);
            eventHandlerSystem.AddListener(GameEventKeys.CardMatched, OnCardMatched);
        }

        public override void Finalise()
        {
            base.Finalise();
            eventHandlerSystem.RemoveListener(GameEventKeys.FlipCards, FlipBack);
            eventHandlerSystem.RemoveListener(GameEventKeys.CardMatched, OnCardMatched);


            button.onClick.RemoveAllListeners();
        }

        private void OnCardMatched()
        {

           if(_gameDataContainer.DisplayedCard.id==_card.id)
            {
                _cardImage.gameObject.SetActive(false);
                _matched = true;
            }
        }

        private void ChangeImage()
        {
            if (!_clicked)
            {
                //_pictureManager.CurrentPuzzleState = PictureManager.PuzzleState.PuzzleRotating;

                //if (!GameSettings.Instance.IsSoundEffectMutedPermanently())
                //_audio.Play();

                StartCoroutine(Rotate(45, false));
                _clicked = true;
            }
            //else
            //{
            //    FlipBack();
            //    _clicked = false;

            //}
        }

        public void FlipBack()
        {
            if ( _revealed)
            {
                //_pictureManager.CurrentPuzzleState = PictureManager.PuzzleState.PuzzleRotating;
                _revealed = false;

                //if (!GameSettings.Instance.IsSoundEffectMutedPermanently())
                //_audio.Play();

                StartCoroutine(Rotate(45, true));
                    _clicked = false;

            }
        }

        private IEnumerator Rotate(float angle, bool FirstMat)
        {
            float rot = 0f;
            const float dir = 1f;
            const float rotSpeed = 90.0f;
            const float rotSpeed1 = 90.0f;
            bool assigned = false;
            float startAngle = angle;

            if (FirstMat)
            {
                while (rot < angle)
                {
                    float step = Time.deltaTime * rotSpeed1;
                    gameObject.GetComponent<Transform>().Rotate(new Vector3(0, 2, 0) * step * dir);
                    if (rot >= (startAngle - 2) && !assigned)
                    {
                        ApplyFirstMaterial();
                        assigned = true;
                    }
                    rot += (1 * step * dir);
                    yield return null;
                }
            }
            else
            {

                while (angle > 0)
                {
                    float step = Time.deltaTime * rotSpeed;
                    gameObject.GetComponent<Transform>().Rotate(new Vector3(0, 2, 0) * step * dir);
                    angle -= (1 * step * dir);
                    yield return null;
                }
            }

            gameObject.GetComponent<Transform>().rotation = _currentRotation;

            if (!FirstMat)
            {
                if (!_revealed)
                {
                    _revealed = true;
                    _gameDataContainer.DisplayedCard = _card;
                }
                ApplySecondMaterial();
                //_pictureManager.CheckPicture();
            }
            else
            {
                ApplyFirstMaterial();  //_pictureManager.CurrentPuzzleState = PictureManager.PuzzleState.CanRotate;
                //_pictureManager.PuzzleRevealedNumber = PictureManager.RevealedState.NoRevealed;
            }

            //_clicked = false;
        }

        //public void SetFirstMaterial(Material mat, string texturePath)
        //{
        //    _firstMaterial = mat;
        //    //_firstMaterial.mainTexture = Resources.Load(texturePath, typeof(Texture2D)) as Texture2D;
        //}

        //public void SetSecondMaterial(Material mat, string texturePath)
        //{
        //    _secondMaterial = mat;
        //    _secondMaterial.mainTexture = Resources.Load(texturePath, typeof(Texture2D)) as Texture2D;
        //}
        public void SetCardImage(Sprite sprite)
        {
            _cardImage.sprite = sprite;
        }
        public void ApplyFirstMaterial()
        {
            gameObject.GetComponent<Image>().sprite = _firstMaterial;

            _cardImage.gameObject.SetActive(false);

        }

        public void ApplySecondMaterial()
        {
            gameObject.GetComponent<Image>().sprite = _secondMaterial;
            _cardImage.gameObject.SetActive(true);

        }

        public void Deactivate()
        {
            StartCoroutine(DeactivateCoroutine());
        }

        private IEnumerator DeactivateCoroutine()
        {
            //Revealed = false;

            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
        }
    }
}