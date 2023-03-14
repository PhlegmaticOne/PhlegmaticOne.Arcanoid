using System;
using UnityEngine;

namespace Common.Data.Models
{
    [Serializable]
    public class PackPreviewData
    {
        [SerializeField] public string name;
        [SerializeField] public int levelsCount;
        [SerializeField] public int startLevelId;
    }
}