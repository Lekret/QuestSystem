using System.Linq;
using UnityEngine;
using XNode;

namespace Quests
{
    [CreateNodeMenu("Quests/Or")]
    public class QuestOrNode : Node, IQuestNode
    {
        [Input(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None)]
        public QuestConn PrevNode;
        
        [Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None)]
        public QuestConn AggregatedNodes;

        private IQuestNode _completedNode;
        private IQuestNode[] _questNodes;

        public IQuestNode GetNextNode()
        {
            return _completedNode.GetNextNode();
        }

        public bool IsCompleted()
        {
            foreach (var node in _questNodes)
            {
                if (node.IsCompleted())
                {
                    _completedNode = node;
                    return true;
                }
            }

            return false;
        }

        public void Initialize()
        {
            _questNodes = GetOutputPort(nameof(AggregatedNodes))
                .GetConnections()
                .Where(n => n.node is IQuestNode)
                .Select(n => n.node as IQuestNode)
                .ToArray();
        }

        public void OnStarted()
        {
            Debug.Log($"Quest started: {name}", this);
            foreach (var node in _questNodes)
            {
                node.OnStarted();
            }
        }

        public void OnCompleted()
        {
            _completedNode.OnCompleted();
            _completedNode = null;
            Debug.Log($"Quest completed: {name}", this);
        }

        public override object GetValue(NodePort port) => null;
    }
}