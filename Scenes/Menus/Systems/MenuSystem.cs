﻿using Arch.Core;
using Arch.Core.Extensions;
using NovemberPirates.Scenes.Menus.Components;
using NovemberPirates.Systems;
using Raylib_CsLo;
using System.Numerics;

namespace NovemberPirates.Scenes.Menus.Systems
{
    internal class MenuSystem : GameSystem
    {

        internal override void Update(World world) { }

        internal override void UpdateNoCamera(World world)
        {
            //RayGui.GuiLoadStyle("Assets/lavanda.rgs");
            var query = new QueryDescription().WithAny<UiTitle, UiButton>();

            var centerPoint = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2);

            var dummyrect = new Rectangle(centerPoint.X - 200, centerPoint.Y - 150, 400, 400);
            RayGui.GuiDummyRec(dummyrect, "");
            var index = 0;

            RayGui.GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE, 48);
            RayGui.GuiSetStyle((int)GuiControl.LABEL, (int)GuiControlProperty.TEXT_ALIGNMENT, 1);

            RayGui.GuiSetStyle((int)GuiControl.BUTTON, (int)GuiControlProperty.TEXT_ALIGNMENT, 0);
            world.Query(in query, (entity) =>
            {
                index++;
                if (entity.Has<UiTitle>())
                {
                    var titleComponent = entity.Get<UiTitle>();

                    var text = titleComponent.Text;
                    var rect = new Rectangle(centerPoint.X - 100, 200 + 50 * titleComponent.Order, 200, 100);
                    //RayGui.GuiTextBox(text, centerPoint.X - 200, centerPoint.Y - 200, 24, Raylib.ORANGE);


                    // TODO fix font size

                    RayGui.GuiLabel(rect, text);
                }
                RayGui.GuiSetStyle((int)GuiControl.BUTTON, (int)GuiControlProperty.TEXT_ALIGNMENT, 1);

                RayGui.GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE, 24);

                if (entity.Has<UiButton>())
                {
                    var button = entity.Get<UiButton>();
                    var rect = dummyrect with { x = dummyrect.x + 100, y = dummyrect.y + (60 * button.Order), width = 200, height = 50 };

                    if (RayGui.GuiButton(rect, button.Text))
                    {
                        button.Action();
                    }
                }

            });
        }
    }
}
