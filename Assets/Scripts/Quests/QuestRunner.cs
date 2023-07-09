using UnityEngine;

namespace Quests
{
    public class QuestRunner
    {
        private QuestGraph _questGraph;
        private IQuestNode _currentNode;
        
        public void Init<T>(QuestGraph questGraph, T payload)
        {
            _questGraph = Object.Instantiate(questGraph);
            _questGraph.InitializeInstance(payload);
            var rootNode = questGraph.GetRoot();
            _currentNode = rootNode.GetStartNode();
            if (_currentNode != null)
            {
                _currentNode.OnStarted();
            }
            else
            {
                Debug.LogError("Root node has no start node", rootNode);
            }
        }

        public void Tick()
        {
            if (_currentNode == null)
                return;

            if (!_currentNode.IsCompleted())
                return;
            
            _currentNode.OnCompleted();
            _currentNode = _currentNode.GetNextNode();
            if (_currentNode != null)
            {
                _currentNode.OnStarted();
            }
            else
            {
                Debug.Log($"Quest graph completed: {_questGraph.name}", _questGraph);
            }
        }
    }
}