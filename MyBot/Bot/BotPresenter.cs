﻿namespace MyBot.Bot
{
	public class BotPresenter
	{
		private BotModel Model;
		private BotView View;

		public BotPresenter()
		{
			Model = new BotModel();
			View = new BotView();

			View.OnGotMessage += Model.Update;
			Model.OnReadyToResponse += View.SendResponse;
		}
	}

	
}
