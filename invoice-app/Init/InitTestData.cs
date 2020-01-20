using invoice_app.Services;
using invoice_demo_app.invoice.service;
using invoice_demo_app.Models;
using invoice_demo_app.Pdf.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
/**
*
* @author dgumbo
*/
public class InitTestData
{

    private readonly InvoiceService invoiceService;
    private readonly ReceiptService receiptService;
    private readonly ProductService productService;
    private readonly AddressService addressService;

    public InitTestData(ReceiptService receiptService, InvoiceService invoiceService, ProductService productService, AddressService addressService)
    {
        this.invoiceService = invoiceService;
        this.productService = productService;
        this.addressService = addressService;
        this.receiptService = receiptService;
    }

    public Invoice initInvoiceData()
    {
        List<Invoice> invoices = invoiceService.FindAll().ToListAsync().Result;
        if (invoices.Count == 0)
        {
            Random r = new Random();
            int lowerBound = 1000000000;
            int upperBound = 1999999999;
            int result = r.Next(upperBound - lowerBound) + lowerBound;
            String number = result.ToString();

            Address billingAddress = initBillingAddress();
            String notes = initNotes();
            String paymentDetails = initPaymentDetails();

            Invoice invoice = new Invoice()
            {
                BillTo = billingAddress,
                ShipTo = billingAddress,
                Date =   DateTime.Now ,
                Number = "INV" + number,
                EndNotes = notes,
                PaymentDetails = paymentDetails
            };

            List<Product> products = initProductListData();
            invoice.InvoiceLines = initInvoiceLineListData(products); 

            return invoiceService.Create(invoice);
        }
        else
        {
            return invoices[0];
        }
    }

    //    public Receipt initReceiptData() {
    //        List<Receipt> receipts = receiptService.findAll();
    //        List<Invoice> invoices = invoiceService.findAll();

    //        if (receipts.isEmpty()) {
    //            Random r = new Random();
    //            int lowerBound = 1000000000;
    //            int upperBound = 1999999999;
    //            Integer result = r.nextInt(upperBound - lowerBound) + lowerBound;
    //            String number = result.toString();

    //            Address billingAddress = initBillingAddress();
    //            String notes = initNotes();

    //            billingAddress = addressService.save(billingAddress);

    //            Receipt receipt = new Receipt();
    //            receipt.BillTo(billingAddress);
    ////            invoice.ShipTo(billingAddress); 

    //            receipt.Date(new Date());
    //            receipt.Number("RCT" + number);
    //            if (invoices.isEmpty()) {
    //                receipt.InvoiceRef("INV20001250103");
    //            } else {
    //                receipt.InvoiceRef(invoices.get(0).getNumber());
    //            }
    //            receipt.EndNotes(notes);

    //            List<Product> products = initProductListData();
    ////        products.forEach(prod => prod = productService.save(prod));

    //            List<ReceiptLine> rows = initReceiptLineListData(products);
    //            for (ReceiptLine row : rows) {
    //                row.Receipt(receipt);
    //            }
    //            receipt.ReceiptLines(rows);

    //            return receiptService.save(receipt);
    //        } else {
    //            return receipts.get(0);
    //        }
    //    }

    private Address initBillingAddress()
    {
        List<Address> addresses = addressService.FindAll().ToListAsync().Result;
        if (addresses.Count == 0)
        {
            Address shipToAddress = new Address()
            {
                Title = "Mr.",
                Firstname = "Denzil",
                Lastname = "Gumbo",

                Address1 = "14657 Galloway Park",
                Address2 = "",
                Address3 = "",
                City = "Norton",
                Country = "Zimbabwe"
            };
            shipToAddress = addressService.Create(shipToAddress);
            return shipToAddress;
        }
        else
        {
            return addresses[0];
        }
    }

    private List<InvoiceLine> initInvoiceLineListData(List<Product> products)
    {
        InvoiceLine ir1 = new InvoiceLine()
        {
            Product = products[0],
            Quantity = 3L,
            Price = 430.00
        };

        InvoiceLine ir2 = new InvoiceLine()
        {
            Product = products[1],
            Quantity = 2L,
            Price = 351.09
        };
        InvoiceLine ir3 = new InvoiceLine()
        {
            Product = products[2],
            Quantity = 1L,
            Price = 951.09
        };

        return new List<InvoiceLine>() { ir1, ir2, ir3 };
    }

    //private List<ReceiptLine> initReceiptLineListData(List<Product> products) {
    //    ReceiptLine ir1 = new ReceiptLine();
    //    ir1.Product=products.get(0));
    //    ir1.Quantit=y(90L);
    //    ir1.Price=new BigDecimal("5.39"));

    //    ReceiptLine ir2 = new ReceiptLine();
    //    ir2.Product=products.get(1));
    //    ir2.Quantity=75L);
    //    ir2.Price=new BigDecimal("30.72"));

    //    ReceiptLine ir3 = new ReceiptLine();
    //    ir3.Product=products[2];
    //    ir3.Quantity= 195  ;
    //    ir3.Price= 79.70 ;

    //    List<ReceiptLine> rows = new List<InvoiceLine>();
    //    rows.Add(ir1);
    //    rows.Add(ir2);
    //    rows.Add(ir3);
    //    return rows;
    //}

    private List<Product> initProductListData()
    {
        List<Product> products = productService.FindAll().ToListAsync().Result;

        String number1 = "1000001105";
        if (products.Find(prod => prod.Number.Equals(number1)) == null)
        {
            Product prod1 = new Product()
            {
                Number = number1,
                Name = "HisOlut - Steamed Chow Mein HisOlut - Steamed Chow Mein HisOlut - Steamed Chow Mein HisOlut - Steamed Chow Mein HisOlut - Steamed Chow Mein HisOlut - Steamed Chow Mein",
                Description = "HisOlut - Steamed Chow Mein"
            };
            prod1 = productService.Create(prod1);
            products.Add(prod1);
        }

        String number2 = "1000001113";
        if (products.Find(prod => prod.Number.Equals(number2)) == null)
        {
            Product prod2 = new Product();
            prod2.Number = number2;
            prod2.Name = "Tea - Honey Green Tea";
            prod2.Description = "Tea - Honey Green Tea";
            prod2 = productService.Create(prod2);
            products.Add(prod2);
        }

        String number3 = "1000001121";
        if (products.Find(prod => prod.Number.Equals(number3)) == null)
        {
            Product prod3 = new Product();
            prod3.Number = number3;
            prod3.Name = "Paper Towel Touchless";
            prod3.Description = "Paper Towel Touchless";
            prod3 = productService.Create(prod3);
            products.Add(prod3);
        }

        return products;
    } 

    private String initNotes()
    {
        String notes = "these are test end notes - notes";

        return notes;
    }

    private String initPaymentDetails()
    {
        String notes = "Account Name : Heritage Innovative Solutions";
        notes += "\n" + "Account Number : 500002833308";
        notes += "\n" + "Bank : People's Own Savings Bank (POSB)";
        notes += "\n" + "Branch : Harare Main Post Office";

        return notes;
    }
}
