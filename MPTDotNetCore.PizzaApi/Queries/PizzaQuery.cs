namespace MPTDotNetCore.PizzaApi.Queries;

public class PizzaQuery
{
    public static string PizzaOrderQuery { get; } =
        @"SELECT PO.*, P.Pizza, P.Price FROM Tbl_PizzaOrder PO
            INNER JOIN Tbl_Pizza P ON P.PizzaId = PO.PizzaId
            WHERE PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo";

    public static string PizzaOrderDetailQuery { get; } =
        @"SELECT POD.*, PE.PizzaExtraName, PE.Price FROM Tbl_PizzaOrderDetail POD
            INNER JOIN Tbl_PizzaExtra PE ON POD.PizzaExtraId = PE.PizzaExtraId
            WHERE PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo";
}