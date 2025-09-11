using UnityEngine;

public class Instrument
{
    private ScriptableInstruments _instrumentData;

    public ScriptableInstruments InstrumentData { get => _instrumentData; set => _instrumentData = value; }

    public Instrument(ScriptableInstruments instrumentData)
    {
        _instrumentData = instrumentData;
    }
}
