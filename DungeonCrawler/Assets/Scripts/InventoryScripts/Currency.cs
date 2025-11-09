    using UnityEngine;
    using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "NewCurrency", menuName = "DropableItems/Currency")]
public class Currency : ScriptableObject
    {
        [SerializeField] GameObject currencyPrefab;
        [SerializeField] int amount;

        public GameObject Prefab => currencyPrefab;
        public int Amount => amount;
    }
