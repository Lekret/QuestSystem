using UnityEngine;

namespace QuestsDemo.UI.Inventory
{
    public class InventoryUi : MonoBehaviour
    {
        [SerializeField] private ItemUi _itemUiPrefab;
        [SerializeField] private RectTransform _itemsRoot;
        
        private QuestsDemo.Inventory _inventory;

        private void Start()
        {
            _inventory = FindObjectOfType<Character>().Inventory;
            _inventory.ItemsChanged += OnInventoryItemsChanged;
            OnInventoryItemsChanged();
        }

        private void OnDestroy()
        {
            if (_inventory)
                _inventory.ItemsChanged -= OnInventoryItemsChanged;
        }

        private void OnInventoryItemsChanged()
        {
            for (var i = _itemsRoot.childCount - 1; i >= 0; i--)
            {
                Destroy(_itemsRoot.GetChild(i).gameObject);
            }
   
            foreach (var item in _inventory.Items)
            {
                var itemUiInstance = Instantiate(_itemUiPrefab, _itemsRoot);
                itemUiInstance.SetItem(item);
            }
        }
    }
}