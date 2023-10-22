using MatchGame.Core;

namespace MatchGame.Data
{
    public class SettingsDataContainer
    {
        private bool _isSoundOn = false;
        public bool IsSoundOn
        {
            get
            {
                return _isSoundOn;
            }
            set
            {
                _isSoundOn = value;
                ServiceLocator.Instance.Get<EventHandlerSystem>().TriggerEvent(SettingsEventKeys.SetSound);


            }
        }
        private bool _isMusicOn = false;
        public bool IsMusicOn
        {
            get
            {
                return _isMusicOn;
            }
            set
            {
                _isMusicOn = value;
                ServiceLocator.Instance.Get<EventHandlerSystem>().TriggerEvent(SettingsEventKeys.SetMusic);


            }
        }
    }

}