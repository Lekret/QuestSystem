namespace Quests
{
    public interface IQuestNode
    {
        IQuestNode GetNextNode();
        bool IsCompleted();
        void Initialize();
        void OnStarted();
        void OnCompleted();
    }
}