using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerActiveHand : MonoBehaviour
    {
        [SerializeField] private Image _handIcon;
        private Instrument _instrument;

        void Awake()
        {
            _handIcon.sprite = null;
            _handIcon.color = new Color(0f, 0f, 0f, 0f);
        }

        public void ChangeInstrument(Instrument instrument)
        {
            _instrument = instrument;
            _handIcon.sprite = instrument.InstrumentData.Icon;
            _handIcon.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}