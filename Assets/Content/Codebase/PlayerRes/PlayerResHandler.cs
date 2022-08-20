using System;
using Woodman.Common;
using Zenject;

namespace Woodman.PlayerRes
{
    public class PlayerResHandler : IInitializable, IDisposable
    {
        private readonly ResourceBarMetaUI _resourceBar;
        private readonly PlayerResRepository _resRepository;

        public PlayerResHandler(PlayerResRepository resRepository, MainViewProvider uiProvider)
        {
            _resRepository = resRepository;
            _resourceBar = uiProvider.MetaUiProvider.ResourceBarMetaUI;
        }

        public void Dispose()
        {
            _resRepository.OnChange -= OnResChanges;
        }

        public void Initialize()
        {
            _resRepository.OnChange += OnResChanges;
            _resourceBar.SetCount(_resRepository.GetPlayerRes());
        }

        private void OnResChanges(int arg1, int arg2)
        {
            //todo: устанавливать с анимацией
            _resourceBar.SetCount(arg2);
        }
    }
}