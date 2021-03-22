using UnityEngine;

namespace Roguelike.Control
{
    [CreateAssetMenu(fileName = "PlayerControlState", menuName = "Control/PlayerControlState")]
    public class PlayerControlState : ScriptableObject
    {
        public enum State { None, Inventory, Playing }

        public State state;
    }
}