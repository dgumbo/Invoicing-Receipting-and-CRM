
//@Entity
//@Data
//@Table(name = "pdf_page_settings")
using invoice_demo_app.Basic.models;
/**
*
* @author dgumbo
*/
public class PdfPageSettings : BaseEntity { 
    
    //@Column(nullable = false, unique = true)
    //@Enumerated(EnumType.STRING)
    private PaperDef paperDef;

    //@Column(nullable = false)
    private int Height;

    //@Column(nullable = false)
    private int Width;

    //@Column(nullable = false)
    private int thermalPaperTopMargin = 0;

    //@Column(nullable = false)
    private int thermalPaperRightMargin = 0;

    //@Column(nullable = false)
    private int thermalPaperBottomMargin = 0;

    //@Column(nullable = true)
    private int thermalPaperLeftMargin = 0;
     
    public enum PaperDef {
        DEFAULT_RECEIPT, SHORT_RECEIPT, NORMAL_RECEIPT, LONG_RECEIPT
    }

}
