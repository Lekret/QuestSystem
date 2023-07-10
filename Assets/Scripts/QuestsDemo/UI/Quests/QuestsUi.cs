using Quests;
using UnityEngine;

namespace QuestsDemo.UI.Quests
{
    public class QuestsUi : MonoBehaviour
    {
        [SerializeField] private QuestUi _questUiPrefab;

        private QuestRunner _questRunner;
        
        private void Start()
        {
            _questRunner = FindObjectOfType<Character>().QuestRunner;
        }

        private void Update()
        {
            var currentNode = _questRunner.CurrentNode;
        }
    }
}