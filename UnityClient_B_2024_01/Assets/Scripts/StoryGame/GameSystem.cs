using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using UnityEngine.UI;       //UI
using TMPro;                //TextMeshPro
using STORYGAME;

namespace STORYGAME
{
#if UNITY_EDITOR
    [CustomEditor(typeof(GameSystem))]
    public class GameSystemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GameSystem gameSystem = (GameSystem)target;

            if (GUILayout.Button("Reset Story Models"))
            {
                gameSystem.ResetStoryModels();
            }

        }
    }
#endif

    public class GameSystem : MonoBehaviour
    {
        public static GameSystem Instance;        //Scene ���ο����� ����

        private void Awake()
        {
            Instance = this;
        }

        public enum GAMESTATE
        {
            STORYSHOW,
            STORYEND,
            ENDMODE
        }

        public GAMESTATE currentState;
        public Stats stats;
        public StoryModel[] storyModels;
        public int currentStoryIndex = 0;

        public void ChangeState(GAMESTATE temp)
        {
            currentState = temp;

            if(currentState == GAMESTATE.STORYSHOW)
            {
                StoryShow(currentStoryIndex);
            }
        }

        public void StoryShow(int number)
        {
            StoryModel tempStoryModels = FindStoryModel(number);

            StorySystem.instance.currentStoryModel = tempStoryModels;
            StorySystem.instance.CoShowText();
        }

        public void ApplyChoice(StoryModel.Result result)       //���丮 ���� �� ���
        {
            switch(result.resultType)
            {
                case StoryModel.Result.ResultType.ChangeHP:
                    //GameUI.Instance.UpdateHPUI()      //���߿� �߰�
                    ChangeStats(result);
                    break;

                case StoryModel.Result.ResultType.GoToNextStory:
                    currentStoryIndex = result.value;       //���� �̵� ���丮 ��ȣ�� �޾ƿͼ� ����
                    ChangeState(GAMESTATE.STORYSHOW);
                    ChangeStats(result);
                    break;

                case StoryModel.Result.ResultType.GoToRandomStory:
                    RandomStory();
                    ChangeState(GAMESTATE.STORYSHOW);
                    ChangeStats(result);
                    break;

                default:
                    Debug.LogError("Unknown effect Type");
                    break;
            }
        }

        public void ChangeStats(StoryModel.Result result)           //���� ���� �Լ�
        {
            //�⺻ ����
            if (result.stats.hpPoint > 0) stats.hpPoint += result.stats.hpPoint;
            if (result.stats.spPoint > 0) stats.spPoint += result.stats.spPoint;
            //���� ����
            if (result.stats.currentHPPoint > 0) stats.currentHPPoint += result.stats.currentHPPoint;
            if (result.stats.currentSPPoint > 0) stats.currentSPPoint += result.stats.currentSPPoint;
            if (result.stats.currentXPPoint > 0) stats.currentXPPoint += result.stats.currentXPPoint;
            //�ɷ�ġ ����
            if (result.stats.strength > 0) stats.strength += result.stats.strength;
            if (result.stats.dexterity > 0) stats.dexterity += result.stats.dexterity;
            if (result.stats.consitiution > 0) stats.consitiution += result.stats.consitiution;
            if (result.stats.wisdom > 0) stats.wisdom += result.stats.wisdom;
            if (result.stats.intelligence > 0) stats.intelligence += result.stats.intelligence;
            if (result.stats.charisma > 0) stats.charisma += result.stats.charisma;
        }
        

        StoryModel RandomStory()
        {
            StoryModel tempStoryModels = null;

            List<StoryModel> StoryModelList = new List<StoryModel>();

            for (int i = 0; i < storyModels.Length; i++)
            {
                if (storyModels[i].storyType == StoryModel.STORYTYPE.MAIN)
                {
                    StoryModelList.Add(storyModels[i]);
                }
            }

            tempStoryModels = StoryModelList[Random.Range(0, StoryModelList.Count)];        //MAIN�鸸 �ִ� ����Ʈ���� �������� ���丮 ����
            currentStoryIndex = tempStoryModels.storyNumber;
            Debug.Log("currentStoryIndex" + currentStoryIndex);

            return tempStoryModels;
        }
        StoryModel FindStoryModel(int number)
        {
            StoryModel tempStorymodels = null;
            for (int i = 0; i < storyModels.Length; i++)
            {
                if (storyModels[i].storyNumber == number)
                    {
                    tempStorymodels = storyModels[i];
                    break;
                    }
            
 
            }
            return tempStorymodels;
        }
    



#if UNITY_EDITOR
        [ContextMenu("Reset Story Models")]
        public void ResetStoryModels()
        {
            storyModels = Resources.LoadAll<StoryModel>("");
            //Resources ���� �Ʒ� ��� StoryModel �ҷ�����
        }


#endif
    }
}



