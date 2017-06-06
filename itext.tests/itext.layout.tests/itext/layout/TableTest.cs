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
using iText.IO.Util;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Xobject;
using iText.Kernel.Utils;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Renderer;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Layout {
    public class TableTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/layout/TableTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/layout/TableTest/";

        internal const String textContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
             + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.\n" + "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.\n";

        internal const String shortTextContent = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.";

        internal const String middleTextContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
             + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleTableTest01() {
            String testName = "tableTest01.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            Table table = new Table(new float[] { 50, 50 }).AddCell(new Cell().Add(new Paragraph("cell 1, 1"))).AddCell
                (new Cell().Add(new Paragraph("cell 1, 2")));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
            NUnit.Framework.Assert.AreEqual("Cell[row=0, col=0, rowspan=1, colspan=1]", table.GetCell(0, 0).ToString()
                );
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleTableTest02() {
            String testName = "tableTest02.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            Table table = new Table(new float[] { 50, 50 }).AddCell(new Cell().Add(new Paragraph("cell 1, 1"))).AddCell
                (new Cell().Add(new Paragraph("cell 1, 2"))).AddCell(new Cell().Add(new Paragraph("cell 2, 1"))).AddCell
                (new Cell().Add(new Paragraph("cell 2, 2"))).AddCell(new Cell().Add(new Paragraph("cell 3, 1"))).AddCell
                (new Cell());
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleTableTest03() {
            String testName = "tableTest03.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            String textContent1 = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.\n";
            String textContent2 = "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.\n"
                 + "Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.\n" + "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.\n";
            Table table = new Table(new float[] { 250, 250 }).AddCell(new Cell().Add(new Paragraph("cell 1, 1\n" + textContent1
                ))).AddCell(new Cell().Add(new Paragraph("cell 1, 2\n" + textContent1 + textContent2))).AddCell(new Cell
                ().Add(new Paragraph("cell 2, 1\n" + textContent2 + textContent1))).AddCell(new Cell().Add(new Paragraph
                ("cell 2, 2\n" + textContent2)));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleTableTest04() {
            String testName = "tableTest04.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            String textContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.\n" + "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.\n";
            Table table = new Table(new float[] { 250, 250 }).AddCell(new Cell().Add(new Paragraph("cell 1, 1\n" + textContent
                )));
            table.AddCell(new Cell(3, 1).Add(new Paragraph("cell 1, 2:3\n" + textContent + textContent + textContent))
                );
            table.AddCell(new Cell().Add(new Paragraph("cell 2, 1\n" + textContent)));
            table.AddCell(new Cell().Add(new Paragraph("cell 3, 1\n" + textContent)));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleTableTest05() {
            String testName = "tableTest05.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            String textContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.\n" + "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.\n";
            Table table = new Table(new float[] { 250, 250 }).AddCell(new Cell(3, 1).Add(new Paragraph("cell 1, 1:3\n"
                 + textContent + textContent + textContent))).AddCell(new Cell().Add(new Paragraph("cell 1, 2\n" + textContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 2, 2\n" + textContent))).AddCell(new Cell().Add(new Paragraph
                ("cell 3, 2\n" + textContent)));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleTableTest06() {
            String testName = "tableTest06.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            String textContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.\n" + "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.\n";
            Table table = new Table(new float[] { 250, 250 }).AddCell(new Cell().Add(new Paragraph("cell 1, 1\n" + textContent
                ))).AddCell(new Cell(3, 1).Add(new Paragraph("cell 1, 2:3\n" + textContent + textContent))).AddCell(new 
                Cell().Add(new Paragraph("cell 2, 1\n" + textContent))).AddCell(new Cell().Add(new Paragraph("cell 3, 1\n"
                 + textContent)));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleTableTest07() {
            String testName = "tableTest07.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            String textContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.\n" + "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.\n";
            Table table = new Table(new float[] { 250, 250 }).AddCell(new Cell(3, 1).Add(new Paragraph("cell 1, 1:3\n"
                 + textContent + textContent))).AddCell(new Cell().Add(new Paragraph("cell 1, 2\n" + textContent))).AddCell
                (new Cell().Add(new Paragraph("cell 2, 2\n" + textContent))).AddCell(new Cell().Add(new Paragraph("cell 3, 2\n"
                 + textContent)));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleTableTest08() {
            String testName = "tableTest08.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            String textContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.\n" + "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.\n";
            String shortTextContent = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.";
            String middleTextContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.";
            Table table = new Table(new float[] { 130, 130, 260 }).AddCell(new Cell(3, 2).Add(new Paragraph("cell 1:2, 1:3\n"
                 + textContent + textContent))).AddCell(new Cell().Add(new Paragraph("cell 1, 3\n" + textContent))).AddCell
                (new Cell().Add(new Paragraph("cell 2, 3\n" + textContent))).AddCell(new Cell().Add(new Paragraph("cell 3, 3\n"
                 + textContent))).AddCell(new Cell().Add(new Paragraph("cell 4, 1\n" + shortTextContent))).AddCell(new 
                Cell().Add(new Paragraph("cell 4, 2\n" + shortTextContent))).AddCell(new Cell().Add(new Paragraph("cell 4, 3\n"
                 + middleTextContent)));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleTableTest09() {
            String testName = "tableTest09.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            String textContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.\n" + "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.\n";
            String shortTextContent = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.";
            String middleTextContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.";
            Table table = new Table(new float[] { 130, 130, 260 }).AddCell(new Cell().Add(new Paragraph("cell 1, 1\n" 
                + shortTextContent))).AddCell(new Cell().Add(new Paragraph("cell 1, 2\n" + shortTextContent))).AddCell
                (new Cell().Add(new Paragraph("cell 1, 3\n" + middleTextContent))).AddCell(new Cell(3, 2).Add(new Paragraph
                ("cell 2:2, 1:3\n" + textContent + textContent))).AddCell(new Cell().Add(new Paragraph("cell 2, 3\n" +
                 middleTextContent))).AddCell(new Cell().Add(new Paragraph("cell 3, 3\n" + middleTextContent))).AddCell
                (new Cell().Add(new Paragraph("cell 4, 3\n" + middleTextContent))).AddCell(new Cell().Add(new Paragraph
                ("cell 5, 1\n" + shortTextContent))).AddCell(new Cell().Add(new Paragraph("cell 5, 2\n" + shortTextContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 5, 3\n" + middleTextContent)));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleTableTest10() {
            String testName = "tableTest10.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            doc.Add(new Paragraph("Table 1"));
            Table table = new Table(new float[] { 100, 100 }).AddCell(new Cell().Add(new Paragraph("1, 1"))).AddCell(new 
                Cell().Add(new Paragraph("1, 2"))).AddCell(new Cell().Add(new Paragraph("2, 1"))).AddCell(new Cell().Add
                (new Paragraph("2, 2")));
            doc.Add(table);
            doc.Add(new Paragraph("Table 2"));
            Table table2 = new Table(new float[] { 50, 50 }).AddCell(new Cell().Add(new Paragraph("1, 1"))).AddCell(new 
                Cell().Add(new Paragraph("1, 2"))).AddCell(new Cell().Add(new Paragraph("2, 1"))).AddCell(new Cell().Add
                (new Paragraph("2, 2")));
            doc.Add(table2);
            doc.Add(new Paragraph("Table 3"));
            PdfImageXObject xObject = new PdfImageXObject(ImageDataFactory.CreatePng(UrlUtil.ToURL(sourceFolder + "itext.png"
                )));
            iText.Layout.Element.Image image = new iText.Layout.Element.Image(xObject, 50);
            Table table3 = new Table(new float[] { 100, 100 }).AddCell(new Cell().Add(new Paragraph("1, 1"))).AddCell(
                new Cell().Add(image)).AddCell(new Cell().Add(new Paragraph("2, 1"))).AddCell(new Cell().Add(new Paragraph
                ("2, 2")));
            doc.Add(table3);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleTableTest11() {
            String testName = "tableTest11.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            String shortTextContent = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.";
            String middleTextContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.";
            Table table = new Table(new float[] { 250, 250 }).AddCell(new Cell().Add(new Paragraph("cell 1, 1\n" + middleTextContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 1, 2\n" + shortTextContent))).AddCell(new Cell().Add(new 
                Paragraph("cell 2, 1\n" + shortTextContent))).AddCell(new Cell().Add(new Paragraph("cell 2, 2\n" + middleTextContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 3, 1\n" + shortTextContent))).AddCell(new Cell().Add(new 
                Paragraph("cell 3, 2\n" + middleTextContent))).AddCell(new Cell().Add(new Paragraph("cell 4, 1\n" + shortTextContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 4, 2\n" + shortTextContent))).AddCell(new Cell().SetKeepTogether
                (true).Add(new Paragraph("cell 5, 1\n" + middleTextContent))).AddCell(new Cell().SetKeepTogether(true)
                .Add(new Paragraph("cell 5, 2\n" + shortTextContent))).AddCell(new Cell().SetKeepTogether(true).Add(new 
                Paragraph("cell 6, 1\n" + middleTextContent))).AddCell(new Cell().SetKeepTogether(true).Add(new Paragraph
                ("cell 6, 2\n" + shortTextContent))).AddCell(new Cell().SetKeepTogether(true).Add(new Paragraph("cell 7, 1\n"
                 + middleTextContent))).AddCell(new Cell().SetKeepTogether(true).Add(new Paragraph("cell 7, 2\n" + middleTextContent
                )));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleTableTest12() {
            String testName = "tableTest12.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            String shortTextContent = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.";
            String middleTextContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.";
            Table table = new Table(new float[] { 250, 250 }).AddCell(new Cell().Add(new Paragraph("cell 1, 1\n" + middleTextContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 1, 2\n" + shortTextContent))).AddCell(new Cell().Add(new 
                Paragraph("cell 2, 1\n" + shortTextContent))).AddCell(new Cell().Add(new Paragraph("cell 2, 2\n" + middleTextContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 3, 1\n" + shortTextContent))).AddCell(new Cell().Add(new 
                Paragraph("cell 3, 2\n" + middleTextContent))).AddCell(new Cell().Add(new Paragraph("cell 4, 1\n" + shortTextContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 4, 2\n" + shortTextContent))).AddCell(new Cell().SetKeepTogether
                (true).Add(new Paragraph("cell 5, 1\n" + middleTextContent))).AddCell(new Cell().SetKeepTogether(true)
                .Add(new Paragraph("cell 5, 2\n" + shortTextContent))).AddCell(new Cell().SetKeepTogether(true).Add(new 
                Paragraph("cell 6, 1\n" + middleTextContent))).AddCell(new Cell().SetKeepTogether(true).Add(new Paragraph
                ("cell 6, 2\n" + shortTextContent))).AddCell(new Cell().SetKeepTogether(true).Add(new Paragraph("cell 7, 1\n"
                 + middleTextContent))).AddCell(new Cell().SetKeepTogether(true).Add(new Paragraph("cell 7, 2\n" + middleTextContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 8, 1\n" + middleTextContent))).AddCell(new Cell().Add(new 
                Paragraph("cell 8, 2\n" + shortTextContent))).AddCell(new Cell().Add(new Paragraph("cell 9, 1\n" + shortTextContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 9, 2\n" + middleTextContent))).AddCell(new Cell().Add(new 
                Paragraph("cell 10, 1\n" + shortTextContent))).AddCell(new Cell().Add(new Paragraph("cell 10, 2\n" + middleTextContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 11, 1\n" + shortTextContent))).AddCell(new Cell().Add(new 
                Paragraph("cell 11, 2\n" + shortTextContent)));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleTableTest13() {
            String testName = "tableTest13.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            String shortTextContent = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.";
            String middleTextContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.";
            Table table = new Table(new float[] { 250, 250 }).AddCell(new Cell().Add(new Paragraph("cell 1, 1\n" + middleTextContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 1, 2\n" + shortTextContent))).AddCell(new Cell().Add(new 
                Paragraph("cell 2, 1\n" + shortTextContent))).AddCell(new Cell().Add(new Paragraph("cell 2, 2\n" + middleTextContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 3, 1\n" + shortTextContent))).AddCell(new Cell().Add(new 
                Paragraph("cell 3, 2\n" + middleTextContent))).AddCell(new Cell().Add(new Paragraph("cell 4, 1\n" + shortTextContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 4, 2\n" + shortTextContent))).AddCell(new Cell().Add(new 
                Paragraph("cell 5, 1\n" + middleTextContent))).AddCell(new Cell().Add(new Paragraph("cell 5, 2\n" + shortTextContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 6, 1\n" + middleTextContent))).AddCell(new Cell().Add(new 
                Paragraph("cell 6, 2\n" + shortTextContent))).AddCell(new Cell().Add(new Paragraph("cell 7, 1\n" + middleTextContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 7, 2\n" + middleTextContent)));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleTableTest14() {
            String testName = "tableTest14.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            String textContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.\n" + "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.\n";
            String shortTextContent = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.";
            String middleTextContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.";
            Table table = new Table(new float[] { 130, 130, 260 }).AddCell(new Cell(3, 2).Add(new Paragraph("cell 1:2, 1:3\n"
                 + textContent + textContent))).AddCell(new Cell().Add(new Paragraph("cell 1, 3\n" + textContent))).AddCell
                (new Cell().Add(new Paragraph("cell 2, 3\n" + textContent))).AddCell(new Cell().Add(new Paragraph("cell 3, 3\n"
                 + textContent))).AddCell(new Cell().Add(new Paragraph("cell 4, 1\n" + shortTextContent))).AddCell(new 
                Cell().Add(new Paragraph("cell 4, 2\n" + shortTextContent))).AddCell(new Cell().Add(new Paragraph("cell 4, 3\n"
                 + middleTextContent))).AddCell(new Cell().Add(new Paragraph("cell 5, 1\n" + shortTextContent))).AddCell
                (new Cell().Add(new Paragraph("cell 5, 2\n" + shortTextContent))).AddCell(new Cell().Add(new Paragraph
                ("cell 5, 3\n" + middleTextContent))).AddCell(new Cell().Add(new Paragraph("cell 6, 1\n" + middleTextContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 6, 2\n" + middleTextContent))).AddCell(new Cell().Add(new 
                Paragraph("cell 6, 3\n" + middleTextContent)));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleTableTest15() {
            String testName = "tableTest15.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            String textContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.\n" + "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.\n";
            String shortTextContent = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.";
            String middleTextContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.";
            Table table = new Table(new float[] { 130, 130, 260 }).AddCell(new Cell().Add(new Paragraph("cell 1, 1\n" 
                + shortTextContent))).AddCell(new Cell().Add(new Paragraph("cell 1, 2\n" + shortTextContent))).AddCell
                (new Cell().Add(new Paragraph("cell 1, 3\n" + middleTextContent))).AddCell(new Cell().Add(new Paragraph
                ("cell 2, 1\n" + shortTextContent))).AddCell(new Cell().Add(new Paragraph("cell 2, 2\n" + shortTextContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 2, 3\n" + middleTextContent))).AddCell(new Cell(3, 2).Add
                (new Paragraph("cell 3:2, 1:3\n" + textContent + textContent))).AddCell(new Cell().Add(new Paragraph("cell 3, 3\n"
                 + textContent))).AddCell(new Cell().Add(new Paragraph("cell 4, 3\n" + textContent))).AddCell(new Cell
                ().Add(new Paragraph("cell 5, 3\n" + textContent))).AddCell(new Cell().Add(new Paragraph("cell 6, 1\n"
                 + shortTextContent))).AddCell(new Cell().Add(new Paragraph("cell 6, 2\n" + shortTextContent))).AddCell
                (new Cell().Add(new Paragraph("cell 6, 3\n" + middleTextContent))).AddCell(new Cell().Add(new Paragraph
                ("cell 7, 1\n" + middleTextContent))).AddCell(new Cell().Add(new Paragraph("cell 7, 2\n" + middleTextContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 7, 3\n" + middleTextContent)));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleTableTest16() {
            String testName = "tableTest16.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            String textContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.\n" + "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.\n";
            String middleTextContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.";
            String longTextContent = "1. " + textContent + "2. " + textContent + "3. " + textContent + "4. " + textContent
                 + "5. " + textContent + "6. " + textContent + "7. " + textContent + "8. " + textContent + "9. " + textContent;
            Table table = new Table(new float[] { 250, 250 }).AddCell(new Cell().Add(new Paragraph("cell 1, 1\n" + longTextContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 1, 2\n" + middleTextContent)).SetBorder(new SolidBorder
                (Color.RED, 2))).AddCell(new Cell().Add(new Paragraph("cell 2, 1\n" + middleTextContent + middleTextContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 2, 2\n" + longTextContent)));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA, Count = 1)]
        public virtual void SimpleTableTest17() {
            String testName = "tableTest17.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            Table table = new Table(new float[] { 50, 50, 50 }).AddCell(new Cell().Add(new Paragraph("cell 1, 1"))).AddCell
                (new Cell().Add(new Paragraph("cell 1, 2"))).AddCell(new Cell().Add(new Paragraph("cell 1, 3")));
            String longText = "Long text, very long text. ";
            for (int i = 0; i < 4; i++) {
                longText += longText;
            }
            table.AddCell(new Cell().Add(new Paragraph("cell 2.1\n" + longText).SetKeepTogether(true)));
            table.AddCell("cell 2.2\nShort text.");
            table.AddCell(new Cell().Add(new Paragraph("cell 2.3\n" + longText)));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA, Count = 1)]
        public virtual void SimpleTableTest18() {
            String testName = "tableTest18.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            doc.Add(new Paragraph(textContent));
            Table table = new Table(new float[] { 50, 50, 50 }).AddCell(new Cell().Add(new Paragraph("cell 1, 1"))).AddCell
                (new Cell().Add(new Paragraph("cell 1, 2"))).AddCell(new Cell().Add(new Paragraph("cell 1, 3")));
            String longText = "Long text, very long text. ";
            for (int i = 0; i < 4; i++) {
                longText += longText;
            }
            table.AddCell(new Cell().Add(new Paragraph("cell 2.1\n" + longText).SetKeepTogether(true)));
            table.AddCell("cell 2.2\nShort text.");
            table.AddCell(new Cell().Add(new Paragraph("cell 2.3\n" + longText)));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA, Count = 1)]
        public virtual void SimpleTableTest19() {
            String testName = "tableTest19.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            Table table = new Table(new float[] { 130, 130, 260 }).AddCell(new Cell(3, 2).Add(new Paragraph("cell 1:2, 1:3\n"
                 + textContent + textContent))).AddCell(new Cell().Add(new Paragraph("cell 1, 3\n" + textContent))).AddCell
                (new Cell().Add(new Paragraph("cell 2, 3\n" + textContent))).AddCell(new Cell().Add(new Paragraph("cell 3, 3\n"
                 + textContent))).AddCell(new Cell().Add(new iText.Layout.Element.Image(ImageDataFactory.Create(sourceFolder
                 + "red.png")))).AddCell(new Cell().Add(new Paragraph("cell 4, 2\n" + shortTextContent))).AddCell(new 
                Cell().Add(new Paragraph("cell 4, 3\n" + middleTextContent))).AddCell(new Cell().Add(new Paragraph("cell 5, 1\n"
                 + shortTextContent))).AddCell(new Cell().Add(new Paragraph("cell 5, 2\n" + shortTextContent))).AddCell
                (new Cell().Add(new Paragraph("cell 5, 3\n" + middleTextContent))).AddCell(new Cell().Add(new Paragraph
                ("cell 6, 1\n" + middleTextContent))).AddCell(new Cell().Add(new Paragraph("cell 6, 2\n" + middleTextContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 6, 3\n" + middleTextContent)));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA, Count = 1)]
        public virtual void SimpleTableTest20() {
            String testName = "tableTest20.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            Table table = new Table(new float[] { 130, 130, 260 }).AddCell(new Cell().Add(new iText.Layout.Element.Image
                (ImageDataFactory.Create(sourceFolder + "red.png")))).AddCell(new Cell().Add(new Paragraph("cell 4, 2\n"
                 + shortTextContent))).AddCell(new Cell().Add(new Paragraph("cell 4, 3\n" + middleTextContent))).AddCell
                (new Cell().Add(new Paragraph("cell 5, 1\n" + shortTextContent))).AddCell(new Cell().Add(new Paragraph
                ("cell 5, 2\n" + shortTextContent))).AddCell(new Cell().Add(new Paragraph("cell 5, 3\n" + middleTextContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 6, 1\n" + middleTextContent))).AddCell(new Cell().Add(new 
                Paragraph("cell 6, 2\n" + middleTextContent))).AddCell(new Cell().Add(new Paragraph("cell 6, 3\n" + middleTextContent
                )));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA, Count = 1)]
        public virtual void SimpleTableTest21() {
            String testName = "tableTest21.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            String textContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.\n" + "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.\n";
            String shortTextContent = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.";
            String middleTextContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.";
            doc.Add(new Paragraph(textContent));
            Table table = new Table(new float[] { 130, 130, 260 }).AddCell(new Cell().Add(new iText.Layout.Element.Image
                (ImageDataFactory.Create(sourceFolder + "red.png")))).AddCell(new Cell().Add(new Paragraph("cell 4, 2\n"
                 + shortTextContent))).AddCell(new Cell().Add(new Paragraph("cell 4, 3\n" + middleTextContent))).AddCell
                (new Cell().Add(new Paragraph("cell 5, 1\n" + shortTextContent))).AddCell(new Cell().Add(new Paragraph
                ("cell 5, 2\n" + shortTextContent))).AddCell(new Cell().Add(new Paragraph("cell 5, 3\n" + middleTextContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 6, 1\n" + middleTextContent))).AddCell(new Cell().Add(new 
                Paragraph("cell 6, 2\n" + middleTextContent))).AddCell(new Cell().Add(new Paragraph("cell 6, 3\n" + middleTextContent
                )));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleTableTest22() {
            String testName = "tableTest22.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            Table table = new Table(new UnitValue[] { UnitValue.CreatePointValue(30), UnitValue.CreatePointValue(30), 
                UnitValue.CreatePercentValue(30), UnitValue.CreatePercentValue(30) }).AddCell(new Cell().Add(new Paragraph
                ("cell 1, 1"))).AddCell(new Cell().Add(new Paragraph("cell 1, 2"))).AddCell(new Cell().Add(new Paragraph
                ("cell 1, 3"))).AddCell(new Cell().Add(new Paragraph("cell 1, 4")));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BigRowspanTest01() {
            String testName = "bigRowspanTest01.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            String textContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.\n" + "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.\n";
            String middleTextContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.";
            String longTextContent = "1. " + textContent + "2. " + textContent + "3. " + textContent + "4. " + textContent
                 + "5. " + textContent + "6. " + textContent + "7. " + textContent + "8. " + textContent + "9. " + textContent;
            Table table = new Table(new float[] { 250, 250 }).AddCell(new Cell().Add(new Paragraph("cell 1, 1\n" + textContent
                ))).AddCell(new Cell(5, 1).Add(new Paragraph("cell 1, 2\n" + longTextContent))).AddCell(new Cell().Add
                (new Paragraph("cell 2, 1\n" + middleTextContent))).AddCell(new Cell().Add(new Paragraph("cell 3, 1\n"
                 + middleTextContent))).AddCell(new Cell().Add(new Paragraph("cell 4, 1\n" + middleTextContent))).AddCell
                (new Cell().Add(new Paragraph("cell 5, 1\n" + middleTextContent)));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BigRowspanTest02() {
            String testName = "bigRowspanTest02.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            String textContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.\n" + "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.\n";
            String middleTextContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.";
            String longTextContent = "1. " + textContent + "2. " + textContent + "3. " + textContent + "4. " + textContent
                 + "5. " + textContent + "6. " + textContent + "7. " + textContent + "8. " + textContent + "9. " + textContent;
            Table table = new Table(new float[] { 250, 250 }).AddCell(new Cell().Add(new Paragraph("cell 1, 1\n" + textContent
                ))).AddCell(new Cell(5, 1).Add(new Paragraph("cell 1, 2\n" + longTextContent))).AddCell(new Cell().Add
                (new Paragraph("cell 2, 1\n" + textContent))).AddCell(new Cell().Add(new Paragraph("cell 3, 1\n" + textContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 4, 1\n" + textContent))).AddCell(new Cell().Add(new Paragraph
                ("cell 5, 1\n" + textContent)));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BigRowspanTest03() {
            String testName = "bigRowspanTest03.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            String textContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.\n" + "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.\n";
            String middleTextContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.";
            Table table = new Table(new float[] { 250, 250 }).AddCell(new Cell().Add(new Paragraph("cell 1, 1\n" + textContent
                ))).AddCell(new Cell(5, 1).Add(new Paragraph("cell 1, 2\n" + middleTextContent))).AddCell(new Cell().Add
                (new Paragraph("cell 2, 1\n" + textContent))).AddCell(new Cell().Add(new Paragraph("cell 3, 1\n" + textContent
                ))).AddCell(new Cell().Add(new Paragraph("cell 4, 1\n" + textContent))).AddCell(new Cell().Add(new Paragraph
                ("cell 5, 1\n" + textContent)));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BigRowspanTest04() {
            String testName = "bigRowspanTest04.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            String textContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.\n" + "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.\n";
            String middleTextContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.";
            String longTextContent = "1. " + textContent + "2. " + textContent + "3. " + textContent + "4. " + textContent
                 + "5. " + textContent + "6. " + textContent + "7. " + textContent + "8. " + textContent + "9. " + textContent;
            Table table = new Table(new float[] { 250, 250 }).AddCell(new Cell().Add(new Paragraph("cell 1, 1\n" + textContent
                ))).AddCell(new Cell(5, 1).Add(new Paragraph("cell 1, 2\n" + longTextContent))).AddCell(new Cell().Add
                (new Paragraph("cell 2, 1\n" + textContent))).AddCell(new Cell().Add(new Paragraph("cell 3, 1\n" + textContent
                ))).AddCell(new Cell().SetKeepTogether(true).Add(new Paragraph("cell 4, 1\n" + textContent))).AddCell(
                new Cell().Add(new Paragraph("cell 5, 1\n" + textContent)));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BigRowspanTest05() {
            String testName = "bigRowspanTest05.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            String textContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.\n"
                 + "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.\n" + "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.\n";
            String longTextContent = "1. " + textContent + "2. " + textContent + "3. " + textContent + "4. " + textContent
                 + "5. " + textContent + "6. " + textContent + "7. " + textContent + "8. " + textContent + "9. " + textContent;
            Table table = new Table(new float[] { 250, 250 }).AddCell(new Cell().Add(new Paragraph("cell 1, 1\n" + textContent
                ))).AddCell(new Cell(2, 1).Add(new Paragraph("cell 1, 1 and 2\n" + longTextContent))).AddCell(new Cell
                ().Add(new Paragraph("cell 2, 1\n" + textContent))).AddCell(new Cell().Add(new Paragraph("cell 3, 1\n"
                 + textContent))).AddCell(new Cell().Add(new Paragraph("cell 3, 2\n" + textContent)));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BigRowspanTest06() {
            String testName = "bigRowspanTest06.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            Table table = new Table(2).AddCell(new Cell(2, 1).Add("col 1 row 2")).AddCell(new Cell(2, 1).Add("col 2 row 2"
                )).AddCell(new Cell(1, 1).Add("col 1 row 3")).AddCell(new Cell(1, 1).Add("col 2 row 3"));
            table.SetBorderTop(new SolidBorder(Color.GREEN, 50)).SetBorderBottom(new SolidBorder(Color.ORANGE, 40));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BigRowspanTest07() {
            String testName = "bigRowspanTest07.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            Table table = new Table(2);
            for (int i = 0; i < 100; i++) {
                Cell cell = new Cell();
                cell.Add("Cell " + i);
                Cell cell2 = new Cell(2, 1);
                cell2.Add("Cell with Rowspan");
                Cell cell3 = new Cell();
                cell3.Add("Cell " + i + ".2");
                table.AddCell(cell);
                table.AddCell(cell2);
                table.AddCell(cell3);
            }
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DifferentPageOrientationTest01() {
            String testName = "differentPageOrientationTest01.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            String textContent1 = "Video provides a powerful way to help you prove your point. When you click Online Video, you can paste in the embed code for the video you want to add. You can also type a keyword to search online for the video that best fits your document.";
            String textContent2 = "To make your document look professionally produced, Word provides header, footer, cover page, and text box designs that complement each other. For example, you can add a matching cover page, header, and sidebar. Click Insert and then choose the elements you want from the different galleries.";
            String textContent3 = "Themes and styles also help keep your document coordinated. When you click Design and choose a new Theme, the pictures, charts, and SmartArt graphics change to match your new theme. When you apply styles, your headings change to match the new theme.";
            Table table = new Table(3);
            for (int i = 0; i < 20; i++) {
                table.AddCell(new Cell().Add(new Paragraph(textContent1))).AddCell(new Cell().Add(new Paragraph(textContent3
                    ))).AddCell(new Cell().Add(new Paragraph(textContent2))).AddCell(new Cell().Add(new Paragraph(textContent3
                    ))).AddCell(new Cell().Add(new Paragraph(textContent2))).AddCell(new Cell().Add(new Paragraph(textContent1
                    ))).AddCell(new Cell().Add(new Paragraph(textContent2))).AddCell(new Cell().Add(new Paragraph(textContent1
                    ))).AddCell(new Cell().Add(new Paragraph(textContent3)));
            }
            doc.SetRenderer(new TableTest.RotatedDocumentRenderer(doc, pdfDoc));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        private class RotatedDocumentRenderer : DocumentRenderer {
            private readonly PdfDocument pdfDoc;

            public RotatedDocumentRenderer(Document doc, PdfDocument pdfDoc)
                : base(doc) {
                this.pdfDoc = pdfDoc;
            }

            protected internal override PageSize AddNewPage(PageSize customPageSize) {
                PageSize pageSize = currentPageNumber % 2 == 1 ? PageSize.A4 : PageSize.A4.Rotate();
                pdfDoc.AddNewPage(pageSize);
                return pageSize;
            }
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ExtendLastRowTest01() {
            String testName = "extendLastRowTest01.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            PdfImageXObject xObject = new PdfImageXObject(ImageDataFactory.CreatePng(UrlUtil.ToURL(sourceFolder + "itext.png"
                )));
            iText.Layout.Element.Image image = new iText.Layout.Element.Image(xObject, 100);
            Table table = new Table(2);
            for (int i = 0; i < 20; i++) {
                table.AddCell(image);
            }
            doc.Add(new Paragraph("Extend the last row on each page"));
            table.SetExtendBottomRow(true);
            doc.Add(table);
            doc.Add(new Paragraph("Extend all last rows on each page except final one"));
            table.SetExtendBottomRow(false);
            table.SetExtendBottomRowOnSplit(true);
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [LogMessage(iText.IO.LogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA, Count = 1)]
        [NUnit.Framework.Test]
        public virtual void ToLargeElementWithKeepTogetherPropertyInTableTest01() {
            String testName = "toLargeElementWithKeepTogetherPropertyInTableTest01.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            Table table = new Table(1).SetWidth(UnitValue.CreatePercentValue(100)).SetFixedLayout();
            Cell cell = new Cell();
            String str = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            String result = "";
            for (int i = 0; i < 53; i++) {
                result += str;
            }
            Paragraph p = new Paragraph(new Text(result));
            p.SetProperty(Property.KEEP_TOGETHER, true);
            cell.Add(p);
            table.AddCell(cell);
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [LogMessage(iText.IO.LogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA, Count = 1)]
        [NUnit.Framework.Test]
        public virtual void ToLargeElementInTableTest01() {
            String testName = "toLargeElementInTableTest01.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(destinationFolder + "toLargeElementInTableTest01.pdf"));
            Document doc = new Document(pdfDoc);
            Table table = new Table(new float[] { 5 });
            table.SetWidth(5).SetProperty(Property.TABLE_LAYOUT, "fixed");
            Cell cell = new Cell();
            Paragraph p = new Paragraph(new Text("a"));
            cell.Add(p);
            table.AddCell(cell);
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void NestedTablesCollapseTest01() {
            String testName = "nestedTablesCollapseTest01.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            Cell cell;
            Table outertable = new Table(1);
            Table innertable = new Table(2);
            // first row
            // column 1
            cell = new Cell().Add("Record Ref:");
            cell.SetBorder(Border.NO_BORDER);
            innertable.AddCell(cell);
            // column 2
            cell = new Cell().Add("GN Staff");
            cell.SetPaddingLeft(2);
            innertable.AddCell(cell);
            // spacing
            cell = new Cell(1, 2);
            cell.SetHeight(3);
            cell.SetBorder(Border.NO_BORDER);
            innertable.AddCell(cell);
            // second row
            // column 1
            cell = new Cell().Add("Hospital:");
            cell.SetBorder(Border.NO_BORDER);
            innertable.AddCell(cell);
            // column 2
            cell = new Cell().Add("Derby Royal");
            cell.SetPaddingLeft(2);
            innertable.AddCell(cell);
            // spacing
            cell = new Cell(1, 2);
            cell.SetHeight(3);
            cell.SetBorder(Border.NO_BORDER);
            innertable.AddCell(cell);
            // first nested table
            cell = new Cell().Add(innertable);
            outertable.AddCell(cell);
            // add the table
            doc.Add(outertable);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void NestedTableSkipHeaderFooterTest() {
            String testName = "nestedTableSkipHeaderFooter.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc, PageSize.A4.Rotate());
            Table table = new Table(5);
            table.AddHeaderCell(new Cell(1, 5).Add(new Paragraph("Table XYZ (Continued)")));
            table.AddFooterCell(new Cell(1, 5).Add(new Paragraph("Continue on next page")));
            table.SetSkipFirstHeader(true);
            table.SetSkipLastFooter(true);
            for (int i = 0; i < 350; i++) {
                table.AddCell(new Cell().Add(new Paragraph((i + 1).ToString())));
            }
            Table t = new Table(1);
            t.AddCell(new Cell().SetBorder(new SolidBorder(Color.RED, 1)).SetPaddings(3, 3, 3, 3).Add(table));
            doc.Add(t);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void NestedTablesWithMarginsTest01() {
            String testName = "nestedTablesWithMarginsTest01.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc, PageSize.A8.Rotate());
            Table innerTable = new Table(1);
            for (int i = 0; i < 4; i++) {
                innerTable.AddCell(new Cell().Add("Hello" + i));
            }
            Table outerTable = new Table(1).AddCell(new Cell().Add(innerTable));
            outerTable.SetMarginTop(10);
            doc.Add(outerTable);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [LogMessage(iText.IO.LogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA, Count = 1)]
        [NUnit.Framework.Test]
        public virtual void SplitTableOnShortPage() {
            String testName = "splitTableOnShortPage.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc, new PageSize(300, 98));
            doc.Add(new Paragraph("Table with setKeepTogether(true):"));
            Table table = new Table(3);
            table.SetKeepTogether(true);
            Cell cell = new Cell(3, 1);
            cell.Add("G");
            cell.Add("R");
            cell.Add("P");
            table.AddCell(cell);
            table.AddCell("middle row 1");
            cell = new Cell(3, 1);
            cell.Add("A");
            cell.Add("B");
            cell.Add("C");
            table.AddCell(cell);
            table.AddCell("middle row 2");
            table.AddCell("middle row 3");
            doc.Add(table);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Table with setKeepTogether(false):"));
            table.SetKeepTogether(false);
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SplitCellWithStyles() {
            String testName = "splitCellWithStyles.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            String text = "Make Gretzky Great Again";
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc, PageSize.A7);
            Table table = new Table(2).SetBorder(Border.NO_BORDER).SetMarginTop(10).SetMarginBottom(10);
            Style cellStyle = new Style();
            cellStyle.SetBorderLeft(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderTop(new SolidBorder(Color
                .BLUE, 1)).SetBorderBottom(new SolidBorder(Color.BLUE, 1));
            for (int i = 0; i < 10; i++) {
                table.AddCell(new Cell().Add(iText.IO.Util.JavaUtil.IntegerToString(i)).AddStyle(cellStyle));
                table.AddCell(new Cell().Add(text).AddStyle(cellStyle));
            }
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ImageInTableTest_HA() {
            String testName = "imageInTableTest_HA.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            PdfImageXObject xObject = new PdfImageXObject(ImageDataFactory.CreatePng(UrlUtil.ToURL(sourceFolder + "itext.png"
                )));
            iText.Layout.Element.Image imageL = new iText.Layout.Element.Image(xObject);
            imageL.SetHorizontalAlignment(HorizontalAlignment.LEFT);
            iText.Layout.Element.Image imageC = new iText.Layout.Element.Image(xObject);
            imageC.SetHorizontalAlignment(HorizontalAlignment.CENTER);
            iText.Layout.Element.Image imageR = new iText.Layout.Element.Image(xObject);
            imageR.SetHorizontalAlignment(HorizontalAlignment.RIGHT);
            doc.Add(new Paragraph("Table"));
            Table table = new Table(1).AddCell(new Cell().Add(imageL)).AddCell(new Cell().Add(imageC)).AddCell(new Cell
                ().Add(imageR));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CellAlignmentAndSplittingTest01() {
            String testName = "cellAlignmentAndSplittingTest01.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            Table table = new Table(1);
            for (int i = 0; i < 20; i++) {
                table.AddCell(new Cell().Add(i + " Liberté!\nÉgalité!\nFraternité!").SetHeight(100).SetVerticalAlignment(VerticalAlignment
                    .MIDDLE));
            }
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CellAlignmentAndKeepTogetherTest01() {
            String testName = "cellAlignmentAndKeepTogetherTest01.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            Table table = new Table(1);
            for (int i = 0; i < 20; i++) {
                table.AddCell(new Cell().Add(i + " Liberté!\nÉgalité!\nFraternité!").SetHeight(100).SetKeepTogether(true).
                    SetVerticalAlignment(VerticalAlignment.MIDDLE));
            }
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [LogMessage(iText.IO.LogMessageConstant.CLIP_ELEMENT, Count = 3)]
        [NUnit.Framework.Test]
        public virtual void TableWithSetHeightProperties01() {
            String testName = "tableWithSetHeightProperties01.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            String textByron = "When a man hath no freedom to fight for at home,\n" + "    Let him combat for that of his neighbours;\n"
                 + "Let him think of the glories of Greece and of Rome,\n" + "    And get knocked on the head for his labours.\n";
            doc.Add(new Paragraph("Default layout:"));
            Table table = new Table(3).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.GREEN, 1))).AddCell
                (new Cell(1, 2).Add(textByron).SetBorder(new SolidBorder(Color.YELLOW, 3))).AddCell(new Cell(2, 1).Add
                (textByron).SetBorder(new SolidBorder(Color.RED, 5))).AddCell(new Cell(2, 1).Add(textByron).SetBorder(
                new SolidBorder(Color.GRAY, 7))).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.BLUE
                , 12))).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.CYAN, 1)));
            table.SetBorder(new SolidBorder(Color.GREEN, 2));
            doc.Add(table);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Table's height is bigger than needed:"));
            table = new Table(3).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.GREEN, 1))).AddCell
                (new Cell(1, 2).Add(textByron).SetBorder(new SolidBorder(Color.YELLOW, 3))).AddCell(new Cell(2, 1).Add
                (textByron).SetBorder(new SolidBorder(Color.RED, 5))).AddCell(new Cell(2, 1).Add(textByron).SetBorder(
                new SolidBorder(Color.GRAY, 7))).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.BLUE
                , 12))).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.CYAN, 1)));
            table.SetBorder(new SolidBorder(Color.GREEN, 2));
            table.SetHeight(1700);
            doc.Add(table);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Table's height is shorter than needed:"));
            table = new Table(3).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.GREEN, 1))).AddCell
                (new Cell(1, 2).Add(textByron).SetBorder(new SolidBorder(Color.YELLOW, 3))).AddCell(new Cell(2, 1).Add
                (textByron).SetBorder(new SolidBorder(Color.RED, 5))).AddCell(new Cell(2, 1).Add(textByron).SetBorder(
                new SolidBorder(Color.GRAY, 7))).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.BLUE
                , 12))).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.CYAN, 1)));
            table.SetBorder(new SolidBorder(Color.GREEN, 2));
            table.SetHeight(200);
            doc.Add(table);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Some cells' heights are set:"));
            table = new Table(3).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.GREEN, 1))).AddCell
                (new Cell(1, 2).Add(textByron).SetBorder(new SolidBorder(Color.YELLOW, 3)).SetHeight(300)).AddCell(new 
                Cell().Add(textByron).SetBorder(new SolidBorder(Color.GREEN, 1))).AddCell(new Cell(1, 2).Add(textByron
                ).SetBorder(new SolidBorder(Color.YELLOW, 3)).SetHeight(40)).AddCell(new Cell(2, 1).Add(textByron).SetBorder
                (new SolidBorder(Color.RED, 5))).AddCell(new Cell(2, 1).Add(textByron).SetBorder(new SolidBorder(Color
                .GRAY, 7))).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.BLUE, 12))).AddCell(new 
                Cell().Add(textByron).SetBorder(new SolidBorder(Color.CYAN, 1)).SetHeight(20));
            table.SetBorder(new SolidBorder(Color.GREEN, 2));
            table.SetHeight(1700);
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [LogMessage(iText.IO.LogMessageConstant.CLIP_ELEMENT, Count = 3)]
        [NUnit.Framework.Test]
        public virtual void TableWithSetHeightProperties02() {
            String testName = "tableWithSetHeightProperties02.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            String textByron = "When a man hath no freedom to fight for at home,\n" + "    Let him combat for that of his neighbours;\n"
                 + "Let him think of the glories of Greece and of Rome,\n" + "    And get knocked on the head for his labours.\n";
            doc.Add(new Paragraph("Default layout:"));
            Table table = new Table(3).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.GREEN, 1))).AddCell
                (new Cell(1, 2).Add(textByron).SetBorder(new SolidBorder(Color.YELLOW, 3))).AddCell(new Cell(2, 1).Add
                (textByron).SetBorder(new SolidBorder(Color.RED, 5))).AddCell(new Cell(2, 1).Add(textByron).SetBorder(
                new SolidBorder(Color.GRAY, 7))).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.BLUE
                , 12))).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.CYAN, 1)));
            table.SetBorder(new SolidBorder(Color.GREEN, 2));
            doc.Add(table);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Table's max height is bigger than needed:"));
            table = new Table(3).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.GREEN, 1))).AddCell
                (new Cell(1, 2).Add(textByron).SetBorder(new SolidBorder(Color.YELLOW, 3))).AddCell(new Cell(2, 1).Add
                (textByron).SetBorder(new SolidBorder(Color.RED, 5))).AddCell(new Cell(2, 1).Add(textByron).SetBorder(
                new SolidBorder(Color.GRAY, 7))).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.BLUE
                , 12))).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.CYAN, 1)));
            table.SetBorder(new SolidBorder(Color.GREEN, 2));
            table.SetMaxHeight(1300);
            doc.Add(table);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Table's max height is shorter than needed:"));
            table = new Table(3).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.GREEN, 1))).AddCell
                (new Cell(1, 2).Add(textByron).SetBorder(new SolidBorder(Color.YELLOW, 3))).AddCell(new Cell(2, 1).Add
                (textByron).SetBorder(new SolidBorder(Color.RED, 5))).AddCell(new Cell(2, 1).Add(textByron).SetBorder(
                new SolidBorder(Color.GRAY, 7))).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.BLUE
                , 12))).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.CYAN, 1)));
            table.SetBorder(new SolidBorder(Color.GREEN, 2));
            table.SetMaxHeight(300);
            doc.Add(table);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Table's min height is bigger than needed:"));
            table = new Table(3).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.GREEN, 1))).AddCell
                (new Cell(1, 2).Add(textByron).SetBorder(new SolidBorder(Color.YELLOW, 3))).AddCell(new Cell(2, 1).Add
                (textByron).SetBorder(new SolidBorder(Color.RED, 5))).AddCell(new Cell(2, 1).Add(textByron).SetBorder(
                new SolidBorder(Color.GRAY, 7))).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.BLUE
                , 12))).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.CYAN, 1)));
            table.SetBorder(new SolidBorder(Color.GREEN, 2));
            table.SetMinHeight(1300);
            doc.Add(table);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Table's min height is shorter than needed:"));
            table = new Table(3).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.GREEN, 1))).AddCell
                (new Cell(1, 2).Add(textByron).SetBorder(new SolidBorder(Color.YELLOW, 3))).AddCell(new Cell(2, 1).Add
                (textByron).SetBorder(new SolidBorder(Color.RED, 5))).AddCell(new Cell(2, 1).Add(textByron).SetBorder(
                new SolidBorder(Color.GRAY, 7))).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.BLUE
                , 12))).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.CYAN, 1)));
            table.SetBorder(new SolidBorder(Color.GREEN, 2));
            table.SetMinHeight(300);
            doc.Add(table);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Some cells' heights are set:"));
            table = new Table(3).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.GREEN, 1))).AddCell
                (new Cell(1, 2).Add(textByron).SetBorder(new SolidBorder(Color.YELLOW, 3)).SetMinHeight(300)).AddCell(
                new Cell().Add(textByron).SetBorder(new SolidBorder(Color.GREEN, 1))).AddCell(new Cell(1, 2).Add(textByron
                ).SetBorder(new SolidBorder(Color.YELLOW, 3)).SetMaxHeight(40)).AddCell(new Cell(2, 1).Add(textByron).
                SetBorder(new SolidBorder(Color.RED, 5))).AddCell(new Cell(2, 1).Add(textByron).SetBorder(new SolidBorder
                (Color.GRAY, 7))).AddCell(new Cell().Add(textByron).SetBorder(new SolidBorder(Color.BLUE, 12))).AddCell
                (new Cell().Add(textByron).SetBorder(new SolidBorder(Color.CYAN, 1)).SetMaxHeight(20));
            table.SetBorder(new SolidBorder(Color.GREEN, 2));
            table.SetHeight(1700);
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TableWithSetHeightProperties03() {
            String testName = "tableWithSetHeightProperties03.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            String textByron = "When a man hath no freedom to fight for at home,\n" + "    Let him combat for that of his neighbours;\n"
                 + "Let him think of the glories of Greece and of Rome,\n" + "    And get knocked on the head for his labours.\n";
            String textFrance = "Liberte Egalite Fraternite";
            doc.Add(new Paragraph("Default layout:"));
            Table table = new Table(1).AddCell(new Cell().Add(textFrance).SetBackgroundColor(Color.RED)).AddCell(new Cell
                ().Add(textFrance).SetBackgroundColor(Color.GREEN)).AddCell(new Cell().Add(textFrance).SetBackgroundColor
                (Color.BLUE));
            doc.Add(table);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Table's height is bigger than needed:"));
            table = new Table(1).AddCell(new Cell().Add(textFrance).SetBackgroundColor(Color.RED)).AddCell(new Cell().
                Add(textFrance).SetBackgroundColor(Color.GREEN)).AddCell(new Cell().Add(textFrance).SetBackgroundColor
                (Color.BLUE));
            table.SetHeight(600);
            doc.Add(table);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Table's height is bigger than needed and some cells have HEIGHT property:"));
            table = new Table(1).AddCell(new Cell().Add(textFrance).SetBackgroundColor(Color.RED)).AddCell(new Cell().
                Add(textFrance).SetBackgroundColor(Color.GREEN).SetHeight(30)).AddCell(new Cell().Add(textFrance).SetBackgroundColor
                (Color.BLUE));
            table.SetHeight(600);
            doc.Add(table);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Table's height is bigger than needed and all cells have HEIGHT property:"));
            table = new Table(1).AddCell(new Cell().Add(textFrance).SetBackgroundColor(Color.RED).SetHeight(25)).AddCell
                (new Cell().Add(textFrance).SetBackgroundColor(Color.GREEN).SetHeight(75)).AddCell(new Cell().Add(textFrance
                ).SetBackgroundColor(Color.BLUE).SetHeight(50));
            table.SetHeight(600);
            doc.Add(table);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Table's height is bigger than needed and some cells have HEIGHT property:"));
            table = new Table(2).AddCell(new Cell().Add(textFrance).SetBackgroundColor(Color.RED).SetHeight(25)).AddCell
                (new Cell().Add(textFrance).SetBackgroundColor(Color.BLUE)).AddCell(new Cell().Add(textFrance).SetBackgroundColor
                (Color.GREEN)).AddCell(new Cell().Add(textFrance).SetBackgroundColor(Color.RED)).AddCell(new Cell().Add
                (textFrance).SetBackgroundColor(Color.BLUE).SetHeight(50)).AddCell(new Cell().Add(textFrance).SetBackgroundColor
                (Color.GREEN));
            table.SetHeight(600);
            doc.Add(table);
            doc.Add(new AreaBreak());
            doc.Add(new Paragraph("Table's height is bigger than needed, some cells have big rowspan and HEIGHT property:"
                ));
            table = new Table(2).AddCell(new Cell().Add(textFrance).SetBackgroundColor(Color.RED)).AddCell(new Cell().
                Add(textFrance).SetBackgroundColor(Color.BLUE)).AddCell(new Cell(2, 1).Add(textFrance).SetBackgroundColor
                (Color.GREEN)).AddCell(new Cell().Add(textFrance).SetBackgroundColor(Color.RED)).AddCell(new Cell().Add
                (textFrance).SetBackgroundColor(Color.GREEN).SetHeight(50));
            table.SetHeight(600);
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TableWithHeaderInTheBottomOfPageTest() {
            String testName = "tableWithHeaderInTheBottomOfPageTest.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            for (int i = 0; i < 28; i++) {
                doc.Add(new Paragraph("Text"));
            }
            Table table = new Table(UnitValue.CreatePercentArray(new float[] { 10, 10 }));
            table.AddHeaderCell(new Cell().Add("Header One"));
            table.AddHeaderCell(new Cell().Add("Header Two"));
            table.AddCell(new Cell().Add("Hello"));
            table.AddCell(new Cell().Add("World"));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA)]
        public virtual void BigFooterTest01() {
            String testName = "bigFooterTest01.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            Table table = new Table(1);
            table.AddFooterCell(new Cell().Add("Footer").SetHeight(650).SetBorderTop(new SolidBorder(Color.GREEN, 100)
                ));
            table.AddCell(new Cell().Add("Body").SetHeight(30));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA)]
        public virtual void BigFooterTest02() {
            String testName = "bigFooterTest02.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            Table table = new Table(1);
            table.AddFooterCell(new Cell().Add("Footer").SetHeight(380).SetBackgroundColor(Color.YELLOW));
            table.AddHeaderCell(new Cell().Add("Header").SetHeight(380).SetBackgroundColor(Color.BLUE));
            table.AddCell(new Cell().Add("Body"));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TableWithDocumentRelayoutTest() {
            String testName = "tableWithDocumentRelayoutTest.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc, PageSize.A4, false);
            Table table = new Table(UnitValue.CreatePercentArray(new float[] { 10 }));
            for (int i = 0; i < 40; i++) {
                table.AddCell(new Cell().Add("" + (i + 1)));
            }
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TableWithKeepTogetherOnCells() {
            String testName = "tableWithKeepTogetherOnCells.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            Document document = new Document(new PdfDocument(new PdfWriter(outFileName)));
            Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1.3f, 1f, 1f, 1f, 1f, 1f, 1f }));
            table.SetWidthPercent(100f).SetFixedLayout();
            for (int i = 1; i <= 7 * 100; i++) {
                Cell cell = new Cell().SetKeepTogether(true).SetMinHeight(45).Add("" + i);
                table.AddCell(cell);
            }
            document.Add(table);
            document.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void EmptyTableTest01() {
            String testName = "emptyTableTest01.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            doc.Add(new Table(1).SetBorderTop(new SolidBorder(Color.ORANGE, 50)).SetBorderBottom(new SolidBorder(Color
                .MAGENTA, 100)));
            doc.Add(new Table(1).SetBorder(new SolidBorder(Color.ORANGE, 2)).AddCell("Is my occupied area correct?"));
            doc.Add(new AreaBreak());
            doc.Add(new Table(1).AddCell(new Cell().SetPadding(0).SetMargin(0).SetBorder(Border.NO_BORDER)).AddCell(new 
                Cell().SetPadding(0).SetMargin(0).SetBorder(Border.NO_BORDER)).AddCell(new Cell().SetPadding(0).SetMargin
                (0).SetBorder(Border.NO_BORDER)).AddCell(new Cell().SetPadding(0).SetMargin(0).SetBorder(Border.NO_BORDER
                )).AddCell(new Cell().Add("Hello")));
            doc.Add(new Table(1).SetBorder(new SolidBorder(Color.ORANGE, 2)).AddCell("Is my occupied area correct?"));
            doc.Add(new AreaBreak());
            doc.Add(new Table(1).SetMinHeight(300).SetBorderRight(new SolidBorder(Color.ORANGE, 5)).SetBorderTop(new SolidBorder
                (100)).SetBorderBottom(new SolidBorder(Color.BLUE, 50)));
            doc.Add(new Table(1).SetBorder(new SolidBorder(Color.ORANGE, 2)).AddCell("Is my occupied area correct?"));
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.LAST_ROW_IS_NOT_COMPLETE, Count = 2)]
        public virtual void TableWithIncompleteFooter() {
            String testName = "tableWithIncompleteFooter.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            Table table = new Table(3);
            table.AddCell("Liberte");
            table.AddCell("Egalite");
            table.AddCell("Fraternite");
            table.AddFooterCell(new Cell(1, 2).Add("Liberte Egalite"));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.LAST_ROW_IS_NOT_COMPLETE, Count = 1)]
        public virtual void TableWithCustomRendererTest01() {
            String testName = "tableWithCustomRendererTest01.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            Table table = new Table(2);
            table.SetBorder(new SolidBorder(Color.GREEN, 100));
            for (int i = 0; i < 10; i++) {
                table.AddCell(new Cell().Add("Cell No." + i));
            }
            table.SetNextRenderer(new TableTest.CustomRenderer(table, new Table.RowRange(0, 10)));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SkipLastRowTest() {
            // This test checks that the table occupies exactly one page and does not draw its footer.
            // A naive algorithm would have this table on two pages with only one row with data on the second page
            // However, as setSkipLastFooter is true, we can lay out that row with data on the first page and avoid unnecessary footer placement.
            String testName = "skipLastRowTest.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            Table table = new Table(2);
            table.AddHeaderCell("Header 1");
            table.AddHeaderCell("Header 2");
            table.AddFooterCell(new Cell(1, 2).Add("Footer"));
            table.SetSkipLastFooter(true);
            for (int i = 0; i < 33; i++) {
                table.AddCell("text 1");
                table.AddCell("text 2");
            }
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SkipFooterTest01() {
            String testName = "skipFooterTest01.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            Table table = new Table(1);
            for (int i = 0; i < 19; i++) {
                table.AddCell(new Cell().Add(i + " Liberté!\nÉgalité!\nFraternité!").SetHeight(100));
            }
            table.AddFooterCell(new Cell().Add("Footer").SetHeight(116).SetBackgroundColor(Color.RED));
            // the next line cause the reuse
            table.SetSkipLastFooter(true);
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TableSplitTest01() {
            String testName = "tableSplitTest01.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            String gretzky = "Make Gretzky great again!";
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc, PageSize.A8.Rotate());
            Table table = new Table(2);
            table.SetBorder(new SolidBorder(Color.GREEN, 15));
            table.AddCell(new Cell().Add(gretzky));
            table.AddCell(new Cell().Add(gretzky));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TableSplitTest02() {
            String testName = "tableSplitTest02.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            String gretzky = "Make Gretzky great again!";
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc, PageSize.A7.Rotate());
            Table table = new Table(2);
            table.SetBorder(new SolidBorder(Color.GREEN, 15));
            PdfImageXObject xObject = new PdfImageXObject(ImageDataFactory.CreatePng(UrlUtil.ToURL(sourceFolder + "itext.png"
                )));
            iText.Layout.Element.Image image = new iText.Layout.Element.Image(xObject, 50);
            table.AddCell(new Cell().Add(gretzky));
            table.AddCell(new Cell().Add(gretzky));
            table.AddCell(new Cell().Add(image));
            table.AddCell(new Cell().Add(gretzky));
            table.AddCell(new Cell().Add(gretzky));
            table.AddCell(new Cell().Add(gretzky));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TableSplitTest03() {
            String testName = "tableSplitTest03.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            String gretzky = "Make Gretzky great again!";
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc, PageSize.A8.Rotate());
            Table table = new Table(2);
            table.SetBorder(new SolidBorder(Color.GREEN, 15));
            table.AddCell(new Cell().Add(gretzky));
            table.AddCell(new Cell(2, 1).Add(gretzky));
            table.AddCell(new Cell().Add(gretzky));
            table.AddCell(new Cell().Add(gretzky));
            table.AddCell(new Cell().Add(gretzky));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TableSplitTest04() {
            String testName = "tableSplitTest04.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            String gretzky = "Make Gretzky great again!";
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc, PageSize.A7.Rotate());
            Table table = new Table(2);
            table.SetBorder(new SolidBorder(Color.GREEN, 15));
            PdfImageXObject xObject = new PdfImageXObject(ImageDataFactory.CreatePng(UrlUtil.ToURL(sourceFolder + "itext.png"
                )));
            iText.Layout.Element.Image image = new iText.Layout.Element.Image(xObject, 50);
            table.AddCell(new Cell().Add(gretzky));
            table.AddCell(new Cell(2, 1).Add(gretzky));
            table.AddCell(new Cell().Add(image));
            table.AddCell(new Cell().Add(gretzky));
            table.AddCell(new Cell().Add(gretzky));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.LAST_ROW_IS_NOT_COMPLETE)]
        [LogMessage(iText.IO.LogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA)]
        public virtual void TableNothingResultTest() {
            String testName = "tableNothingResultTest.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDoc);
            Table table = new Table(UnitValue.CreatePercentArray(new float[] { 30, 30 }));
            table.SetKeepTogether(true);
            for (int i = 0; i < 40; i++) {
                table.AddCell(new Cell().Add("Hello"));
                table.AddCell(new Cell().Add("World"));
                table.StartNewRow();
            }
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void AutoLayoutTest01() {
            String testName = "autoLayoutTest01.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(outFileName));
            // Initialize document
            Document doc = new Document(pdf);
            doc.Add(new Paragraph("Simple cell:"));
            Table table = new Table(new float[1]);
            table.AddCell("A cell");
            doc.Add(table);
            doc.Add(new Paragraph("A cell with bold text:"));
            table = new Table(new float[1]);
            table.AddCell("A cell").SetBold();
            doc.Add(table);
            doc.Add(new Paragraph("A cell with italic text:"));
            table = new Table(new float[1]);
            table.AddCell("A cell").SetItalic();
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void AutoLayoutTest02() {
            String testName = "autoLayoutTest02.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            PdfDocument pdf = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdf);
            doc.Add(new Paragraph("Simple cell:"));
            Table table = new Table(UnitValue.CreatePercentArray(new float[] { 5, 95 }));
            table.AddCell(new Cell().Add(new Paragraph("Hellowor ld!")));
            table.AddCell(new Cell().Add(new Paragraph("Long long long Long long long Long long long Long long long text"
                )));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void FixedLayoutTest01() {
            String testName = "fixedLayoutTest01.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(outFileName));
            // Initialize document
            Document doc = new Document(pdf);
            doc.Add(new Paragraph("Simple table with proportional width. Ignore cell width, because sum(col[*]) < tableWidth:"
                ));
            Table table = new Table(new float[] { 1, 2, 3 }).SetFixedLayout().SetWidth(400);
            table.AddCell("1x");
            table.AddCell("2x");
            table.AddCell("3x");
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void FixedLayoutTest02() {
            String testName = "fixedLayoutTest02.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(outFileName));
            // Initialize document
            Document doc = new Document(pdf);
            doc.Add(new Paragraph("Simple table with proportional width. Ignore table width, because sum(col[*]) > tableWidth."
                ));
            Table table = new Table(new float[] { 20, 40, 60 }).SetFixedLayout().SetWidth(10);
            table.AddCell("1x");
            table.AddCell("2x");
            table.AddCell("3x");
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.CLIP_ELEMENT, Count = 2)]
        public virtual void FixedPositionTest01() {
            String testName = "fixedPositionTest01.pdf";
            String outFileName = destinationFolder + testName;
            String cmpFileName = sourceFolder + "cmp_" + testName;
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(outFileName));
            // Initialize document
            Document doc = new Document(pdf);
            Table table = new Table(1);
            for (int i = 0; i < 100; i++) {
                table.AddCell(new Cell().Add("Hello " + i).SetBackgroundColor(Color.RED));
            }
            table.SetFixedPosition(150, 300, 200);
            table.SetHeight(300);
            table.SetBackgroundColor(Color.YELLOW);
            doc.Add(new Paragraph("The next table has fixed position and height property. However set height is shorter than needed and we can place table only partially."
                ));
            doc.Add(table);
            doc.Add(new AreaBreak());
            table.SetHeight(10);
            doc.Add(new Paragraph("The next table has fixed position and height property. However set height is shorter than needed and we cannot fully place even a cell."
                ));
            doc.Add(table);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , testName + "_diff"));
        }

        internal class CustomRenderer : TableRenderer {
            public CustomRenderer(Table modelElement, Table.RowRange rowRange)
                : base(modelElement, rowRange) {
            }
        }
    }
}
