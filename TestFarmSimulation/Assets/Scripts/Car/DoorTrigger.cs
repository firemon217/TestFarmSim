using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private ToggleControll _toggleControll;
    [SerializeField] private GameObject _car;

    private void Awake()
    {
        _toggleControll = new ToggleControll(_car);
        _toggleControll.Init();

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        //if(collision.gameObject.CompareTag("Player"))
        //{
        //    _toggleControll.SetPlayerObject(collision.gameObject);
        //}
    }

    //private void OnCollisionExit(Collision collider)
    //{
    //    if (collider.gameObject.CompareTag("Player"))
    //    {
    //        _toggleControll.SetPlayerObject(collider.gameObject);
    //    }
    //}
}
