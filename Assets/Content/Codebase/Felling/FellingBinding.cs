using Zenject;

namespace Woodman.Felling
{
    public class FellingBinding : IInitializable
    {
        private readonly TapController _tapController;
        private readonly Cutter _cutter;

        public FellingBinding(TapController tapController, Cutter cutter)
        {
            _tapController = tapController;
            _cutter = cutter;
        }

        public void Initialize()
        {
            _tapController.OnLeftTap += () => _cutter.Cut(Side.Left);
            _tapController.OnRightTap += () => _cutter.Cut(Side.Right);
        }
    }
}