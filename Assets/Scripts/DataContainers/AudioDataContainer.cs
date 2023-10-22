using MatchGame.Core;

namespace MatchGame.Data
{
    public class AudioDataContainer
    {
        private bool _onButtonClick = false;
        public bool OnButtonClick
        {
            get
            {
                return _onButtonClick;
            }
            set
            {
                _onButtonClick = value;
                ServiceLocator.Instance.Get<EventHandlerSystem>().TriggerEvent(AudioEventKeys.OnButtonClick);


            }
        }


    }

}