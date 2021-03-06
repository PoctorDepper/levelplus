using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using levelplus.UI;

namespace levelplus
{
	public class levelplus : Mod
	{

		internal GUI gui;
		public UserInterface guiInterface;

		internal LevelUI levelUI;
		public UserInterface levelInterface;

		public override void Load()
		{
			//makes sure UI isn't opened server side
			if (!Main.dedServ)
			{
				gui = new GUI();
				gui.Activate();
				guiInterface = new UserInterface();
				guiInterface.SetState(gui);

				levelUI = new LevelUI();
				levelUI.Activate();
				levelInterface = new UserInterface();
				levelInterface.SetState(levelUI);
			}
		}

		public override void UpdateUI(GameTime gameTime)
		{
			//will only draw UI if !inMenu
			if (/*!Main.gameMenu &&*/ GUI.visible)
				guiInterface?.Update(gameTime);

			if (LevelUI.visible)
				levelInterface?.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			base.ModifyInterfaceLayers(layers);

			int resourceBarsIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
			layers.RemoveAt(resourceBarsIndex);
			layers.Insert(resourceBarsIndex, new LegacyGameInterfaceLayer("Level+: Resource Bars", delegate
			{
				if (/*!Main.gameMenu &&*/ GUI.visible)
					guiInterface.Draw(Main.spriteBatch, new GameTime());
				if (LevelUI.visible)
					levelInterface.Draw(Main.spriteBatch, new GameTime());

				return true;
			}, InterfaceScaleType.UI));
		}

		/*private bool DrawBars()
		{
			if (!Main.gameMenu && GUI.visible)
			{
				guiInterface.Draw(Main.spriteBatch, new GameTime());
			}

			return true;
		}*/
	}
}
