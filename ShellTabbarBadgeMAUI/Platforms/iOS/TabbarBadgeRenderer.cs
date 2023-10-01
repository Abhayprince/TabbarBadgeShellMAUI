using System;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;
using UIKit;

namespace ShellTabbarBadgeMAUI
{
    public class TabbarBadgeRenderer : ShellRenderer
    {
        protected override IShellTabBarAppearanceTracker CreateTabBarAppearanceTracker()
        {
            //return base.CreateTabBarAppearanceTracker();
            return new BadgeShellTabbarAppearanceTracker();
        }
    }
    class BadgeShellTabbarAppearanceTracker : ShellTabBarAppearanceTracker
    {
        private UITabBarItem? _cartTabbarItem;
        public override void UpdateLayout(UITabBarController controller)
        {
            base.UpdateLayout(controller);

            if (_cartTabbarItem is null)
            {
                const int cartTabbarItemIndex = 1;

                _cartTabbarItem = controller.TabBar.Items?[cartTabbarItemIndex];
                if (_cartTabbarItem is not null)
                {
                    UpdateBadge(0);
                    BadgeCounterService.CountChanged += OnCountChanged;
                }
            }
        }
        private void OnCountChanged(object? sender, int newCount)
        {
            UpdateBadge(newCount);
        }
        private void UpdateBadge(int count)
        {
            if(_cartTabbarItem is not null)
            {
                if(count <= 0)
                {
                    _cartTabbarItem.BadgeValue = null;
                }
                else
                {
                    _cartTabbarItem.BadgeValue = count.ToString();
                    _cartTabbarItem.BadgeColor = Colors.Red.ToPlatform();
                }
            }
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            BadgeCounterService.CountChanged -= OnCountChanged;
        }
    }
}

