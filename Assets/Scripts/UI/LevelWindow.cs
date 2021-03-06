using UI.Views;
using Zenject;
using Level;

namespace UI
{
    public class LevelWindow : AbstractWindow
    {
        public override EWindowType WindowType => EWindowType.Level;
        
        private readonly LazyInject<LevelWindowView> _view;
        private readonly ILevelModel _levelModel; // TODO I should use level controller to get current level 

        public LevelWindow(LazyInject<LevelWindowView> view,
                            ILevelModel levelModel)
        {
            _view = view;
            _levelModel = levelModel;
        }
        
        protected override void OnAssignView() => SetView(_view.Value);

        protected override void OnBeforeShow()
        {
            _view.Value.LevelNumber.text = (_levelModel.CurrentLevel + 1).ToString();
        }
    }
}