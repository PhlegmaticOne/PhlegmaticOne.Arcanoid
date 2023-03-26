using System.Collections.Generic;
using System.Linq;
using Common.Energy;
using Common.Packs.Data.Models;
using Common.Packs.Data.Repositories.Base;
using Common.Positioning;
using DG.Tweening;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Popups;
using Libs.Popups.Animations;
using Libs.Popups.Animations.Concrete;
using Libs.Popups.Animations.Extensions;
using Libs.Popups.Animations.Info;
using Libs.Popups.Controls;
using Libs.Popups.ViewModels.Actions;
using Libs.Popups.ViewModels.Collections;
using Popups.PackChoose.Views;
using Popups.PackChoose.Views.Factory;
using UnityEngine;

namespace Popups.PackChoose
{
    public class PackChoosePopup : ViewModelPopup<PackChoosePopupViewModel>, ILocalizable
    {
        [SerializeField] private RectTransform _itemsTransform;
        [SerializeField] private ViewportResizer _viewportResizer;
        [SerializeField] [Range(0f, 1f)] private float _marginSide;
        
        [SerializeField] private ButtonControl _backControl;
        [SerializeField] private EnergyView _energyView;
        [SerializeField] private List<LocalizationBindableComponent> _bindableComponents;

        [SerializeField] private TweenAnimationInfo _showAnimationInfo;
        [SerializeField] private TweenAnimationInfo _closeAnimationInfo;
        [SerializeField] private TweenAnimationInfo _packClickAnimationInfo;
        [SerializeField] private float _energyAnimationTime;
        
        private LocalizationContext _localizationContext;
        private IPackPreviewFactory _packPreviewFactory;
        private EnergyController _energyController;
        private ObservableCollection<PackGameData, PackPreview> _packsCollection;


        [PopupConstructor]
        public void Initialize(ILocalizationManager localizationManager,
            IPackPreviewFactory packPreviewFactory,
            IPackRepository packRepository, 
            EnergyManager energyManager)
        {
            _packPreviewFactory = packPreviewFactory;
            _energyController = new EnergyController(energyManager, _energyView);

            var packConfigurations = packRepository.GetAll().Reverse();
            _packsCollection = CreateCollection();
            _packsCollection.AddRange(packConfigurations);
            
            _localizationContext = LocalizationContext
                .Create(localizationManager)
                .BindLocalizable(this)
                .Refresh();
        }
        
        protected override void SetupViewModel(PackChoosePopupViewModel viewModel)
        {
            ViewModel.CloseAction = PopupAction.Empty;
            
            SetAnimation(viewModel.ShowAction, Animate.RectTransform(RectTransform)
                .RelativeTo(ParentTransform)
                .FromLeft(_showAnimationInfo)
                .ToPopupCallbackAnimation());
            SetAnimation(viewModel.CloseAction, Animate.RectTransform(RectTransform)
                .RelativeTo(ParentTransform)
                .ToRight(_closeAnimationInfo)
                .ToPopupCallbackAnimation());
            SetAnimation(viewModel.BackControlAction, Animate.None());
            SetAnimation(viewModel.PackClickedAction, Animate.None());
            
            BindToActionWithValue(_backControl, viewModel.BackControlAction, viewModel);
        }
        
         
        private void PacksCollectionOnItemClicked(PackPreview view, PackGameData data)
        {
            if (data.PackPersistentData.isOpened == false)
            {
                return;
            }
            
            SetAnimation(ViewModel.PackClickedAction, new DoTweenSequenceAnimation(s =>
            {
                var energyToChange = -data.PackConfiguration.StartLevelEnergy;
                
                _energyView.AppendAnimationToSequence(s, energyToChange, _energyAnimationTime);
                s.Append(Animate.RectTransform(view.RectTransform)
                    .RelativeTo(RectTransform)
                    .ToRight(_packClickAnimationInfo));
            }));
            
            BindToAction(view, ViewModel.PackClickedAction);
            ViewModel.PackClickedAction.Execute(data);
        }

        public IEnumerable<ILocalizationBindable> GetBindableComponents()
        {
            var views = _packsCollection.Views.Select(x => x.PackNameTextBindableComponent);
            return _bindableComponents.Concat(views);
        }

        public override void EnableInput()
        {
            _backControl.Enable();
            _packsCollection.Enable();
        }
        
        public override void DisableInput()
        {
            _backControl.Disable();
            _packsCollection.Disable();
        }

        public override void Reset()
        {
            ToZeroPosition();
            _localizationContext.Flush();
            _energyController.Disable();
            _packsCollection.ItemClicked -= PacksCollectionOnItemClicked;
            _packsCollection.Clear();
            _backControl.Reset();
            Unbind(ViewModel.BackControlAction);
            Unbind(ViewModel.PackClickedAction);
        }

        private ObservableCollection<PackGameData, PackPreview> CreateCollection()
        {
            var context = new PackPreviewCreationContext
            {
                Transform = _itemsTransform,
            };
            var collection = new ObservableCollection<PackGameData, PackPreview>(
                bindingFunc: model => _packPreviewFactory.CreatePackPreview(model, context), 
                isInteractableBinding: i => i.PackPersistentData.isOpened,
                destroyAction: d => Destroy(d.gameObject));
            collection.ItemClicked += PacksCollectionOnItemClicked;
            return collection;
        }
    }
}