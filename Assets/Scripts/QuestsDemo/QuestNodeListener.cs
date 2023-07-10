using System;
using Quests;
using Quests.Logging;
using Object = UnityEngine.Object;

namespace QuestsDemo
{
    public class QuestNodeListener : IDisposable
    {
        private readonly IQuestNode[] _nodes;
        private readonly QuestNodeLogger _logger;

        public QuestNodeListener(IQuestNode[] nodes, QuestNodeLogger logger)
        {
            _nodes = nodes;
            _logger = logger;
        }

        public void Init()
        {
            foreach (var node in _nodes)
            {
                node.Started += OnNodeStarted;
                node.Completed += OnNodeCompleted;
            }
        }

        public void Dispose()
        {
            foreach (var node in _nodes)
            {
                if (node is Object obj && obj || node != null)
                {
                    node.Started -= OnNodeStarted;
                    node.Completed -= OnNodeCompleted;
                }
            }
        }

        private void OnNodeStarted(IQuestNode node)
        {
            _logger.LogStarted(node);
        }

        private void OnNodeCompleted(IQuestNode node)
        {
            _logger.LogCompleted(node);
        }
    }
}