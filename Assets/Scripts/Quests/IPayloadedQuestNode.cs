namespace Quests
{
    public interface IPayloadedQuestNode<in TPayload>
    {
        void SetPayload(TPayload payload);
    }
}