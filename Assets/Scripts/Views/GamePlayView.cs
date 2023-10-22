
using System;
using UnityEngine.UI;

namespace MatchGame.View
{
    public class GamePlayView : View
    {
        private Button _play;

        public override void Init()
        {
            base.Init();
            _play.GetComponent<Button>();
            _play.onClick.AddListener(() => OnPlayButtonClick());
        }

        private void OnPlayButtonClick()
        {
            throw new NotImplementedException();
        }
        public override void Finalise()
        {
            base.Finalise();
            _play.onClick.RemoveAllListeners();
        }
    }
}