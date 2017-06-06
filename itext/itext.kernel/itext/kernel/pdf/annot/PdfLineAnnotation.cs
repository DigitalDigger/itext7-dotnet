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
using iText.Kernel.Geom;
using iText.Kernel.Pdf;

namespace iText.Kernel.Pdf.Annot {
    /// <summary>The purpose of a line annotation is to display a single straight line on the page.</summary>
    /// <remarks>
    /// The purpose of a line annotation is to display a single straight line on the page.
    /// When opened, it displays a pop-up window containing the text of the associated note.
    /// See also ISO-320001 12.5.6.7 "Line Annotations".
    /// </remarks>
    public class PdfLineAnnotation : PdfMarkupAnnotation {
        /// <summary>
        /// Creates a
        /// <see cref="PdfLineAnnotation"/>
        /// instance.
        /// </summary>
        /// <param name="rect">
        /// the annotation rectangle, defining the location of the annotation on the page
        /// in default user space units. See
        /// <see cref="PdfAnnotation.SetRectangle(iText.Kernel.Pdf.PdfArray)"/>
        /// .
        /// </param>
        /// <param name="line">
        /// an array of four numbers, [x1 y1 x2 y2], specifying the starting and ending coordinates
        /// of the line in default user space. See also
        /// <see cref="GetLine()"/>
        /// .
        /// </param>
        public PdfLineAnnotation(Rectangle rect, float[] line)
            : base(rect) {
            Put(PdfName.L, new PdfArray(line));
        }

        /// <summary>
        /// Creates a
        /// <see cref="PdfLineAnnotation"/>
        /// instance from the given
        /// <see cref="iText.Kernel.Pdf.PdfDictionary"/>
        /// that represents annotation object. This method is useful for property reading in reading mode or
        /// modifying in stamping mode.
        /// </summary>
        /// <param name="pdfDictionary">
        /// a
        /// <see cref="iText.Kernel.Pdf.PdfDictionary"/>
        /// that represents existing annotation in the document.
        /// </param>
        public PdfLineAnnotation(PdfDictionary pdfDictionary)
            : base(pdfDictionary) {
        }

        /// <summary><inheritDoc/></summary>
        public override PdfName GetSubtype() {
            return PdfName.Line;
        }

        /// <summary>
        /// An array of four numbers, [x1 y1 x2 y2], specifying the starting and ending coordinates of the line
        /// in default user space.
        /// </summary>
        /// <remarks>
        /// An array of four numbers, [x1 y1 x2 y2], specifying the starting and ending coordinates of the line
        /// in default user space. If the
        /// <see cref="iText.Kernel.Pdf.PdfName.LL"/>
        /// (see
        /// <see cref="GetLeaderLine()"/>
        /// ) entry is present, this value represents
        /// the endpoints of the leader lines rather than the endpoints of the line itself.
        /// </remarks>
        /// <returns>An array of four numbers specifying the starting and ending coordinates of the line in default user space.
        ///     </returns>
        public virtual PdfArray GetLine() {
            return GetPdfObject().GetAsArray(PdfName.L);
        }

        /// <summary>An array of two names specifying the line ending styles that is used in drawing the line.</summary>
        /// <remarks>
        /// An array of two names specifying the line ending styles that is used in drawing the line.
        /// The first and second elements of the array shall specify the line ending styles for the endpoints defined,
        /// respectively, by the first and second pairs of coordinates, (x1, y1) and (x2, y2), in the
        /// <see cref="iText.Kernel.Pdf.PdfName.L"/>
        /// array
        /// (see
        /// <see cref="GetLine()"/>
        /// . For possible values see
        /// <see cref="SetLineEndingStyles(iText.Kernel.Pdf.PdfArray)"/>
        /// .
        /// </remarks>
        /// <returns>
        /// An array of two names specifying the line ending styles that is used in drawing the line; or null if line
        /// endings style is not explicitly defined, default value is [/None /None].
        /// </returns>
        public virtual PdfArray GetLineEndingStyles() {
            return GetPdfObject().GetAsArray(PdfName.LE);
        }

        /// <summary>Sets the line ending styles that are used in drawing the line.</summary>
        /// <remarks>
        /// Sets the line ending styles that are used in drawing the line.
        /// The first and second elements of the array shall specify the line ending styles for the endpoints defined,
        /// respectively, by the first and second pairs of coordinates, (x1, y1) and (x2, y2), in the
        /// <see cref="iText.Kernel.Pdf.PdfName.L"/>
        /// array
        /// (see
        /// <see cref="GetLine()"/>
        /// . Possible values for styles are:
        /// <ul>
        /// <li>
        /// <see cref="iText.Kernel.Pdf.PdfName.Square"/>
        /// - A square filled with the annotation's interior color, if any; </li>
        /// <li>
        /// <see cref="iText.Kernel.Pdf.PdfName.Circle"/>
        /// - A circle filled with the annotation's interior color, if any; </li>
        /// <li>
        /// <see cref="iText.Kernel.Pdf.PdfName.Diamond"/>
        /// - A diamond shape filled with the annotation's interior color, if any; </li>
        /// <li>
        /// <see cref="iText.Kernel.Pdf.PdfName.OpenArrow"/>
        /// - Two short lines meeting in an acute angle to form an open arrowhead; </li>
        /// <li>
        /// <see cref="iText.Kernel.Pdf.PdfName.ClosedArrow"/>
        /// - Two short lines meeting in an acute angle as in the
        /// <see cref="iText.Kernel.Pdf.PdfName.OpenArrow"/>
        /// style and
        /// connected by a third line to form a triangular closed arrowhead filled with the annotation's interior color, if any; </li>
        /// <li>
        /// <see cref="iText.Kernel.Pdf.PdfName.None"/>
        /// - No line ending; </li>
        /// <li>
        /// <see cref="iText.Kernel.Pdf.PdfName.Butt"/>
        /// - A short line at the endpoint perpendicular to the line itself; </li>
        /// <li>
        /// <see cref="iText.Kernel.Pdf.PdfName.ROpenArrow"/>
        /// - Two short lines in the reverse direction from
        /// <see cref="iText.Kernel.Pdf.PdfName.OpenArrow"/>
        /// ; </li>
        /// <li>
        /// <see cref="iText.Kernel.Pdf.PdfName.RClosedArrow"/>
        /// - A triangular closed arrowhead in the reverse direction from
        /// <see cref="iText.Kernel.Pdf.PdfName.ClosedArrow"/>
        /// ; </li>
        /// <li>
        /// <see cref="iText.Kernel.Pdf.PdfName.Slash"/>
        /// - A short line at the endpoint approximately 30 degrees clockwise from perpendicular to the line itself; </li>
        /// </ul>
        /// see also ISO-320001, Table 176 "Line ending styles".
        /// </remarks>
        /// <param name="lineEndingStyles">An array of two names specifying the line ending styles that is used in drawing the line.
        ///     </param>
        /// <returns>
        /// this
        /// <see cref="PdfLineAnnotation"/>
        /// instance.
        /// </returns>
        public virtual iText.Kernel.Pdf.Annot.PdfLineAnnotation SetLineEndingStyles(PdfArray lineEndingStyles) {
            return (iText.Kernel.Pdf.Annot.PdfLineAnnotation)Put(PdfName.LE, lineEndingStyles);
        }

        /// <summary>
        /// The length of leader lines in default user space that extend from each endpoint of the line perpendicular
        /// to the line itself.
        /// </summary>
        /// <remarks>
        /// The length of leader lines in default user space that extend from each endpoint of the line perpendicular
        /// to the line itself. A positive value means that the leader lines appear in the direction that is clockwise
        /// when traversing the line from its starting point to its ending point (as specified by
        /// <see cref="iText.Kernel.Pdf.PdfName.L"/>
        /// (see
        /// <see cref="GetLine()"/>
        /// );
        /// a negative value indicates the opposite direction.
        /// </remarks>
        /// <returns>a float specifying the length of leader lines in default user space.</returns>
        [System.ObsoleteAttribute(@"use GetLeaderLineLength() instead.")]
        public virtual float GetLeaderLine() {
            PdfNumber n = GetPdfObject().GetAsNumber(PdfName.LL);
            return n == null ? 0 : n.FloatValue();
        }

        /// <summary>
        /// Sets the length of leader lines in default user space that extend from each endpoint of the line perpendicular
        /// to the line itself.
        /// </summary>
        /// <remarks>
        /// Sets the length of leader lines in default user space that extend from each endpoint of the line perpendicular
        /// to the line itself. A positive value means that the leader lines appear in the direction that is clockwise
        /// when traversing the line from its starting point to its ending point (as specified by
        /// <see cref="iText.Kernel.Pdf.PdfName.L"/>
        /// (see
        /// <see cref="GetLine()"/>
        /// );
        /// a negative value indicates the opposite direction.
        /// </remarks>
        /// <param name="leaderLine">a float specifying the length of leader lines in default user space.</param>
        /// <returns>
        /// this
        /// <see cref="PdfLineAnnotation"/>
        /// instance.
        /// </returns>
        [System.ObsoleteAttribute(@"use SetLeaderLineLength(float) instead.")]
        public virtual iText.Kernel.Pdf.Annot.PdfLineAnnotation SetLeaderLine(float leaderLine) {
            return (iText.Kernel.Pdf.Annot.PdfLineAnnotation)Put(PdfName.LL, new PdfNumber(leaderLine));
        }

        /// <summary>
        /// The length of leader lines in default user space that extend from each endpoint of the line perpendicular
        /// to the line itself.
        /// </summary>
        /// <remarks>
        /// The length of leader lines in default user space that extend from each endpoint of the line perpendicular
        /// to the line itself. A positive value means that the leader lines appear in the direction that is clockwise
        /// when traversing the line from its starting point to its ending point (as specified by
        /// <see cref="iText.Kernel.Pdf.PdfName.L"/>
        /// (see
        /// <see cref="GetLine()"/>
        /// );
        /// a negative value indicates the opposite direction.
        /// </remarks>
        /// <returns>a float specifying the length of leader lines in default user space.</returns>
        public virtual float GetLeaderLineLength() {
            PdfNumber n = GetPdfObject().GetAsNumber(PdfName.LL);
            return n == null ? 0 : n.FloatValue();
        }

        /// <summary>
        /// Sets the length of leader lines in default user space that extend from each endpoint of the line perpendicular
        /// to the line itself.
        /// </summary>
        /// <remarks>
        /// Sets the length of leader lines in default user space that extend from each endpoint of the line perpendicular
        /// to the line itself. A positive value means that the leader lines appear in the direction that is clockwise
        /// when traversing the line from its starting point to its ending point (as specified by
        /// <see cref="iText.Kernel.Pdf.PdfName.L"/>
        /// (see
        /// <see cref="GetLine()"/>
        /// );
        /// a negative value indicates the opposite direction.
        /// </remarks>
        /// <param name="leaderLineLength">a float specifying the length of leader lines in default user space.</param>
        /// <returns>
        /// this
        /// <see cref="PdfLineAnnotation"/>
        /// instance.
        /// </returns>
        public virtual iText.Kernel.Pdf.Annot.PdfLineAnnotation SetLeaderLineLength(float leaderLineLength) {
            return (iText.Kernel.Pdf.Annot.PdfLineAnnotation)Put(PdfName.LL, new PdfNumber(leaderLineLength));
        }

        /// <summary>
        /// A non-negative number that represents the length of leader line extensions that extend from the line proper
        /// 180 degrees from the leader lines.
        /// </summary>
        /// <returns>
        /// a non-negative float that represents the length of leader line extensions; or if the leader line extension
        /// is not explicitly set, returns the default value, which is 0.
        /// </returns>
        public virtual float GetLeaderLineExtension() {
            PdfNumber n = GetPdfObject().GetAsNumber(PdfName.LLE);
            return n == null ? 0 : n.FloatValue();
        }

        /// <summary>Sets the length of leader line extensions that extend from the line proper 180 degrees from the leader lines.
        ///     </summary>
        /// <remarks>
        /// Sets the length of leader line extensions that extend from the line proper 180 degrees from the leader lines.
        /// <b>This value shall not be set unless
        /// <see cref="iText.Kernel.Pdf.PdfName.LL"/>
        /// (see
        /// <see cref="SetLeaderLine(float)"/>
        /// ) is set.</b>
        /// </remarks>
        /// <param name="leaderLineExtension">a non-negative float that represents the length of leader line extensions.
        ///     </param>
        /// <returns>
        /// this
        /// <see cref="PdfLineAnnotation"/>
        /// instance.
        /// </returns>
        public virtual iText.Kernel.Pdf.Annot.PdfLineAnnotation SetLeaderLineExtension(float leaderLineExtension) {
            return (iText.Kernel.Pdf.Annot.PdfLineAnnotation)Put(PdfName.LLE, new PdfNumber(leaderLineExtension));
        }

        /// <summary>
        /// A non-negative number that represents the length of the leader line offset, which is the amount of empty space
        /// between the endpoints of the annotation and the beginning of the leader lines.
        /// </summary>
        /// <returns>
        /// a non-negative number that represents the length of the leader line offset,
        /// or null if leader line offset is not set.
        /// </returns>
        public virtual float GetLeaderLineOffset() {
            PdfNumber n = GetPdfObject().GetAsNumber(PdfName.LLO);
            return n == null ? 0 : n.FloatValue();
        }

        /// <summary>
        /// Sets the length of the leader line offset, which is the amount of empty space between the endpoints of the
        /// annotation and the beginning of the leader lines.
        /// </summary>
        /// <param name="leaderLineOffset">a non-negative number that represents the length of the leader line offset.
        ///     </param>
        /// <returns>
        /// this
        /// <see cref="PdfLineAnnotation"/>
        /// instance.
        /// </returns>
        public virtual iText.Kernel.Pdf.Annot.PdfLineAnnotation SetLeaderLineOffset(float leaderLineOffset) {
            return (iText.Kernel.Pdf.Annot.PdfLineAnnotation)Put(PdfName.LLO, new PdfNumber(leaderLineOffset));
        }

        /// <summary>
        /// If true, the text specified by the
        /// <see cref="iText.Kernel.Pdf.PdfName.Contents"/>
        /// or
        /// <see cref="iText.Kernel.Pdf.PdfName.RC"/>
        /// entries
        /// (see
        /// <see cref="PdfAnnotation.GetContents()"/>
        /// and
        /// <see cref="PdfMarkupAnnotation.GetRichText()"/>
        /// )
        /// is replicated as a caption in the appearance of the line.
        /// </summary>
        /// <returns>
        /// true, if the annotation text is replicated as a caption, false otherwise. If this property is
        /// not set, default value is used which is <i>false</i>.
        /// </returns>
        public virtual bool GetContentsAsCaption() {
            PdfBoolean b = GetPdfObject().GetAsBoolean(PdfName.Cap);
            return b != null && b.GetValue();
        }

        /// <summary>
        /// If set to true, the text specified by the
        /// <see cref="iText.Kernel.Pdf.PdfName.Contents"/>
        /// or
        /// <see cref="iText.Kernel.Pdf.PdfName.RC"/>
        /// entries
        /// (see
        /// <see cref="PdfAnnotation.GetContents()"/>
        /// and
        /// <see cref="PdfMarkupAnnotation.GetRichText()"/>
        /// )
        /// will be replicated as a caption in the appearance of the line.
        /// </summary>
        /// <param name="contentsAsCaption">true, if the annotation text should be replicated as a caption, false otherwise.
        ///     </param>
        /// <returns>
        /// this
        /// <see cref="PdfLineAnnotation"/>
        /// instance.
        /// </returns>
        public virtual iText.Kernel.Pdf.Annot.PdfLineAnnotation SetContentsAsCaption(bool contentsAsCaption) {
            return (iText.Kernel.Pdf.Annot.PdfLineAnnotation)Put(PdfName.Cap, PdfBoolean.ValueOf(contentsAsCaption));
        }

        /// <summary>A name describing the annotation's caption positioning.</summary>
        /// <remarks>
        /// A name describing the annotation's caption positioning. Valid values are
        /// <see cref="iText.Kernel.Pdf.PdfName.Inline"/>
        /// , meaning the caption
        /// is centered inside the line, and
        /// <see cref="iText.Kernel.Pdf.PdfName.Top"/>
        /// , meaning the caption is on top of the line.
        /// </remarks>
        /// <returns>
        /// a name describing the annotation's caption positioning, or null if the caption positioning is not
        /// explicitly defined (in this case the default value is used, which is
        /// <see cref="iText.Kernel.Pdf.PdfName.Inline"/>
        /// ).
        /// </returns>
        public virtual PdfName GetCaptionPosition() {
            return GetPdfObject().GetAsName(PdfName.CP);
        }

        /// <summary>Sets annotation's caption positioning.</summary>
        /// <remarks>
        /// Sets annotation's caption positioning. Valid values are
        /// <see cref="iText.Kernel.Pdf.PdfName.Inline"/>
        /// , meaning the caption
        /// is centered inside the line, and
        /// <see cref="iText.Kernel.Pdf.PdfName.Top"/>
        /// , meaning the caption is on top of the line.
        /// </remarks>
        /// <param name="captionPosition">a name describing the annotation's caption positioning.</param>
        /// <returns>
        /// this
        /// <see cref="PdfLineAnnotation"/>
        /// instance.
        /// </returns>
        public virtual iText.Kernel.Pdf.Annot.PdfLineAnnotation SetCaptionPosition(PdfName captionPosition) {
            return (iText.Kernel.Pdf.Annot.PdfLineAnnotation)Put(PdfName.CP, captionPosition);
        }

        /// <summary>A measure dictionary (see ISO-320001, Table 261) that specifies the scale and units that apply to the line annotation.
        ///     </summary>
        /// <returns>
        /// a
        /// <see cref="iText.Kernel.Pdf.PdfDictionary"/>
        /// that represents a measure dictionary.
        /// </returns>
        public virtual PdfDictionary GetMeasure() {
            return GetPdfObject().GetAsDictionary(PdfName.Measure);
        }

        /// <summary>Sets a measure dictionary that specifies the scale and units that apply to the line annotation.</summary>
        /// <param name="measure">
        /// a
        /// <see cref="iText.Kernel.Pdf.PdfDictionary"/>
        /// that represents a measure dictionary, see ISO-320001, Table 261 for valid
        /// contents specification.
        /// </param>
        /// <returns>
        /// this
        /// <see cref="PdfLineAnnotation"/>
        /// instance.
        /// </returns>
        public virtual iText.Kernel.Pdf.Annot.PdfLineAnnotation SetMeasure(PdfDictionary measure) {
            return (iText.Kernel.Pdf.Annot.PdfLineAnnotation)Put(PdfName.Measure, measure);
        }

        /// <summary>An array of two numbers that specifies the offset of the caption text from its normal position.</summary>
        /// <remarks>
        /// An array of two numbers that specifies the offset of the caption text from its normal position.
        /// The first value is the horizontal offset along the annotation line from its midpoint, with a positive value
        /// indicating offset to the right and a negative value indicating offset to the left. The second value is the vertical
        /// offset perpendicular to the annotation line, with a positive value indicating a shift up and a negative value indicating
        /// a shift down.
        /// </remarks>
        /// <returns>
        /// a
        /// <see cref="iText.Kernel.Pdf.PdfArray"/>
        /// of two numbers that specifies the offset of the caption text from its normal position,
        /// or null if caption offset is not explicitly specified (in this case a default value is used, which is [0, 0]).
        /// </returns>
        public virtual PdfArray GetCaptionOffset() {
            return GetPdfObject().GetAsArray(PdfName.CO);
        }

        /// <summary>Sets the offset of the caption text from its normal position.</summary>
        /// <param name="captionOffset">
        /// a
        /// <see cref="iText.Kernel.Pdf.PdfArray"/>
        /// of two numbers that specifies the offset of the caption text from its
        /// normal position. The first value defines the horizontal offset along the annotation line from
        /// its midpoint, with a positive value indicating offset to the right and a negative value indicating
        /// offset to the left. The second value defines the vertical offset perpendicular to the annotation line,
        /// with a positive value indicating a shift up and a negative value indicating a shift down.
        /// </param>
        /// <returns>
        /// this
        /// <see cref="PdfLineAnnotation"/>
        /// instance.
        /// </returns>
        public virtual iText.Kernel.Pdf.Annot.PdfLineAnnotation SetCaptionOffset(PdfArray captionOffset) {
            return (iText.Kernel.Pdf.Annot.PdfLineAnnotation)Put(PdfName.CO, captionOffset);
        }

        /// <summary>Sets the offset of the caption text from its normal position.</summary>
        /// <param name="captionOffset">
        /// an array of two floats that specifies the offset of the caption text from its
        /// normal position. The first value defines the horizontal offset along the annotation line from
        /// its midpoint, with a positive value indicating offset to the right and a negative value indicating
        /// offset to the left. The second value defines the vertical offset perpendicular to the annotation line,
        /// with a positive value indicating a shift up and a negative value indicating a shift down.
        /// </param>
        /// <returns>
        /// this
        /// <see cref="PdfLineAnnotation"/>
        /// instance.
        /// </returns>
        public virtual iText.Kernel.Pdf.Annot.PdfLineAnnotation SetCaptionOffset(float[] captionOffset) {
            return SetCaptionOffset(new PdfArray(captionOffset));
        }
    }
}
