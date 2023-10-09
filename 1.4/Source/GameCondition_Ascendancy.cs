using Verse;

namespace Ascendancy
{
    public class GameCondition_Ascendancy : GameComponent
    {
        public int lastTimeWindWaveApplied;
        public int windWaveAppliedCount;

        public static GameCondition_Ascendancy Instance;

        public GameCondition_Ascendancy()
        {
            Instance = this;
        }

        public GameCondition_Ascendancy(Game game)
        {
            Instance = this;
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref lastTimeWindWaveApplied, nameof(lastTimeWindWaveApplied));
            Scribe_Values.Look(ref windWaveAppliedCount, nameof(windWaveAppliedCount));
        }
    }
}
