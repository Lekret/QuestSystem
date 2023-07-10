using System;
using XNode;

namespace Quests
{
    [CreateNodeMenu("Quests/And")]
    public class AndQuestNode : Node, IQuestNode
    {
        [Input(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None)]
        public QuestConn PrevNode;

        [Output(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.None)]
        public QuestConn NextNode;

        [Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None)]
        public QuestConn ChildNodes;

        private IQuestNode[] _questNodes;

        public event Action<IQuestNode> Started;
        public event Action<IQuestNode> Completed;

        public IQuestNode GetNextNode()
        {
            return GetOutputPort(nameof(NextNode)).GetConnectedQuestNode();
        }

        public void Initialize()
        {
            _questNodes = GetOutputPort(nameof(ChildNodes)).GetConnectedQuestNodes();
        }

        public bool IsCompleted()
        {
            foreach (var node in _questNodes)
            {
                if (!node.IsCompleted())
                    return false;
            }

            return true;
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
            foreach (var node in _questNodes)
            {
                node.OnComplete();
            }

            Completed?.Invoke(this);
        }
    }
}