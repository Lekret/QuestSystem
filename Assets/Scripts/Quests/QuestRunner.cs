using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    public class QuestRunner
    {
        private readonly QuestGraph _questGraph;
        private IQuestNode _currentNode;
        public IQuestNode CurrentNode => _currentNode;
        
        public IQuestNode[] GetQuestNodes() => _questGraph.GetQuestNodes();

        public QuestRunner(QuestGraph questGraphTemplate)
        {
            _questGraph = Object.Instantiate(questGraphTemplate);
        }

        public void Init<TPayload>(TPayload payload)
        {
            _questGraph.InitializeInstance(payload);
            _currentNode = _questGraph.GetStartNode();
            if (_currentNode != null)
            {
                _currentNode.OnStart();
            }
            else
            {
                Debug.LogError("StartNode is null", _questGraph);
            }
        }

        public void Tick()
        {
            if (_currentNode == null)
                return;

            if (!_currentNode.IsCompleted())
                return;
            
            _currentNode.OnComplete();
            _currentNode = _currentNode.GetNextNode();
            if (_currentNode != null)
            {
                _currentNode.OnStart();
            }
            else
            {
                Debug.Log($"Quest graph completed: {_questGraph.name}", _questGraph);
            }
        }
    }
}