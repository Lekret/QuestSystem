using System;
using XNode;

namespace Quests
{
    [CreateNodeMenu("Quests/Or")]
    public class OrQuestNode : Node, IQuestNode
    {
        [Input(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None)]
        public QuestConn PrevNode;

        [Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None)]
        public QuestConn ChildNodes;

        private IQuestNode _completedNode;
        private IQuestNode[] _questNodes;

        public event Action<IQuestNode> Started;
        public event Action<IQuestNode> Completed;

        public IQuestNode GetNextNode()
        {
            return _completedNode.GetNextNode();
        }

        public void Initialize()
        {
            _questNodes = GetOutputPort(nameof(ChildNodes)).GetConnectedQuestNodes();
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

        public void OnStart()
        {
            Started?.Invoke(this);
            foreach (var node in _questNodes)
            {
                node.OnStart();
            }
        }

        public void OnComplete()
        {
            _completedNode.OnComplete();
            _completedNode = null;
            Completed?.Invoke(this);
        }

        public override object GetValue(NodePort port) => null;
    }
}