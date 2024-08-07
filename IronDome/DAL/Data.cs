namespace IronDome.DAL
{
	public class Data
	{
		string Cs = "server = YOSIWEBIKS\\SQLEXPRESS; initial catalog = iron_dome_db; user id = sa; password = 1234; TrustServerCertificate=Yes";

		private Data()
		{
			Layer = new DataLayer(Cs);
		}

		static Data _Data;


		public static DataLayer Get
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
