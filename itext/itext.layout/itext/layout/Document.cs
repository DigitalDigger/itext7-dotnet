/*

This file is part of the iText (R) project.
Copyright (c) 1998-2017 iText Group NV
Authors: Bruno Lowagie, Paulo Soares, et al.

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
using iText.Kernel;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Renderer;

namespace iText.Layout {
    /// <summary>Document is the default root element when creating a self-sufficient PDF.</summary>
    /// <remarks>
    /// Document is the default root element when creating a self-sufficient PDF. It
    /// mainly operates high-level operations e.g. setting page size and rotation,
    /// adding elements, and writing text at specific coordinates. It has no
    /// knowledge of the actual PDF concepts and syntax.
    /// <p>
    /// A
    /// <see cref="Document"/>
    /// 's rendering behavior can be modified by extending
    /// <see cref="iText.Layout.Renderer.DocumentRenderer"/>
    /// and setting an instance of this newly created with
    /// <see cref="SetRenderer(iText.Layout.Renderer.DocumentRenderer)"></see>
    /// .
    /// </remarks>
    public class Document : RootElement<iText.Layout.Document> {
        protected internal float leftMargin = 36;

        protected internal float rightMargin = 36;

        protected internal float topMargin = 36;

        protected internal float bottomMargin = 36;

        /// <summary>
        /// Creates a document from a
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
        /// . Initializes the first page
        /// with the
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
        /// 's current default
        /// <see cref="iText.Kernel.Geom.PageSize"/>
        /// .
        /// </summary>
        /// <param name="pdfDoc">the in-memory representation of the PDF document</param>
        public Document(PdfDocument pdfDoc)
            : this(pdfDoc, pdfDoc.GetDefaultPageSize()) {
        }

        /// <summary>
        /// Creates a document from a
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
        /// with a manually set
        /// <see cref="iText.Kernel.Geom.PageSize"/>
        /// .
        /// </summary>
        /// <param name="pdfDoc">the in-memory representation of the PDF document</param>
        /// <param name="pageSize">the page size</param>
        public Document(PdfDocument pdfDoc, PageSize pageSize)
            : this(pdfDoc, pageSize, true) {
        }

        /// <summary>
        /// Creates a document from a
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
        /// with a manually set
        /// <see cref="iText.Kernel.Geom.PageSize"/>
        /// .
        /// </summary>
        /// <param name="pdfDoc">the in-memory representation of the PDF document</param>
        /// <param name="pageSize">the page size</param>
        /// <param name="immediateFlush">
        /// if true, write pages and page-related instructions
        /// to the
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
        /// as soon as possible.
        /// </param>
        public Document(PdfDocument pdfDoc, PageSize pageSize, bool immediateFlush)
            : base() {
            this.pdfDocument = pdfDoc;
            this.pdfDocument.SetDefaultPageSize(pageSize);
            this.immediateFlush = immediateFlush;
        }

        /// <summary>Closes the document and associated PdfDocument.</summary>
        public override void Close() {
            if (rootRenderer != null) {
                rootRenderer.Close();
            }
            pdfDocument.Close();
        }

        /// <summary>Terminates the current element, usually a page.</summary>
        /// <remarks>
        /// Terminates the current element, usually a page. Sets the next element
        /// to be the size specified in the argument.
        /// </remarks>
        /// <param name="areaBreak">
        /// an
        /// <see cref="iText.Layout.Element.AreaBreak"/>
        /// , optionally with a specified size
        /// </param>
        /// <returns>this element</returns>
        public virtual iText.Layout.Document Add(AreaBreak areaBreak) {
            CheckClosingStatus();
            childElements.Add(areaBreak);
            EnsureRootRendererNotNull().AddChild(areaBreak.CreateRendererSubTree());
            if (immediateFlush) {
                childElements.JRemoveAt(childElements.Count - 1);
            }
            return this;
        }

        public override iText.Layout.Document Add(IBlockElement element) {
            CheckClosingStatus();
            base.Add(element);
            if (element is ILargeElement) {
                ((ILargeElement)element).SetDocument(this);
                ((ILargeElement)element).FlushContent();
            }
            return this;
        }

        /// <summary>Gets PDF document.</summary>
        /// <returns>the in-memory representation of the PDF document</returns>
        public virtual PdfDocument GetPdfDocument() {
            return pdfDocument;
        }

        /// <summary>
        /// Changes the
        /// <see cref="iText.Layout.Renderer.DocumentRenderer"/>
        /// at runtime. Use this to customize
        /// the Document's
        /// <see cref="iText.Layout.Renderer.IRenderer"/>
        /// behavior.
        /// </summary>
        /// <param name="documentRenderer"/>
        public virtual void SetRenderer(DocumentRenderer documentRenderer) {
            this.rootRenderer = documentRenderer;
        }

        /// <summary>
        /// Forces all registered renderers (including child element renderers) to
        /// flush their contents to the content stream.
        /// </summary>
        public virtual void Flush() {
            rootRenderer.Flush();
        }

        /// <summary>
        /// Performs an entire recalculation of the document flow, taking into
        /// account all its current child elements.
        /// </summary>
        /// <remarks>
        /// Performs an entire recalculation of the document flow, taking into
        /// account all its current child elements. May become very
        /// resource-intensive for large documents.
        /// <p>
        /// Do not use when you have set
        /// <see cref="RootElement{T}.immediateFlush"/>
        /// to <code>true</code>.
        /// </remarks>
        public virtual void Relayout() {
            if (immediateFlush) {
                throw new InvalidOperationException("Operation not supported with immediate flush");
            }
            IRenderer nextRelayoutRenderer = rootRenderer != null ? rootRenderer.GetNextRenderer() : null;
            if (nextRelayoutRenderer == null || !(nextRelayoutRenderer is RootRenderer)) {
                nextRelayoutRenderer = new DocumentRenderer(this, immediateFlush);
            }
            while (pdfDocument.GetNumberOfPages() > 0) {
                pdfDocument.RemovePage(pdfDocument.GetNumberOfPages());
            }
            rootRenderer = (RootRenderer)nextRelayoutRenderer;
            foreach (IElement element in childElements) {
                rootRenderer.AddChild(element.CreateRendererSubTree());
            }
        }

        /// <summary>Gets the left margin, measured in points</summary>
        /// <returns>a <code>float</code> containing the left margin value</returns>
        public virtual float GetLeftMargin() {
            return leftMargin;
        }

        /// <summary>Sets the left margin, measured in points</summary>
        /// <param name="leftMargin">a <code>float</code> containing the new left margin value</param>
        public virtual void SetLeftMargin(float leftMargin) {
            this.leftMargin = leftMargin;
        }

        /// <summary>Gets the right margin, measured in points</summary>
        /// <returns>a <code>float</code> containing the right margin value</returns>
        public virtual float GetRightMargin() {
            return rightMargin;
        }

        /// <summary>Sets the right margin, measured in points</summary>
        /// <param name="rightMargin">a <code>float</code> containing the new right margin value</param>
        public virtual void SetRightMargin(float rightMargin) {
            this.rightMargin = rightMargin;
        }

        /// <summary>Gets the top margin, measured in points</summary>
        /// <returns>a <code>float</code> containing the top margin value</returns>
        public virtual float GetTopMargin() {
            return topMargin;
        }

        /// <summary>Sets the top margin, measured in points</summary>
        /// <param name="topMargin">a <code>float</code> containing the new top margin value</param>
        public virtual void SetTopMargin(float topMargin) {
            this.topMargin = topMargin;
        }

        /// <summary>Gets the bottom margin, measured in points</summary>
        /// <returns>a <code>float</code> containing the bottom margin value</returns>
        public virtual float GetBottomMargin() {
            return bottomMargin;
        }

        /// <summary>Sets the bottom margin, measured in points</summary>
        /// <param name="bottomMargin">a <code>float</code> containing the new bottom margin value</param>
        public virtual void SetBottomMargin(float bottomMargin) {
            this.bottomMargin = bottomMargin;
        }

        /// <summary>Convenience method to set all margins with one method.</summary>
        /// <param name="topMargin">the upper margin</param>
        /// <param name="rightMargin">the right margin</param>
        /// <param name="leftMargin">the left margin</param>
        /// <param name="bottomMargin">the lower margin</param>
        public virtual void SetMargins(float topMargin, float rightMargin, float bottomMargin, float leftMargin) {
            SetTopMargin(topMargin);
            SetRightMargin(rightMargin);
            SetBottomMargin(bottomMargin);
            SetLeftMargin(leftMargin);
        }

        /// <summary>
        /// Returns the area that will actually be used to write on the page, given
        /// the current margins.
        /// </summary>
        /// <remarks>
        /// Returns the area that will actually be used to write on the page, given
        /// the current margins. Does not have any side effects on the document.
        /// </remarks>
        /// <param name="pageSize">the size of the page to</param>
        /// <returns>
        /// a
        /// <see cref="iText.Kernel.Geom.Rectangle"/>
        /// with the required dimensions and origin point
        /// </returns>
        public virtual Rectangle GetPageEffectiveArea(PageSize pageSize) {
            return new Rectangle(pageSize.GetLeft() + leftMargin, pageSize.GetBottom() + bottomMargin, pageSize.GetWidth
                () - leftMargin - rightMargin, pageSize.GetHeight() - bottomMargin - topMargin);
        }

        protected internal override RootRenderer EnsureRootRendererNotNull() {
            if (rootRenderer == null) {
                rootRenderer = new DocumentRenderer(this, immediateFlush);
            }
            return rootRenderer;
        }

        /// <summary>Checks whether a method is invoked at the closed document</summary>
        protected internal virtual void CheckClosingStatus() {
            if (GetPdfDocument().IsClosed()) {
                throw new PdfException(PdfException.DocumentClosedItIsImpossibleToExecuteAction);
            }
        }
    }
}
