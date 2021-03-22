using Roguelike.Control;
using System;
using UnityEngine;

namespace Roguelike.Interactions
{
    public class PlayerInteractor : Interactor
    {
        ClickToGetInteractable clicker;

        CellClickInteractable targetInteraction;
        public void SetInteraction(CellClickInteractable interactable) => targetInteraction = interactable;

        public Func<bool> OnDecisionMaking;

        protected override void Awake()
        {
            base.Awake();
            clicker = GetComponent<ClickToGetInteractable>();
        }

        public override void GetInteraction()
        {
            if (GetComponent<LivingEntity>().IsParalysed)
            {
                turnEntity.SpendTime(1f);
                return;
            }

            if (targetInteraction != null)
            {
                if(TryToInteract(targetInteraction))
                {
                    targetInteraction = null;
                    return;
                }
            }

            bool hasDecisionBeenMade = false;
            if(OnDecisionMaking != null)
            {
                hasDecisionBeenMade = OnDecisionMaking.Invoke();
                if (hasDecisionBeenMade) return;
            }

            if (Input.GetKey(KeyCode.W))
            {
                turnEntity.SpendTime(0.5f);
                return;
            }

            IClickable interactable = clicker.GetClickable();
            if (interactable != null)
            {
                interactable.OnClickedAndChosen(this);
                return;
            }
        }
    }
}
