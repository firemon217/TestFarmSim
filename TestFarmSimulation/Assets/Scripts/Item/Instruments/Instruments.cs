using UnityEngine;

namespace Items
{
    public class Instrument : Item
    {
        private ScriptableInstruments _instrumentData;

        public ScriptableInstruments InstrumentData { get => _instrumentData; set => _instrumentData = value; }

        public Instrument(ScriptableInstruments instrumentData) : base(instrumentData)
        {
            _instrumentData = instrumentData;
        }
    }
}