using System;
using Quests;
using Quests.Logging;
using UnityEngine;

namespace QuestsDemo
{
    [RequireComponent(typeof(Inventory))]
    public class Character : MonoBehaviour, IQuestCharacter
    {
        [SerializeField] private CharacterQuestGraph _startGraph;

        private QuestNodeListener _questNodeListener;

        public Inventory Inventory { get; private set; }
        public QuestRunner QuestRunner { get; private set; }

        private void Awake()
        {
            Inventory = GetComponent<Inventory>();
            QuestRunner = new QuestRunner(_startGraph);
            _questNodeListener = new QuestNodeListener(QuestRunner.GetQuestNodes(), new QuestNodeLogger());
            _questNodeListener.Init();
            QuestRunner.Init<IQuestCharacter>(this);
        }

        private void OnDestroy()
        {
            _questNodeListener.Dispose();
        }

        private void Update()
        {
            QuestRunner.Tick();
        }
    }
}