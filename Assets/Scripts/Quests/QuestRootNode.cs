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
            return GetOutputPort(nameof(StartQuest)).GetConnectedQuestNode();
        }

        public override object GetValue(NodePort port) => null;
    }
}