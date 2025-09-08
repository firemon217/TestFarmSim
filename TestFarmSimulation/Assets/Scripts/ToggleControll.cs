using NWH.WheelController3D;
using UnityEngine;

public class ToggleControll
{
    private GameObject _player;
    private GameObject _car;
    private CarController _carController;
    private bool isActiveCar = false;

    public ToggleControll(GameObject car)
    {
        _car = car;
    }

    public void SetPlayerObject(GameObject player)
    {
        _player = player;
    }

    public void UnsetPlayerObject()
    {
        if(!isActiveCar) _player = null;
    }

    public void Init()
    {
        _carController = _car.GetComponent<CarController>();
        _carController.enabled = false;
    }

    public void Toggle()
    {
        if(isActiveCar)
        {
            _player.SetActive(true);
            _carController.enabled = false;
        }
        if(!isActiveCar && _player != null)
        {
            _player.SetActive(false);
            _carController.enabled = true;
        }
    }
}
