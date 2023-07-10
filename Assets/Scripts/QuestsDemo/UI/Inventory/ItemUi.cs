using TMPro;
using UnityEngine;

namespace QuestsDemo.UI.Inventory
{
    public class ItemUi : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void SetItem(Item item)
        {
            _text.text = $"{item.Type} ({item.Count})";
        }
    }
}