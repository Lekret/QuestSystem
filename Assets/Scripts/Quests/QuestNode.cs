using System;
using XNode;

namespace Quests
{
    public abstract class QuestNode : Node, IQuestNode
    {
        [Input(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None)]
        public QuestConn PrevNode;

        [Output(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.None)]
        public QuestConn NextNode;

        public event Action<IQuestNode> Started;
        public event Action<IQuestNode> Completed;

        public virtual IQuestNode GetNextNode()
        {
            return GetOutputPort(nameof(NextNode)).GetConnectedQuestNode();
        }

        public void Initialize()
        {
            OnInitialize();
        }

        public abstract bool IsCompleted();

        public void OnStart()
        {
            OnStarted();
            Started?.Invoke(this);
        }

        public void OnComplete()
        {
            OnCompleted();
            Completed?.Invoke(this);
        }

        protected virtual void OnInitialize()
        {
        }

        protected virtual void OnStarted()
        {
        }

        protected virtual void OnCompleted()
        {
        }

        public override object GetValue(NodePort port) => null;
    }
}