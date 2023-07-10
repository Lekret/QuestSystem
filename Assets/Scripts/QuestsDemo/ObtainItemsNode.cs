using Quests;

namespace QuestsDemo
{
    [CreateNodeMenu("Quests/Custom/ObtainItems")]
    public class ObtainItemsNode : QuestNode, IPayloadedQuestNode<IQuestCharacter>
    {
        public ItemType ItemType;
        public int ItemCount;

        private IQuestCharacter _character;

        public void SetPayload(IQuestCharacter payload)
        {
            _character = payload;
        }
        
        public override bool IsCompleted()
        {
            var item = _character.Inventory.ReadItem(ItemType);
            return item.HasValue && item.Value.Count >= ItemCount;
        }
    }
}