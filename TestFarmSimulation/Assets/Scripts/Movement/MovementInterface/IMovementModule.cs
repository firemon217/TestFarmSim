using UnityEngine;

namespace Controller
{
    public interface IMovementModule
    {
        public void HandleInput();
        public void HandleUpdate();
    }
}