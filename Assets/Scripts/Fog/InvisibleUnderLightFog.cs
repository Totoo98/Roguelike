using Roguelike.CellMapping;
using UnityEngine;

namespace Roguelike.Fog
{
    public class InvisibleUnderLightFog : MonoBehaviour
    {
        SpriteRenderer[] renders;
        public CellElement CellElement { get; private set; }

        [SerializeField] InvisibleUnderLightFogHashSet lightFogSet = null;
        [SerializeField] VisibleCellHashSet visibleCellSet = null;

        private void Awake()
        {
            renders = GetComponentsInChildren<SpriteRenderer>();
            CellElement = GetComponent<CellElement>();
            CellElement.OnChangePosition += OnChangeCell;
        }

        private void OnEnable() => lightFogSet.Add(this);
        private void OnDisable() => lightFogSet.Remove(this);

        public void OnChangeCell(Vector2 position)
        {
            if(visibleCellSet.Collection.Contains(CellElement.Cell))
            {
                Reveal();
            }
            else
            {
                Hide();
            }
        }

        public void Reveal()
        {
            foreach(var render in renders)
            {
                if(render != null) render.enabled = true;
            }
        }

        public void Hide()
        {
            foreach (var render in renders)
            {
                if(render != null) render.enabled = false;
            }
        }

        private void OnDestroy()
        {
            CellElement.OnChangePosition -= OnChangeCell;
        }
    }
}
