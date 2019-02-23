namespace PaymentGatewayAPI.Models.Interfaces
{
	public interface IOrderItem
	{
		string ProductId{ get; set; }
		string ProductTitle { get; set; }
		double Price{ get; set; }
		string Category { get; set; }
		double Quantity{ get; set; }
	}
}
