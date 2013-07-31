using System;
using System.IO;
using System.Collections;

namespace TrainingSessionUtility
{
    /// <summary>
    /// Represents an Excel report.
    /// This is a very dead simple excel report showing sample prices data.
    /// </summary>
    public class ExcelReport
    {

        private string myXmlFile;
        ArrayList allPriceObjects;
        public Form1 parent;
        /// <summary>
        /// A simple way to provide sample data for the excel report.
        /// </summary>
        public class SamplePriceData
        {
            
            public string myTitle;
            public decimal myRate;
            public decimal myMinCharge;
            public decimal myMaxCharge;
            public string myDataVersion;
            public System.DateTime myDateCreated;
            public System.DateTime myDateModified;

            public SamplePriceData
                (string title, decimal rate, decimal mincharge, decimal maxcharge,
                string dataversion, System.DateTime datecreated, System.DateTime datemodified)
            {
                myTitle = title;
                myRate = rate;
                myMinCharge = mincharge;
                myMaxCharge = maxcharge;
                myDataVersion = dataversion;
                myDateCreated = datecreated;
                myDateModified = datemodified;
            }
        } // SamplePriceData

        /// <summary>
        /// Instantiate a price report.
        /// </summary>
        public ExcelReport(Form1 tparent)
        {
            parent = tparent;
            myXmlFile = @".\prices.xml";

            allPriceObjects = new ArrayList(5);

            allPriceObjects.Add(new SamplePriceData
                ("Standard", 10, 5, 25, "1.0", System.DateTime.Now, System.DateTime.Now));

            allPriceObjects.Add(new SamplePriceData
                ("Laptop", (decimal)7.5, 5, 50, "1.0", System.DateTime.Now, System.DateTime.Now));

            allPriceObjects.Add(new SamplePriceData
                ("Premium", (decimal)12.5, 10, 999, "1.0", System.DateTime.Now, System.DateTime.Now));

        } // constructor

        /// <summary>
        /// Generates the actual Excel report and saves to disk.
        /// </summary>
        public void GenerateSelf()
        {
            FileInfo fi = new FileInfo(myXmlFile);

            StreamWriter s = fi.CreateText();

            string crlf = "\r\n";

            s.WriteLine(
                "<?xml version=\"1.0\"?>" + crlf +
                "<?mso-application progid=\"Excel.Sheet\"?>" + crlf +
                "<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"" + crlf +
                "xmlns:o=\"urn:schemas-microsoft-com:office:office\"" + crlf +
                "xmlns:x=\"urn:schemas-microsoft-com:office:excel\"" + crlf +
                "xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"" + crlf +
                "xmlns:html=\"http://www.w3.org/TR/REC-html40\">" + crlf +
                "<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">" +
                "<Author>Best Cafe Management System</Author>" + crlf +
                "<LastAuthor>Best Cafe Management System</LastAuthor>" + crlf +
                //"<Created>2005-11-07T02:19:12Z</Created>
                "<Company>Nivlag.com</Company>" + crlf +
                "<Title><![CDATA[Sample Price Report as of " + System.DateTime.Now.ToString() + "]]></Title>" + crlf +
                "<Subject>Pricing Report</Subject>" + crlf +
                //"<Description>Some comments</Description>" + crlf +
                "<Version>11.6408</Version>" + crlf +
                "</DocumentProperties>");

            s.WriteLine(
                "<OfficeDocumentSettings xmlns=\"urn:schemas-microsoft-com:office:office\">" + crlf +
                "<DownloadComponents/>" + crlf +
                //"<LocationOfComponents HRef=\"file:///D:\\\"/>" + crlf +
                "</OfficeDocumentSettings>" + crlf +
                "<ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">" + crlf +
                //<WindowHeight>11765</WindowHeight>
                //<WindowWidth>15446</WindowWidth>
                //<WindowTopX>217</WindowTopX>
                //<WindowTopY>95</WindowTopY>
                //<ProtectStructure>False</ProtectStructure>
                //<ProtectWindows>False</ProtectWindows>
                "</ExcelWorkbook>");

            s.WriteLine(
                "<Styles>" + crlf +
                "  <Style ss:ID=\"Default\" ss:Name=\"Normal\">" + crlf +
                "    <Alignment ss:Vertical=\"Bottom\"/>" + crlf +
                "    <Borders/>" + crlf +
                "    <Font/>" + crlf +
                "    <Interior/>" + crlf +
                "    <NumberFormat/>" + crlf +
                "    <Protection/>" + crlf +
                "  </Style>" + crlf +
                "  <Style ss:ID=\"BoldAndUnderline\">" + crlf +
                "    <Font x:Family=\"Swiss\" ss:Bold=\"1\" ss:Underline=\"Single\"/>" + crlf +
                "  </Style>" + crlf +
                "  <Style ss:ID=\"s26\">" + crlf +
                "    <Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\"/>" + crlf +
                "    <NumberFormat ss:Format=\"&quot;$&quot;#,##0.00\"/>" + crlf +
                "  </Style>" + crlf +
                "  <Style ss:ID=\"Date\">" + crlf +
                "    <NumberFormat ss:Format=\"[$-409]m/d/yy\\ h:mm\\ AM/PM;@\"/>" + crlf +
                "  </Style>" + crlf +
                "</Styles>" + crlf);

            s.WriteLine(
                "<Worksheet ss:Name=\"Master Price List\">" + crlf +
                "<Table " +
                //ss:ExpandedColumnCount=\"3\" " +
                //"ss:ExpandedRowCount=\"1\" " +
                "x:FullColumns=\"1\"" + crlf +
                "x:FullRows=\"1\" ss:DefaultColumnWidth=\"48.905660377358487\"" + crlf +
                "ss:DefaultRowHeight=\"12.90566037735849\">" + crlf);

            s.WriteLine("<Column ss:Index=\"1\" ss:Width=\"150\"/>");     // Title
            s.WriteLine("<Column ss:Index=\"2\" ss:Width=\"35\" ss:StyleID=\"s26\"/>");   // Rate 
            s.WriteLine("<Column ss:Index=\"3\" ss:Width=\"70\" ss:StyleID=\"s26\"/>");   // Min charge
            s.WriteLine("<Column ss:Index=\"4\" ss:Width=\"70\" ss:StyleID=\"s26\"/>");   // Max charge
            s.WriteLine("<Column ss:Index=\"5\" ss:Width=\"70\" ss:StyleID=\"s26\"/>");   // data version
            s.WriteLine("<Column ss:Index=\"6\" ss:Width=\"100\" ss:StyleID=\"Date\"/>");   // date created
            s.WriteLine("<Column ss:Index=\"7\" ss:Width=\"100\" ss:StyleID=\"Date\"/>");   // date modified






            // Column headers. 
            s.WriteLine("<Row ss:StyleID=\"BoldAndUnderline\">");
            s.WriteLine("<Cell><Data ss:Type=\"String\">ID(X)</Data></Cell>");
            s.WriteLine("<Cell><Data ss:Type=\"String\">TIME (Seconds from 01/01/1970)</Data></Cell>");
            s.WriteLine("<Cell><Data ss:Type=\"String\">X Acceleration (Gs)</Data></Cell>");
            s.WriteLine("<Cell><Data ss:Type=\"String\">Y Acceleration (Gs)</Data></Cell>");
            s.WriteLine("<Cell><Data ss:Type=\"String\">Z Acceleration (Gs)</Data></Cell>");
            //s.WriteLine("<Cell><Data ss:Type=\"String\">Col6</Data></Cell>");
            //s.WriteLine("<Cell><Data ss:Type=\"String\">Col7</Data></Cell>");
            s.WriteLine("</Row>");

            // Loop
            int count = parent.AccZIndexes.Count;

            //foreach (SamplePriceData aPrice in allPriceObjects)
            for(int j = 0; j < count;j++)
            {
                s.WriteLine("<Row>");

               // s.WriteLine("<Cell><Data ss:Type=\"String\">");    s.WriteLine(aPrice.myTitle);       s.WriteLine("</Data></Cell>");
                
                




                s.WriteLine("<Cell><Data ss:Type=\"Number\">" + parent.ID[parent.AccXIndexes[j]] + "</Data></Cell>");
                s.WriteLine("<Cell><Data ss:Type=\"Number\">" + parent.DATETIME[parent.AccXIndexes[j]] + "</Data></Cell>");
                s.WriteLine("<Cell><Data ss:Type=\"Number\">" + parent.DVALUE[parent.AccXIndexes[j]] + "</Data></Cell>");
                s.WriteLine("<Cell><Data ss:Type=\"Number\">" + parent.DVALUE[parent.AccYIndexes[j]] + "</Data></Cell>");
                s.WriteLine("<Cell><Data ss:Type=\"Number\">" + parent.DVALUE[parent.AccZIndexes[j]] + "</Data></Cell>");
                //s.WriteLine("<Cell><Data ss:Type=\"Number\">" + parent.s + "</Data></Cell>");








                // Note that I've noticed that the carriage return here is necessary or my version of
                // Excel balks at processing it.
                //s.WriteLine("<Cell><Data ss:Type=\"String\">");
                //s.WriteLine(aPrice.myDataVersion);
                //s.WriteLine("</Data></Cell>");

                System.DateTime ds = new System.DateTime();
                
               // double i = parent.ID[0];
               /* ds = aPrice.myDateCreated;

                s.Write("<Cell><Data ss:Type=\"DateTime\">");
                s.Write(ds.Year.ToString("0000") + "-" + ds.Month.ToString("00") + "-" + ds.Day.ToString("00") +
                    "T" + ds.Hour.ToString("00") + ":" + ds.Minute.ToString("00") + ":" + ds.Second.ToString("00") + "." + ds.Millisecond.ToString("000"));
                s.WriteLine("</Data></Cell>");

                ds = aPrice.myDateModified;

                s.Write("<Cell><Data ss:Type=\"DateTime\">");
                s.Write(ds.Year.ToString("0000") + "-" + ds.Month.ToString("00") + "-" + ds.Day.ToString("00") +
                    "T" + ds.Hour.ToString("00") + ":" + ds.Minute.ToString("00") + ":" + ds.Second.ToString("00") + "." + ds.Millisecond.ToString("000"));
                s.WriteLine("</Data></Cell>");*/

                s.WriteLine("</Row>" + crlf);
            }
            s.WriteLine("</Table>" + crlf);

            s.WriteLine(
                "<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">" + crlf +
                "<Selected/>" + crlf +
                "<Panes>" + crlf +
                "<Pane>" + crlf +
                "<Number>3</Number>" + crlf +
                "<ActiveRow>1</ActiveRow>" + crlf +
                "</Pane>" + crlf +
                "</Panes>" + crlf +
                "<ProtectObjects>False</ProtectObjects>" + crlf +
                "<ProtectScenarios>False</ProtectScenarios>" + crlf +
                "</WorksheetOptions>" + crlf +
                "</Worksheet>");

            s.WriteLine("</Workbook>" + crlf);

            s.Close();
        } // GenerateReport

    }
}

