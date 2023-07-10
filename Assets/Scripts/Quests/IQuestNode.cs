using System;

namespace Quests
{
    public interface IQuestNode
    {
        event Action<IQuestNode> Started;
        event Action<IQuestNode> Completed;
        IQuestNode GetNextNode();
        void Initialize();
        bool IsCompleted();
        void OnStart();
        void OnComplete();
    }
}