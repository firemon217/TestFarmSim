using System;
using UnityEngine;
using Items;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("Цвета")]
    [SerializeField] private Image _icon;
    [SerializeField] private Image _background;

    [Header("Цвета")]
    [SerializeField] private Color _backgroundColorNormal;
    [SerializeField] private Color _backgroundColorDisable;
    [SerializeField] private Color _backgroundColorSelected;

    // Slots data
    private int _slotIndex = -1;
    private Instrument _instrument;

    // Triggers
    private bool _isSelected = false;
    private bool _isActive = false;

    // Events
    public event Action<Instrument> onPressSlot;

    // Property
    public bool IsSelected => _isSelected;
    public bool IsActive => _isActive;
    public int SlotIndex { get => _slotIndex; set => _slotIndex = value; }

    // Invoke onPressSlot to select an item and drag it to the active hand
    public void Selected()
    {
        onPressSlot?.Invoke(_instrument);
        ChangeBackgorundColor(_backgroundColorSelected);
        _isSelected = true;
    }

    // Change color for not selected slots
    public void Unselected()
    {
        ChangeBackgorundColor(_backgroundColorNormal);
        _isSelected = false;
    }

    // Change inctrument in slots
    public void InstrumentChange(Instrument instrument)
    {
        _instrument = instrument;
        _icon.sprite = _instrument.Icon;
        _icon.color = new Color(1f, 1f, 1f, 1f);
        ChangeBackgorundColor(_backgroundColorNormal);
        _isActive = true;
    }

    // Change inctrument in slots to null
    public void InstrumentChange()
    {
        _instrument = null;
        _icon.sprite = null;
        _icon.color = new Color(0f, 0f, 0f, 0f);
        ChangeBackgorundColor(_backgroundColorDisable);
        _isActive = false;
    }

    // Change slot`s background color
    private void ChangeBackgorundColor(Color color)
    {
        _background.color = color;
    }
}
