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
using Libs.Popups.Controls;
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
            SetAnimation(viewModel.ShowAction, new DoTweenCallbackAnimation(() =>
            {
                return DefaultAnimations.FromLeft(RectTransform, ParentTransform, _showAnimationInfo);
            }));
            
            SetAnimation(viewModel.CloseAction, new DoTweenCallbackAnimation(() =>
            {
                return DefaultAnimations.ToRight(RectTransform, ParentTransform, _closeAnimationInfo);
            }));
            
            SetAnimation(viewModel.BackControlAction, DefaultAnimations.None());
            SetAnimation(viewModel.PackClickedAction, DefaultAnimations.None());
            
            BindToActionWithValue(_backControl, viewModel.BackControlAction, viewModel);
        }
        
         
        private void PacksCollectionOnItemClicked(PackPreview view, PackGameData data)
        {
            if (data.PackPersistentData.isOpened == false)
            {
                return;
            }
            
            BindToAction(view, ViewModel.PackClickedAction);
            SetAnimation(ViewModel.PackClickedAction, new DoTweenSequenceAnimation(s =>
            {
                s.AppendCallback(() => 
                    _energyView.ChangeEnergyAnimate(-data.PackConfiguration.StartLevelEnergy, _energyAnimationTime));
                s.AppendInterval(_energyAnimationTime);
                s.Append(DefaultAnimations.ToRight(view.RectTransform, RectTransform, _packClickAnimationInfo));
            }));
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
                Width = (1 - 2 * _marginSide) * _viewportResizer.Width
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