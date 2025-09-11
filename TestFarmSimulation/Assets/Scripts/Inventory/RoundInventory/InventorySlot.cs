using System;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Instrument _instrument;
    [SerializeField] private Image _icon;
    private Button _button;
    private int slotIndex = -1;

    public event Action<string> onHoverSlot;
    public event Action<Instrument> onPressSlot;

    public int SlotIndex { get => slotIndex; set => slotIndex = value; }
    public Instrument Instrument { get => _instrument; set => _instrument = value; }
    public void OnClick()
    {
        onPressSlot?.Invoke(_instrument);
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void InstrumentChange(Instrument instrument)
    {
        _instrument = instrument;
        _icon.sprite = _instrument.InstrumentData.Icon;
        _icon.color = new Color(1f, 1f, 1f, 1f);
    }

    public void InstrumentChange()
    {
        _instrument = null;
        _icon.sprite = null;
        _icon.color = new Color(0f, 0f, 0f, 0f);
        _button.interactable = false;
    }

    public void OnHover()
    {
        onHoverSlot?.Invoke(_instrument.InstrumentData.Name);
    }

    public void OnUnhover()
    {
        onHoverSlot?.Invoke("");
    }
}
