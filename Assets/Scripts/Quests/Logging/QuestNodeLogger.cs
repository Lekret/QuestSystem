using UnityEngine;

namespace Quests.Logging
{
    public class QuestNodeLogger
    {
        public virtual void LogStarted(IQuestNode node)
        {
            switch (node)
            {
                case QuestNode questNode:
                    Debug.Log($"Quest started: {questNode.name}");
                    break;
                case AndQuestNode andQuestNode:
                    Debug.Log($"AND Quest started: {andQuestNode.name}");
                    break;
                case OrQuestNode orQuestNode:
                    Debug.Log($"OR Quest started: {orQuestNode.name}");
                    break;
                default:
                    Debug.LogError($"Unhandled QuestNode type: {node.GetType()}");
                    break;
            }
        }

        public virtual void LogCompleted(IQuestNode node)
        {
            switch (node)
            {
                case QuestNode questNode:
                    Debug.Log($"Quest completed: {questNode.name}");
                    break;
                case AndQuestNode andQuestNode:
                    Debug.Log($"AND Quest completed: {andQuestNode.name}");
                    break;
                case OrQuestNode orQuestNode:
                    Debug.Log($"OR Quest completed: {orQuestNode.name}");
                    break;
                default:
                    Debug.LogError($"Unhandled QuestNode type: {node.GetType()}");
                    break;
            }
        }
    }
}