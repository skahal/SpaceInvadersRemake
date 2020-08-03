using UnityEngine;

namespace Skahal.SpaceInvadersRemake
{
    [CreateAssetMenu(menuName = "Skahal/SpaceInvadersRemake/AliensWaveKind")]
    public class AliensWaveKind : ScriptableObject
    {
        public int ForAliensAlive;
        public float Delay = 1.5f;
    }
}
