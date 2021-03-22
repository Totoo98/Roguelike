using UnityEngine;

namespace Roguelike.CellMapping
{
    public class RandomPositionOnChangeCell : MonoBehaviour
    {
        [Range(0, 0.5f)]
        [SerializeField] float randomness = 0.3f;

        private void OnEnable() => GetComponent<CellElement>().OnChangeCell += RandomPosition;
        private void OnDisable() => GetComponent<CellElement>().OnChangeCell -= RandomPosition;

        public void RandomPosition(Cell cell)
        {
            var delta = new Vector2(Random.Range(-randomness, randomness), Random.Range(-randomness, randomness));
            transform.position += (Vector3)delta;
        }
    }
}
