using Sirenix.OdinInspector;
using UnityEngine;

namespace DataConfigs
{
    [CreateAssetMenu(fileName = "GameLoop", menuName = "Configs/GameLoop", order = 0)]
    public sealed class GameLoop : ScriptableObject
    {
        [FoldoutGroup("Cycle Timer")] public float wheatHarvestCycle;
        [FoldoutGroup("Cycle Timer")] public float attackCycle;
        [FoldoutGroup("Cycle Timer")] public float eatsCycle;

        [FoldoutGroup("Win condition")] public int countWarriorsToWin;
        [FoldoutGroup("Win condition")] public int countPeasantsToWin;
        
        [FoldoutGroup("Start Stats")] public int startCountPeasant;
        [FoldoutGroup("Start Stats")] public int startCountWarriorsOne;
        [FoldoutGroup("Start Stats")] public int startCountWarriorsTwo;
        [FoldoutGroup("Start Stats")] public int startCountWheat;

        [TabGroup("Peasant Property"), PreviewField(150)] public Texture2D peasantImage;
        [TabGroup("Peasant Property")] public int peasantGivesAmountWheat;
        [TabGroup("Peasant Property")] public int peasantEatsWheat;
        [TabGroup("Peasant Property")] public float timeToGetNewPeasant;
        [TabGroup("Peasant Property")] public int currencyNewPeasant;

        [TabGroup("Warrior Property One"), PreviewField(150)] public Texture2D warriorOneImage;
        [TabGroup("Warrior Property One")] public int warriorOneEatsWheat;
        [TabGroup("Warrior Property One")] public float timeToGetNewWarriorOne;
        [TabGroup("Warrior Property One")] public int currencyNewWarriorOne;

        [TabGroup("Warrior Property Two"), PreviewField(150)] public Texture2D warriorTwoImage;
        [TabGroup("Warrior Property Two")] public int warriorTwoEatsWheat;
        [TabGroup("Warrior Property Two")] public float timeToGetNewWarriorTwo;
        [TabGroup("Warrior Property Two")] public int currencyNewWarriorTwo;

        [TitleGroup("Enemy", alignment: TitleAlignments.Centered, boldTitle: true)]
        public int addedCountEnemyNextWave;
    }
}