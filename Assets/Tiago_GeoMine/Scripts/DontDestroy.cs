using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiago_GeoMine
{
    public class DontDestroy : MonoBehaviour
    {
        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
