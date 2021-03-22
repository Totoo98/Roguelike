using Roguelike.Interactions;
using UnityEngine.SceneManagement;

namespace Roguelike.Level
{
    public class DownStairs : CellClickInteractable
    {
        public override bool InteractWith(Interactor interactor)
        {
            SceneManager.LoadScene(0);
            return true;
        }
    }
}
