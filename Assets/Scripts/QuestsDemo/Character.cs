using Quests;
using UnityEngine;

namespace QuestsDemo
{
    [RequireComponent(typeof(Inventory))]
    public class Character : MonoBehaviour, IQuestCharacter
    {
        [SerializeField] private CharacterQuestGraph _startGraph;
        
        private Inventory _inventory;
        private QuestRunner _questRunner;

        private void Awake()
        {
            _inventory = GetComponent<Inventory>();
            _questRunner = new QuestRunner();
            _questRunner.Init<IQuestCharacter>(_startGraph, this);
        }

        private void Update()
        {
            _questRunner.Tick();
        }

        public Inventory GetInventory()
        {
            return _inventory;
        }
    }
}