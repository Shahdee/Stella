using System;

namespace Level
{
    public class LevelCollisionController : ILevelCollisionController, IDisposable
    {
        private readonly LevelView _levelView;
        private readonly ILevelViewController _levelViewController;

        public LevelCollisionController(LevelView levelView,
                                        ILevelViewController levelViewController)
        {
            _levelView = levelView;
            _levelViewController = levelViewController;

            _levelViewController.OnInitialize += AddInitialColliders;
        }

        private void AddInitialColliders()
        {
            // foreach (var pos in _levelView.Level.cellBounds.allPositionsWithin)
            // {
            //     var block = _levelModel.GetBlock(pos);
            //     if (block == null)
            //         continue;
            //
            //     if (HasNeighbours(block))
            //         AddCollider(block);
            // }
        }

        public void Dispose()
        {
            _levelViewController.OnInitialize -= AddInitialColliders;
        }
    }
}