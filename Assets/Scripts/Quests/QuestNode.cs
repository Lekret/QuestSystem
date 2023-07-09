using UnityEngine;
using XNode;

namespace Quests
{
    public abstract class QuestNode : Node, IQuestNode
    {
        [Input(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None)]
        public QuestConn PrevNode;

        [Output(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.None)]
        public QuestConn NextNode;

        public virtual IQuestNode GetNextNode()
        {
            var outputPort = GetOutputPort(nameof(NextNode));
            if (outputPort.ConnectionCount > 0)
                return outputPort.GetConnection(0).node as IQuestNode;
            return null;
        }

        public abstract bool IsCompleted();
        
        public virtual void Initialize()
        {
        }

        public virtual void OnStarted()
        {
            Debug.Log($"Quest started: {name}", this);
        }

        public virtual void OnCompleted()
        {
            Debug.Log($"Quest completed: {name}", this);
        }
        
        public override object GetValue(NodePort port) => null;
    }
}