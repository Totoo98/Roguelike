using Roguelike.AI;

namespace Roguelike.Interactions
{
    public interface IClickable
    {
        float ClickPriority { get; }
        void OnClickedAndChosen(PlayerInteractor interactor);
    }
}