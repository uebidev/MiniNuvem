using System;
namespace Wrapper
{
	public class ProductWrapper
	{
		private int _id;

		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		private string _Name;

		public int Name
		{
			get { return _Name; }
			set { _Name = value; }
		}

		private double _price;

		public int Price
		{
			get { return double _price; }
			set { double _price.ToString("F2",CultureInfo.InvariantCulture) = value; }
		}




	}
}