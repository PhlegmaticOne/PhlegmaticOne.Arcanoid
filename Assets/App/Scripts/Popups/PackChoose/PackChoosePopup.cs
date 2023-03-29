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
using Common.ServiceInstallers;
using Common.Packs.Views.Views;
using Libs.Localization;
using UnityEngine;

namespace Common.Packs.Views
{
    public class PackChoosePopup : ViewModelPopup<PackChoosePopupViewModel>, ILocalizable
    {
        [SerializeField] private LocalizationComponent _localizationComponent;
        [SerializeField] private PackChoosePopupAnimationConfiguration _animationConfiguration;
        [SerializeField] private RectTransform _itemsTransform;
        [SerializeField] private ButtonControl _backControl;
        [SerializeField] private EnergyView _energyView;
        
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
            _packsCollection = CreateCollection(packRepository.GetAll().Reverse());
            _localizationComponent.BindInitial(localizationManager);
            _localizationComponent.AddNew(this);
            _localizationComponent.Refresh();
        }
        
        protected override void SetupViewModel(PackChoosePopupViewModel viewModel)
        {
            ViewModel.CloseAction = PopupAction.Empty;
            
            SetAnimation(viewModel.ShowAction, Animate.RectTransform(RectTransform)
                .RelativeTo(ParentTransform)
                .Appear(_animationConfiguration.ShowAnimation)
                .ToPopupCallbackAnimation());
            SetAnimation(viewModel.CloseAction, Animate.RectTransform(RectTransform)
                .RelativeTo(ParentTransform)
                .Disappear(_animationConfiguration.CloseAnimation)
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

            if (ViewModel.PackClickedAction.CanExecute(data))
            {
                SetAnimation(ViewModel.PackClickedAction, new DoTweenSequenceAnimation(s =>
                {
                    var energyToChange = -data.PackConfiguration.StartLevelEnergy;
                
                    _energyView.AppendAnimationToSequence(s, energyToChange, _animationConfiguration.EnergyAnimationTime);
                    s.Append(Animate.RectTransform(view.RectTransform)
                        .RelativeTo(RectTransform)
                        .Disappear(_animationConfiguration.PackClickedAnimation));
                }, () => RectTransform.DOKill()));
                
                BindToAction(view, ViewModel.PackClickedAction);
            }
            
            ViewModel.PackClickedAction.Execute(data);
        }

        public IEnumerable<ILocalizationBindable> GetBindableComponents() => 
            _packsCollection.Views.Select(x => x.PackNameTextBindableComponent);

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
            _localizationComponent.Unbind();
            _energyController.Disable();
            _packsCollection.ItemClicked -= PacksCollectionOnItemClicked;
            _packsCollection.Clear();
            _backControl.Reset();
            Unbind(ViewModel.BackControlAction);
            Unbind(ViewModel.PackClickedAction);
        }

        private ObservableCollection<PackGameData, PackPreview> CreateCollection(IEnumerable<PackGameData> items)
        {
            var context = new PackPreviewCreationContext { Transform = _itemsTransform, };
            var collection = new ObservableCollection<PackGameData, PackPreview>(
                bindingFunc: model => _packPreviewFactory.CreatePackPreview(model, context), 
                isInteractableBinding: i => i.PackPersistentData.isOpened,
                destroyAction: d => Destroy(d.gameObject));
            collection.AddRange(items);
            collection.ItemClicked += PacksCollectionOnItemClicked;
            return collection;
        }
    }
}