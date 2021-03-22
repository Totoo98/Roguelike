using System.Collections.Generic;

namespace Roguelike.CellMapping
{
    public partial class Cell
    {
        public class CellComposition
        {
            public CellElement interactor = null;
            public CellElement solidInteractable = null;
            public CellElement trap = null;
            public List<CellElement> interactables = new List<CellElement>();
            public List<CellElement> triggers = new List<CellElement>();

            public bool TryToAdd(CellElement cellElement)
            {
                switch (cellElement.Tag)
                {
                    case CellElementTag.Interactor:
                        if (interactor != null) return false;
                        interactor = cellElement;
                        break;

                    case CellElementTag.Trap:
                        if (trap != null) return false;
                        trap = cellElement;
                        break;

                    case CellElementTag.SolidInteractable:
                        if (solidInteractable != null) return false;
                        solidInteractable = cellElement;
                        break;

                    case CellElementTag.Interactable:
                        interactables.Add(cellElement);
                        break;

                    case CellElementTag.Trigger:
                        triggers.Add(cellElement);
                        break;
                }

                return true;
            }

            public void Remove(CellElement cellElement)
            {
                switch (cellElement.Tag)
                {
                    case CellElementTag.Interactor:
                        if (interactor == cellElement) interactor = null;
                        break;

                    case CellElementTag.Trap:
                        if (trap == cellElement) trap = null;
                        break;

                    case CellElementTag.SolidInteractable:
                        if (solidInteractable == cellElement) solidInteractable = null;
                        break;

                    case CellElementTag.Interactable:
                        interactables.Remove(cellElement);
                        break;

                    case CellElementTag.Trigger:
                        triggers.Remove(cellElement);
                        break;
                }
            }
        }
    }
}