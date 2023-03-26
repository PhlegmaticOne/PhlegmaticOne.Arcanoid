using Common.Packs.Data.Models;
using Common.Packs.Views.Views;
using UnityEngine;

namespace Common.ServiceInstallers
{
    public interface IPackPreviewFactory
    {
        PackPreview CreatePackPreview(PackGameData packGameData, PackPreviewCreationContext context);
    }

    public class PackPreviewCreationContext
    {
        public Transform Transform { get; set; }
    }
}