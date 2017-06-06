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

namespace iText.IO.Font {
    /// <summary>Base font descriptor.</summary>
    public class FontProgramDescriptor {
        private readonly String fontName;

        private readonly String fullNameLowerCase;

        private readonly String fontNameLowerCase;

        private readonly String familyNameLowerCase;

        private readonly String style;

        private readonly int macStyle;

        private readonly int weight;

        private readonly float italicAngle;

        private readonly bool isMonospace;

        internal FontProgramDescriptor(FontNames fontNames, float italicAngle, bool isMonospace) {
            this.fontName = fontNames.GetFontName();
            this.fontNameLowerCase = this.fontName.ToLowerInvariant();
            this.fullNameLowerCase = fontNames.GetFullName()[0][3].ToLowerInvariant();
            this.familyNameLowerCase = fontNames.GetFamilyName() != null && fontNames.GetFamilyName()[0][3] != null ? 
                fontNames.GetFamilyName()[0][3].ToLowerInvariant() : null;
            this.style = fontNames.GetStyle();
            this.weight = fontNames.GetFontWeight();
            this.macStyle = fontNames.GetMacStyle();
            this.italicAngle = italicAngle;
            this.isMonospace = isMonospace;
        }

        internal FontProgramDescriptor(FontNames fontNames, FontMetrics fontMetrics)
            : this(fontNames, fontMetrics.GetItalicAngle(), fontMetrics.IsFixedPitch()) {
        }

        public virtual String GetFontName() {
            return fontName;
        }

        public virtual String GetStyle() {
            return style;
        }

        public virtual int GetFontWeight() {
            return weight;
        }

        public virtual float GetItalicAngle() {
            return italicAngle;
        }

        public virtual bool IsMonospace() {
            return isMonospace;
        }

        public virtual bool IsBold() {
            return (macStyle & FontNames.BOLD_FLAG) != 0;
        }

        public virtual bool IsItalic() {
            return (macStyle & FontNames.ITALIC_FLAG) != 0;
        }

        public virtual String GetFullNameLowerCase() {
            return fullNameLowerCase;
        }

        public virtual String GetFontNameLowerCase() {
            return fontNameLowerCase;
        }

        public virtual String GetFamilyNameLowerCase() {
            return familyNameLowerCase;
        }
    }
}
