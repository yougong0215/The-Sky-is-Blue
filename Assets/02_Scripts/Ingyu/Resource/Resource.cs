using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingyu
{
    [CreateAssetMenu(menuName = "Resource")]
    public class Resource : ScriptableObject
    {
        public int water;
        public int food;
        public int wood;
        public int stone;
        public int iron;
        public int magicStone;
        public int coreStone;
    }
}
