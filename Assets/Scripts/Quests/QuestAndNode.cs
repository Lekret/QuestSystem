using System.Linq;

namespace Quests
{
    [CreateNodeMenu("Quests/And")]
    public class QuestAndNode : QuestNode
    {
        [Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None)]
        public QuestConn AggregatedNodes;

        private IQuestNode[] _questNodes;

        public override bool IsCompleted()
        {
            foreach (var node in _questNodes)
            {
                if (!node.IsCompleted())
                    return false;
            }

            return true;
        }

        public override void Initialize()
        {
            _questNodes = GetOutputPort(nameof(AggregatedNodes))
                .GetConnections()
                .Where(n => n.node is IQuestNode)
                .Select(n => n.node as IQuestNode)
                .ToArray();
        }

        public override void OnStarted()
        {
            base.OnStarted();
            foreach (var node in _questNodes)
            {
                node.OnStarted();
            }
        }

        public override void OnCompleted()
        {
            foreach (var node in _questNodes)
            {
                node.OnCompleted();
            }

            base.OnCompleted();
        }
    }
}