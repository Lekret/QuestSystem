using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
using UnityEngine.Events;

namespace QuestsDemo
{
    public class Inventory : MonoBehaviour
    {
        private readonly List<Item> _items = new();

        public event UnityAction ItemsChanged;

        public IEnumerable<Item> Items => _items;

        public void AddItem(ItemType itemType, int itemCount)
        {
            var resourceMerged = false;

            for (var i = 0; i < _items.Count; i++)
            {
                var resource = _items[i];
                if (resource.Type == itemType)
                {
                    resource.Count += itemCount;
                    _items[i] = resource;
                    resourceMerged = true;
                    break;
                }
            }

            if (!resourceMerged)
                _items.Add(new Item(itemType, itemCount));

            ItemsChanged?.Invoke();
        }

        public Item? ReadItem(ItemType itemType)
        {
            var itemIdx = _items.FindIndex(x => x.Type == itemType);
            if (itemIdx >= 0)
                return _items[itemIdx];
            return null;
        }
        
        #if UNITY_EDITOR
        [Header("Editor")]
        [SerializeField] private ItemType _editorItemType;
        [SerializeField] private int _editorItemCount = 1;

        [Button]
        private void EditorAddItem()
        {
            AddItem(_editorItemType, _editorItemCount);
        }
        #endif
    }
}