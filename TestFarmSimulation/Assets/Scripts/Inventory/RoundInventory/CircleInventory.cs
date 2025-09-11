using TMPro;
using UnityEngine;
using System;

public class CircleInventory : MonoBehaviour
{
    [SerializeField] private InventorySlot[] slots;
    [SerializeField] private ScriptableInstruments[] instData;
    [SerializeField] private TextMeshProUGUI _label;

    public event Action<Instrument> onChangeInstrument;

    private void Awake()
    {
        if (slots != null)
        {
            short slotIndex = 0;
            foreach (InventorySlot slot in slots)
            {
                slot.SlotIndex = slotIndex;
                if (instData.Length > slotIndex)
                {
                    if (instData[slotIndex] != null)
                    {
                        Instrument instrument = new Instrument(instData[slotIndex]);
                        slot.InstrumentChange(instrument);
                        slot.onHoverSlot += ChangeLabel;
                        slot.onPressSlot += ChangeItemInActiveHand;
                    }
                }
                else
                {
                    slot.InstrumentChange();
                }
                slotIndex++;
            }
        }
    }

    public void ChangeItemInActiveHand(Instrument instrument)
    {
        onChangeInstrument?.Invoke(instrument);
    }

    private void ChangeLabel(string name)
    {
        _label.text = name;
    }

    private void OnEnable()
    {
        _label.text = "";
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
