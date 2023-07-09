using XNode;

namespace Quests
{
    [DisallowMultipleNodes]
    [CreateNodeMenu("Quests/Root")]
    public class QuestRootNode : Node
    {
        [Output(ShowBackingValue.Never, ConnectionType.Override)]
        public QuestConn StartQuest;

        public IQuestNode GetStartNode()
        {
            var outputPort = GetOutputPort(nameof(StartQuest));
            if (outputPort.ConnectionCount > 0)
                return outputPort.GetConnection(0).node as IQuestNode;
            return null;
        }

        public override object GetValue(NodePort port) => null;
    }
}