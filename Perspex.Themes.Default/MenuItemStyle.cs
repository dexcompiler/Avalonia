﻿// -----------------------------------------------------------------------
// <copyright file="MenuItemStyle.cs" company="Steven Kirk">
// Copyright 2015 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Perspex.Themes.Default
{
    using Perspex.Controls;
    using Perspex.Controls.Presenters;
    using Perspex.Controls.Shapes;
    using Perspex.Input;
    using Perspex.Layout;
    using Perspex.Media;
    using Perspex.Styling;
    using System.Linq;

    public class MenuItemStyle : Styles
    {
        public MenuItemStyle()
        {
            this.AddRange(new[]
            {
                new Style(x => x.OfType<MenuItem>())
                {
                    Setters = new[]
                    {
                        new Setter(MenuItem.BorderThicknessProperty, 1.0),
                        new Setter(MenuItem.PaddingProperty, new Thickness(6, 0)),
                        new Setter(MenuItem.TemplateProperty, ControlTemplate.Create<MenuItem>(this.Template)),
                    },
                },
                new Style(x => x.OfType<MenuItem>().Class(":pointerover").Template().Name("root"))
                {
                    Setters = new[]
                    {
                        new Setter(Border.BackgroundProperty, new SolidColorBrush(0x3d26a0da)),
                        new Setter(Border.BorderBrushProperty, new SolidColorBrush(0xff26a0da)),
                    },
                }
            });
        }

        private Control Template(MenuItem control)
        {
            Popup popup;

            var result = new Border
            {
                Name = "root",
                [~Border.BackgroundProperty] = control[~MenuItem.BackgroundProperty],
                [~Border.BorderBrushProperty] = control[~MenuItem.BorderBrushProperty],
                [~Border.BorderThicknessProperty] = control[~MenuItem.BorderThicknessProperty],
                Content = new Grid
                {
                    ColumnDefinitions = new ColumnDefinitions
                    {
                        new ColumnDefinition(GridLength.Auto),
                        new ColumnDefinition(GridLength.Auto),
                    },
                    Children = new Controls
                    {
                        //new ContentPresenter
                        //{
                        //    Name = "icon",
                        //    [~ContentPresenter.ContentProperty] = control[~MenuItem.IconProperty],
                        //    Width = 16,
                        //    Height = 16,
                        //    Margin = new Thickness(3),
                        //    HorizontalAlignment = HorizontalAlignment.Center,
                        //    VerticalAlignment = VerticalAlignment.Center,
                        //},
                        new Path
                        {
                            Data = StreamGeometry.Parse("F1M10,1.2L4.7,9.1 4.5,9.1 0,5.2 1.3,3.5 4.3,6.1 8.3,0 10,1.2z"),
                            Margin = new Thickness(3),
                            IsVisible = false,
                            VerticalAlignment = VerticalAlignment.Center,
                            [~Path.FillProperty] = control[~MenuItem.ForegroundProperty],
                        },
                        new ContentPresenter
                        {
                            [~ContentPresenter.ContentProperty] = control[~MenuItem.HeaderProperty],
                            [~ContentPresenter.MarginProperty] = control[~MenuItem.PaddingProperty],
                            [Grid.ColumnProperty] = 1,
                        },
                        (popup = new Popup
                        {
                            Name = "popup",
                            StaysOpen = false,
                            [!!Popup.IsOpenProperty] = control[!!MenuItem.IsSubMenuOpenProperty],
                            Child = new Border
                            {
                                Background = new SolidColorBrush(0xfff0f0f0),
                                BorderBrush = new SolidColorBrush(0xff999999),
                                BorderThickness = 1,
                                Padding = new Thickness(2),
                                Content = new ScrollViewer
                                {
                                    Content = new ItemsPresenter
                                    {
                                        Name = "itemsPresenter",
                                        [~ItemsPresenter.ItemsProperty] = control[~Menu.ItemsProperty],
                                        [~ItemsPresenter.ItemsPanelProperty] = control[~Menu.ItemsPanelProperty],
                                        [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                                    }
                                }
                            }
                         })
                    },
                }
            };

            popup.PlacementTarget = result;

            return result;
        }
    }
}
