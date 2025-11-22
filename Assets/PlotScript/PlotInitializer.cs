using System.Collections.Generic;
using UnityEngine;

public class PlotInitializer : MonoBehaviour
{
    public PlotSO plotSO;

    void Start()
    {
        plotSO.plotList.Clear();

        plotSO.plotList.Add(new Augment
        {
            plotID = "001",
            plotName = "한 숨 돌리기",
            plotDesc = "푸른 풀밭에 나를 쉬게 하시고 \n[시 23:2]",
            plotEffect = "즉시 체력 20 회복",

            plotCondition = new List<PlotCondition>
            {
                new PlotCondition { statType = "influence", compareType = ">=", value = 15 }
            },

            trigger = TriggerType.OnSelect,

            plotTarget = TargetType.Self,
            targetStat = "hp",

            pietyCost = 20,

            hpChangeType = ChangeType.Increase,
            hpChange = 20,

            influenceChangeType = ChangeType.Increase,
            influenceChange = 0,

            pietyChangeType = ChangeType.Increase,
            pietyChange = 0,

            plotWeight = 50
        });

        plotSO.plotList.Add(new Augment
        {
            plotID = "002",
            plotName = "푹 쉬기",
            plotDesc = "제가 비록 어둠의 골짜기를 간다 하여도 재앙을 두려워하지 않으리니 \n[시 23:4]",
            plotEffect = "즉시 체력 20 회복",

            plotCondition = new List<PlotCondition>
            {
                new PlotCondition { statType = "influence", compareType = ">=", value = 40 }
            },

            trigger = TriggerType.OnSelect,

            plotTarget = TargetType.Self,
            targetStat = "hp",

            pietyCost = 35,

            hpChangeType = ChangeType.Increase,
            hpChange = 20,

            influenceChangeType = ChangeType.Increase,
            influenceChange = 0,

            pietyChangeType = ChangeType.Increase,
            pietyChange = 0,

            plotWeight = 20
        });

        plotSO.plotList.Add(new Augment
        {
            plotID = "003",
            plotName = "은밀한 논의",
            plotDesc = "...점심 뭐 먹지?",
            plotEffect = "즉시 정치력 15 회복",

            plotCondition = new List<PlotCondition>
            {
                new PlotCondition { statType = "influence", compareType = "<=", value = 30 }
            },

            trigger = TriggerType.OnSelect,

            plotTarget = TargetType.Self,
            targetStat = "hp",

            pietyCost = 20,

            hpChangeType = ChangeType.Increase,
            hpChange = 0,

            influenceChangeType = ChangeType.Increase,
            influenceChange = 15,

            pietyChangeType = ChangeType.Increase,
            pietyChange = 0,

            plotWeight = 50
        });

        plotSO.plotList.Add(new Augment
        {
            plotID = "004",
            plotName = "매우 심도 있는 논의",
            plotDesc = "파인애플 피자는 이단인가?",
            plotEffect = "즉시 정치력 15 회복",

            plotCondition = new List<PlotCondition>
            {
                new PlotCondition { statType = "influence", compareType = ">=", value = 0 }
            },

            trigger = TriggerType.OnSelect,

            plotTarget = TargetType.Self,
            targetStat = "hp",

            pietyCost = 20,

            hpChangeType = ChangeType.Increase,
            hpChange = 0,

            influenceChangeType = ChangeType.Increase,
            influenceChange = 15,

            pietyChangeType = ChangeType.Increase,
            pietyChange = 0,

            plotWeight = 20
        });

        plotSO.plotList.Add(new Augment
        {
            plotID = "005",
            plotName = "삼위일체",
            plotDesc = "성부와 성자와 성령의 이름으로",
            plotEffect = "체력 33, 정치력 3, 경건함 3 증가",

            plotCondition = new List<PlotCondition>
            {
                new PlotCondition { statType = "hp", compareType = ">=", value = 33 },
                new PlotCondition { statType = "influence", compareType = ">=", value = 33 },
                new PlotCondition { statType = "piety", compareType = ">=", value = 33 }
            },

            trigger = TriggerType.OnSelect,

            plotTarget = TargetType.Self,
            targetStat = "hp",

            pietyCost = 0,

            hpChangeType = ChangeType.Increase,
            hpChange = 33,

            influenceChangeType = ChangeType.Increase,
            influenceChange = 3,

            pietyChangeType = ChangeType.Increase,
            pietyChange = 3,

            plotWeight = 50
        });

        plotSO.plotList.Add(new Augment
        {
            plotID = "006",
            plotName = "삼위일체?",
            plotDesc = "트포다 트포",
            plotEffect = "체력, 정치력, 경건함 33 감소",

            plotCondition = new List<PlotCondition>
            {
                new PlotCondition { statType = "hp", compareType = ">=", value = 33 },
                new PlotCondition { statType = "influence", compareType = ">=", value = 33 },
                new PlotCondition { statType = "piety", compareType = ">=", value = 33 }
            },

            trigger = TriggerType.OnSelect,

            plotTarget = TargetType.Self,
            targetStat = "hp",

            pietyCost = 0,

            hpChangeType = ChangeType.Decrease,
            hpChange = 33,

            influenceChangeType = ChangeType.Decrease,
            influenceChange = 33,

            pietyChangeType = ChangeType.Decrease,
            pietyChange = 33,

            plotWeight = 20
        });

        plotSO.plotList.Add(new Augment
        {
            plotID = "007",
            plotName = "멍청한 적 하기",
            plotDesc = "저는 예수님을 몰라요. 세 번 몰라요.",
            plotEffect = "정치력 10 감소",

            plotCondition = new List<PlotCondition>
            {
                new PlotCondition { statType = "influence", compareType = ">=", value = 60 }
            },

            trigger = TriggerType.OnSelect,

            plotTarget = TargetType.Self,
            targetStat = "hp",

            pietyCost = 15,

            hpChangeType = ChangeType.Increase,
            hpChange = 0,

            influenceChangeType = ChangeType.Decrease,
            influenceChange = 10,

            pietyChangeType = ChangeType.Increase,
            pietyChange = 0,

            plotWeight = 50
        });

        plotSO.plotList.Add(new Augment
        {
            plotID = "008",
            plotName = "깡!",
            plotDesc = "깡!",
            plotEffect = "가장 경건함이 낮은 상대 체력 40 감소",

            plotCondition = new List<PlotCondition>
            {
                new PlotCondition { statType = "influence", compareType = ">=", value = 50 }
            },

            trigger = TriggerType.OnSelect,

            plotTarget = TargetType.LowestStatCardinal,
            targetStat = "piety",

            pietyCost = 50,

            hpChangeType = ChangeType.Decrease,
            hpChange = 40,

            influenceChangeType = ChangeType.Increase,
            influenceChange = 0,

            pietyChangeType = ChangeType.Increase,
            pietyChange = 0,

            plotWeight = 20
        });

        plotSO.plotList.Add(new Augment
        {
            plotID = "009",
            plotName = "똑똑한 척 하기",
            plotDesc = "",
            plotEffect = "효과가 없는 것 같다...",

            plotCondition = new List<PlotCondition>
            {
                new PlotCondition { statType = "influence", compareType = ">=", value = 50 }
            },

            trigger = TriggerType.OnSelect,

            plotTarget = TargetType.Self,
            targetStat = "hp",

            pietyCost = 70,

            hpChangeType = ChangeType.Increase,
            hpChange = 0,

            influenceChangeType = ChangeType.Increase,
            influenceChange = 0,

            pietyChangeType = ChangeType.Increase,
            pietyChange = 0,

            plotWeight = 5
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
