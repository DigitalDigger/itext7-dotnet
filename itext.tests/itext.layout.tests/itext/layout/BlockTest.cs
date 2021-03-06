/*
This file is part of the iText (R) project.
Copyright (c) 1998-2017 iText Group NV
Authors: iText Software.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
using System;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Xobject;
using iText.Kernel.Utils;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Layout {
    public class BlockTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/layout/BlockTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/layout/BlockTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [LogMessage(iText.IO.LogMessageConstant.CLIP_ELEMENT, Count = 2)]
        [NUnit.Framework.Test]
        public virtual void BlockWithSetHeightProperties01() {
            String outFileName = destinationFolder + "blockWithSetHeightProperties01.pdf";
            String cmpFileName = sourceFolder + "cmp_blockWithSetHeightProperties01.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFileName));
            String textByron = "When a man hath no freedom to fight for at home,\n" + "    Let him combat for that of his neighbours;\n"
                 + "Let him think of the glories of Greece and of Rome,\n" + "    And get knocked on the head for his labours.\n"
                 + "\n" + "To do good to Mankind is the chivalrous plan,\n" + "    And is always as nobly requited;\n"
                 + "Then battle for Freedom wherever you can,\n" + "    And, if not shot or hanged, you'll get knighted.";
            Document doc = new Document(pdfDocument);
            Paragraph p = new Paragraph(textByron);
            for (int i = 0; i < 10; i++) {
                p.Add(textByron);
            }
            p.SetBorder(new SolidBorder(0.5f));
            doc.Add(new Paragraph("Default layout:"));
            doc.Add(p);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Paragraph's height is set shorter than needed:"));
            p.SetHeight(1300);
            doc.Add(p);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Paragraph's min height is set shorter than needed:"));
            p.DeleteOwnProperty(Property.HEIGHT);
            p.SetMinHeight(1300);
            doc.Add(p);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Paragraph's max height is set shorter than needed:"));
            p.DeleteOwnProperty(Property.MIN_HEIGHT);
            p.SetMaxHeight(1300);
            doc.Add(p);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Paragraph's height is set bigger than needed:"));
            p.DeleteOwnProperty(Property.MAX_HEIGHT);
            p.SetHeight(2500);
            doc.Add(p);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Paragraph's min height is set bigger than needed:"));
            p.DeleteOwnProperty(Property.HEIGHT);
            p.SetMinHeight(2500);
            doc.Add(p);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Paragraph's max height is set bigger than needed:"));
            p.DeleteOwnProperty(Property.MIN_HEIGHT);
            p.SetMaxHeight(2500);
            doc.Add(p);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , "diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [LogMessage(iText.IO.LogMessageConstant.CLIP_ELEMENT, Count = 2)]
        [NUnit.Framework.Test]
        public virtual void BlockWithSetHeightProperties02() {
            String outFileName = destinationFolder + "blockWithSetHeightProperties02.pdf";
            String cmpFileName = sourceFolder + "cmp_blockWithSetHeightProperties02.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFileName));
            String textByron = "When a man hath no freedom to fight for at home,\n" + "    Let him combat for that of his neighbours;\n"
                 + "Let him think of the glories of Greece and of Rome,\n" + "    And get knocked on the head for his labours.\n"
                 + "\n" + "To do good to Mankind is the chivalrous plan,\n" + "    And is always as nobly requited;\n"
                 + "Then battle for Freedom wherever you can,\n" + "    And, if not shot or hanged, you'll get knighted.";
            Document doc = new Document(pdfDocument);
            Paragraph p = new Paragraph(textByron);
            Div div = new Div();
            div.SetBorder(new SolidBorder(Color.RED, 2));
            for (int i = 0; i < 5; i++) {
                div.Add(p);
            }
            PdfImageXObject xObject = new PdfImageXObject(ImageDataFactory.Create(sourceFolder + "Desert.jpg"));
            iText.Layout.Element.Image image = new iText.Layout.Element.Image(xObject, 300);
            div.Add(image);
            doc.Add(new Paragraph("Default layout:"));
            doc.Add(div);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Div's height is set shorter than needed:"));
            div.SetHeight(1000);
            doc.Add(div);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Div's min height is set shorter than needed:"));
            div.DeleteOwnProperty(Property.HEIGHT);
            div.SetMinHeight(1000);
            doc.Add(div);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Div's max height is set shorter than needed:"));
            div.DeleteOwnProperty(Property.MIN_HEIGHT);
            div.SetMaxHeight(1000);
            doc.Add(div);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Div's height is set bigger than needed:"));
            div.DeleteOwnProperty(Property.MAX_HEIGHT);
            div.SetHeight(2500);
            doc.Add(div);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Div's min height is set bigger than needed:"));
            div.DeleteOwnProperty(Property.HEIGHT);
            div.SetMinHeight(2500);
            doc.Add(div);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Div's max height is set bigger than needed:"));
            div.DeleteOwnProperty(Property.MIN_HEIGHT);
            div.SetMaxHeight(2500);
            doc.Add(div);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , "diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BlockFillAvailableArea01() {
            String outFileName = destinationFolder + "blockFillAvailableArea01.pdf";
            String cmpFileName = sourceFolder + "cmp_blockFillAvailableArea01.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFileName));
            String textByron = "When a man hath no freedom to fight for at home,\n" + "    Let him combat for that of his neighbours;\n"
                 + "Let him think of the glories of Greece and of Rome,\n" + "    And get knocked on the head for his labours.\n"
                 + "\n" + "To do good to Mankind is the chivalrous plan,\n" + "    And is always as nobly requited;\n"
                 + "Then battle for Freedom wherever you can,\n" + "    And, if not shot or hanged, you'll get knighted."
                 + "To do good to Mankind is the chivalrous plan,\n" + "    And is always as nobly requited;\n" + "Then battle for Freedom wherever you can,\n"
                 + "    And, if not shot or hanged, you'll get knighted." + "To do good to Mankind is the chivalrous plan,\n"
                 + "    And is always as nobly requited;\n" + "Then battle for Freedom wherever you can,\n" + "    And, if not shot or hanged, you'll get knighted.";
            textByron = textByron + textByron;
            Document doc = new Document(pdfDocument);
            DeviceRgb blue = new DeviceRgb(80, 114, 153);
            Div text = new Div().Add(new Paragraph(textByron));
            Div image = new Div().Add(new iText.Layout.Element.Image(ImageDataFactory.Create(sourceFolder + "Desert.jpg"
                )).SetHeight(500).SetAutoScaleWidth(true));
            doc.Add(new Div().Add(new Paragraph("Fill on split").SetFontSize(30).SetFontColor(blue).SetTextAlignment(TextAlignment
                .CENTER)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetFillAvailableArea(true)).Add(new AreaBreak
                ());
            doc.Add(new Paragraph("text").SetFontSize(18).SetFontColor(blue));
            Div div = CreateDiv(text, textByron, blue, true, false, true);
            doc.Add(div);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("image").SetFontSize(18).SetFontColor(blue));
            div = CreateDiv(image, textByron, blue, false, false, true);
            doc.Add(div);
            doc.Add(new AreaBreak());
            doc.Add(new Div().Add(new Paragraph("Fill always").SetFontSize(30).SetFontColor(blue).SetTextAlignment(TextAlignment
                .CENTER)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetFillAvailableArea(true)).Add(new AreaBreak
                ());
            doc.Add(new Paragraph("text").SetFontSize(18).SetFontColor(blue));
            div = CreateDiv(text, textByron, blue, true, true, false);
            doc.Add(div);
            doc.Add(new Paragraph("image").SetFontSize(18).SetFontColor(blue));
            div = CreateDiv(image, textByron, blue, false, true, false);
            doc.Add(div);
            doc.Add(new Div().Add(new Paragraph("No fill").SetFontSize(30).SetFontColor(blue).SetTextAlignment(TextAlignment
                .CENTER)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetFillAvailableArea(true)).Add(new AreaBreak
                ());
            doc.Add(new Paragraph("text").SetFontSize(18).SetFontColor(blue));
            div = CreateDiv(text, textByron, blue, true, false, false);
            doc.Add(div);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("image").SetFontSize(18).SetFontColor(blue));
            div = CreateDiv(image, textByron, blue, false, false, false);
            doc.Add(div);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , "diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1092")]
        public virtual void MarginsBordersPaddingOverflow01() {
            String outFileName = destinationFolder + "marginsBordersPaddingOverflow01.pdf";
            String cmpFileName = sourceFolder + "cmp_marginsBordersPaddingOverflow01.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDocument);
            Div div = new Div();
            div.SetHeight(760).SetBackgroundColor(Color.DARK_GRAY);
            doc.Add(div);
            // TODO overflow of this div on second page is of much bigger height than 1pt
            Div div1 = new Div().SetMarginTop(42).SetMarginBottom(42).SetBackgroundColor(Color.BLUE).SetHeight(1);
            doc.Add(div1);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , "diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1092")]
        public virtual void MarginsBordersPaddingOverflow02() {
            String outFileName = destinationFolder + "marginsBordersPaddingOverflow02.pdf";
            String cmpFileName = sourceFolder + "cmp_marginsBordersPaddingOverflow02.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDocument);
            // TODO div with fixed height is bigger than 60pt
            Div div = new Div();
            div.SetHeight(60).SetBackgroundColor(Color.DARK_GRAY);
            Div div1 = new Div().SetMarginTop(200).SetMarginBottom(200).SetBorder(new SolidBorder(6));
            div.Add(div1);
            doc.Add(div);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , "diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1092")]
        public virtual void MarginsBordersPaddingOverflow03() {
            String outFileName = destinationFolder + "marginsBordersPaddingOverflow03.pdf";
            String cmpFileName = sourceFolder + "cmp_marginsBordersPaddingOverflow03.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDocument);
            Div div = new Div();
            div.SetHeight(710).SetBackgroundColor(Color.DARK_GRAY);
            doc.Add(div);
            // TODO this element is below first page visible area
            Div div1 = new Div().SetMarginTop(200).SetMarginBottom(200).SetBorder(new SolidBorder(6));
            doc.Add(div1);
            doc.Add(new AreaBreak());
            // TODO same with this one the second page
            SolidBorder border = new SolidBorder(400);
            Div div2 = new Div().SetBorderTop(border).SetBorderBottom(border);
            doc.Add(div);
            doc.Add(div2);
            doc.Add(new AreaBreak());
            // TODO same with this one the third page
            Div div3 = new Div().SetBorder(new SolidBorder(6)).SetPaddingTop(400).SetPaddingBottom(400);
            doc.Add(div);
            doc.Add(div3);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , "diff"));
        }

        private Div CreateDiv(Div innerOverflowDiv, String text, DeviceRgb backgroundColor, bool keepTogether, bool
             fillAlways, bool fillOnSplit) {
            Div div = new Div().SetBorder(new DoubleBorder(10)).SetBackgroundColor(new DeviceRgb(216, 243, 255)).SetFillAvailableAreaOnSplit
                (fillOnSplit).SetFillAvailableArea(fillAlways);
            div.Add(new Paragraph(text));
            div.Add(innerOverflowDiv.SetKeepTogether(keepTogether));
            if (backgroundColor != null) {
                innerOverflowDiv.SetBackgroundColor(backgroundColor);
            }
            return div;
        }
    }
}
