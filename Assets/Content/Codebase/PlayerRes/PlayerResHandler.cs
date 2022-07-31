using System;
using Zenject;

namespace Woodman.PlayerRes
{
    public class PlayerResHandler : IInitializable, IDisposable
    {
        private readonly PlayerResRepository _resRepository;
        private readonly MetaUiProvider _uiProvider;

        public PlayerResHandler(PlayerResRepository resRepository, MetaUiProvider uiProvider)
        {
            _resRepository = resRepository;
            _uiProvider = uiProvider;
        }

        public void Initialize()
        {
            _resRepository.OnChange += OnResChanges;
            _uiProvider.ResourceBarMetaUI.SetCount(_resRepository.GetPlayerRes());
        }

        private void OnResChanges(int arg1, int arg2)
        {
            //todo: устанавливать с анимацией
            _uiProvider.ResourceBarMetaUI.SetCount(arg2);
        }

        public void Dispose()
        {
            _resRepository.OnChange -= OnResChanges;
        }
    }
}