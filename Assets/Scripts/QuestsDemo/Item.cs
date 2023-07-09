using System;

namespace QuestsDemo
{
    [Serializable]
    public struct Item
    {
        public ItemType Type;
        public int Count;

        public Item(ItemType type, int count)
        {
            Type = type;
            Count = count;
        }
    }
}