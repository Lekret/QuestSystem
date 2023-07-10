using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;

namespace Quests
{
    public abstract class QuestGraph : NodeGraph
    {
        private bool _isInstance;
        
        public IQuestNode GetStartNode()
        {
            var rootNode = (QuestRootNode) nodes.Single(n => n is QuestRootNode);
            if (rootNode == null)
            {
                Debug.LogError("QuestGraph has no root node", rootNode);
                return null;
            }
            
            return rootNode.GetStartNode();
        }

        public IQuestNode[] GetQuestNodes()
        {
            return nodes.OfType<IQuestNode>().ToArray();
        }
        
        public void InitializeInstance<T>(T payload)
        {
            _isInstance = true;
            foreach (var node in nodes)
            {
                if (node is IPayloadedQuestNode<T> payloadedQuestNode)
                    payloadedQuestNode.SetPayload(payload);
                
                if (node is IQuestNode questNode)
                    questNode.Initialize();
            }
        }

        public override void Clear()
        {
            if (_isInstance)
                return;
            
            if (Application.isPlaying) {
                for (int i = 0; i < nodes.Count; i++) {
                    if (nodes[i] != null) Destroy(nodes[i]);
                }
            }
            nodes.Clear();
        }
    }
}