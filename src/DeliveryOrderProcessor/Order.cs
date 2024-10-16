using System;

namespace DeliveryOrderProcessor;
public class Order
{
    public string id { get; set; }
    public string buyerId { get; set; }
    public DateTime orderDate { get; set; }
    public Shiptoaddress shipToAddress { get; set; }
    public Orderitem[] orderItems { get; set; }
    public decimal total { get; set; }
}

public class Shiptoaddress
{
    public string street { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string country { get; set; }
    public string zipCode { get; set; }
}

public class Orderitem
{
    public Itemordered itemOrdered { get; set; }
    public float unitPrice { get; set; }
    public int units { get; set; }
    public int id { get; set; }
}

public class Itemordered
{
    public int catalogItemId { get; set; }
    public string productName { get; set; }
    public string pictureUri { get; set; }
}
