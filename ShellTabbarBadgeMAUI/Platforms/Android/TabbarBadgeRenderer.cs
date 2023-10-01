using System;
using Google.Android.Material.Badge;
using Google.Android.Material.BottomNavigation;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;

namespace ShellTabbarBadgeMAUI
{
    public class TabbarBadgeRenderer : ShellRenderer
    {
        protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
        {
            //return base.CreateBottomNavViewAppearanceTracker(shellItem);
            return new BadgeShellBottomNavViewAppearanceTracker(this, shellItem);
        }
    }

    class BadgeShellBottomNavViewAppearanceTracker : ShellBottomNavViewAppearanceTracker
    {
        private BadgeDrawable? badgeDrawable;
        public BadgeShellBottomNavViewAppearanceTracker(IShellContext shellContext, ShellItem shellItem) : base(shellContext, shellItem)
        {
        }
        public override void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {
            base.SetAppearance(bottomView, appearance);

            if (badgeDrawable is null)
            {
                const int cartTabbarItemIndex = 1;

                badgeDrawable = bottomView.GetOrCreateBadge(cartTabbarItemIndex);
                UpdateBadge(0);
                BadgeCounterService.CountChanged += OnCountChanged;
            }
        }
        private void OnCountChanged(object? sender, int newCount)
        {
            UpdateBadge(newCount);
        }
        private void UpdateBadge(int count)
        {
            if(badgeDrawable is not null)
            {
                if (count <= 0)
                {
                    badgeDrawable.SetVisible(false);
                }
                else
                {
                    badgeDrawable.Number = count;
                    badgeDrawable.BackgroundColor = Colors.Red.ToPlatform();
                    badgeDrawable.BadgeTextColor = Colors.White.ToPlatform();
                    badgeDrawable.SetVisible(true);
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

