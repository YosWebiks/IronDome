namespace IronDome.DAL
{
	public class Data
	{
		string Cs = "";

		private Data()
		{
			Layer = new DataLayer(Cs);
		}

		static Data _Data;


		public static DataLayer GetData
		{
			get
			{
				if (_Data == null)
				{
					_Data = new Data();
				}
				return _Data.Layer;
			}
		}


		public DataLayer Layer { get; set; }


	}
}
