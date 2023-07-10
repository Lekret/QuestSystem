using System.Linq;
using XNode;

namespace Quests
{
    public static class QuestNodeUtils
    {
        public static IQuestNode GetConnectedQuestNode(this NodePort nodePort)
        {
            if (nodePort.ConnectionCount > 0)
                return nodePort.GetConnection(0).node as IQuestNode;
            return null;
        }

        public static IQuestNode[] GetConnectedQuestNodes(this NodePort nodePort)
        {
            return nodePort.GetConnections()
                .Select(n => n.node)
                .OfType<IQuestNode>()
                .ToArray();
        }
    }
}