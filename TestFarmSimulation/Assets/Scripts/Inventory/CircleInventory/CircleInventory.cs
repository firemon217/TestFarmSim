using TMPro;
using UnityEngine;
using System;
using Items;

public class CircleInventory : MonoBehaviour
{

    [Header("Игрок")]
    [SerializeField] private Player.Player _player;

    [Header("Настройки меню")]
    [SerializeField] private RectTransform _menuCenter;
    [SerializeField] private float _deadZoneRadius = 20f;

    [Header("Предметы и слоты")]
    [SerializeField] private InventorySlot[] _slots;
    [SerializeField] private ScriptableInstruments[] _instData;
    [SerializeField] private TextMeshProUGUI _label;

    // Total number of tools for segment inventory
    private int _totalTools;

    // Invoke onChangeInstrument to drag it to the active hand
    public event Action<Instrument> onChangeInstrument;

    private void OnEnable()
    {
        // Unlock cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        // Lock camera
        _player.LockCamera(true);

        // Set total tools
        _totalTools = _slots.Length;

        if (_slots != null)
        {
            // Index for each slot
            short slotIndex = 0;
            foreach (InventorySlot slot in _slots)
            {
                slot.SlotIndex = slotIndex;
                if (_instData.Length > slotIndex)
                {
                    // If instrumets is intended for a slot
                    if (_instData[slotIndex] != null)
                    {
                        Instrument instrument = new Instrument(_instData[slotIndex]);
                        slot.InstrumentChange(instrument);
                        slot.onPressSlot += SelectItem;
                    }
                }
                else
                {
                    // If instrumets is not intended for a slot
                    slot.InstrumentChange();
                }
                slotIndex++;
            }
        }
        _label.text = "";
    }

    private int GetSelectedToolIndex()
    {
        // Get the mouse's local position relative to the menu center
        Vector2 localMousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _menuCenter,
            Input.mousePosition,
            null, // null for Screen Space Overlay
            out localMousePos
        );

        // Checking the dead zone
        if (localMousePos.magnitude < _deadZoneRadius)
            return -1;

        // Calculating the angle: 0° = up, growing counterclockwise
        float angle = Mathf.Atan2(localMousePos.x, localMousePos.y) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360f;

        // Divide the circle into sectors
        float sectorAngle = 360f / _totalTools;
        angle += sectorAngle / 2;
        int selectedIndex = Mathf.FloorToInt(angle / sectorAngle);

        // Limiting the index
        selectedIndex = Mathf.Clamp(selectedIndex, 0, _totalTools - 1);

        // For Debug
        //Debug.Log($"Angle: {angle}, Tool: {selectedIndex}");

        return selectedIndex;
    }

    private void Update()
    {
        // Set index selected slot
        int index = GetSelectedToolIndex();
        if (_slots.Length > index)
            foreach(var slot in _slots)
            {
                // Check for each active slot
                if (slot.IsActive)
                {
                    // If slot can be selected but not yet
                    if (slot.SlotIndex == index && !slot.IsSelected)
                    {
                        slot.Selected();
                    }
                    // Each unsekected slot
                    if (slot.SlotIndex != index)
                    {
                        slot.Unselected();
                    }
                }
            }
    }

    // If item is selected, drop it in hand and change inventory label
    public void SelectItem(Instrument instrument)
    {
        onChangeInstrument?.Invoke(instrument);
        _label.text = instrument.Name;
    }

    private void OnDisable()
    {
        // Unlock player camera
        _player.LockCamera(false);
        foreach (var slot in _slots)
        {
            if (slot)
                slot.onPressSlot -= SelectItem;
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
