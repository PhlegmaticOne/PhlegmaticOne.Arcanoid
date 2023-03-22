using Common.Packs.Data.Models;
using UnityEngine;

namespace Popups.PackChoose.Views.Factory
{
    public interface IPackPreviewFactory
    {
        PackPreview CreatePackPreview(PackGameData packGameData, PackPreviewCreationContext context);
    }

    public class PackPreviewCreationContext
    {
        public float Width { get; set; }
        public Transform Transform { get; set; }
    }
}