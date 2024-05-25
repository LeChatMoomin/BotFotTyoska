namespace MyBot.Bot
{
	public class BotPresenter
	{
		private BotModel Model;
		private BotView View;

		public BotPresenter()
		{
			Model = new BotModel();
			View = new BotView();

			View.OnGotCommandMessage += Model.HandleCommand;
			Model.OnReadyToResponse += View.SendResponse;
		}
	}

	
}
