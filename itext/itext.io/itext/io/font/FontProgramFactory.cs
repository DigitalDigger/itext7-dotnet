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
using System.Collections.Generic;

namespace iText.IO.Font {
    /// <summary>Provides methods for creating various types of fonts.</summary>
    public sealed class FontProgramFactory {
        /// <summary>This is the default value of the <VAR>cached</VAR> variable.</summary>
        private static bool DEFAULT_CACHED = true;

        private static FontRegisterProvider fontRegisterProvider = new FontRegisterProvider();

        private FontProgramFactory() {
        }

        /// <summary>Creates a new standard Helvetica font program file.</summary>
        /// <returns>
        /// a
        /// <see cref="FontProgram"/>
        /// object with Helvetica font description
        /// </returns>
        /// <exception cref="System.IO.IOException"/>
        public static FontProgram CreateFont() {
            return CreateFont(FontConstants.HELVETICA);
        }

        /// <summary>Creates a new font program.</summary>
        /// <remarks>
        /// Creates a new font program. This font program can be one of the 14 built in fonts,
        /// a Type1 font referred to by an AFM or PFM file, a TrueType font (simple or one from collection) or
        /// a CJK font from the Adobe Asian Font Pack.
        /// TrueType fonts and CJK fonts can have an optional style modifier
        /// appended to the name. These modifiers are: Bold, Italic and BoldItalic. An
        /// example would be "STSong-Light,Bold". Note that this modifiers do not work if
        /// the font is embedded. Fonts in TrueType Collections are addressed by index such as "msgothic.ttc,1".
        /// This would get the second font (indexes start at 0), in this case "MS PGothic".
        /// <p/>
        /// The fonts are cached and if they already exist they are extracted from the cache,
        /// not parsed again.
        /// <p/>
        /// </remarks>
        /// <param name="fontProgram">the name of the font or its location on file</param>
        /// <returns>
        /// returns a new
        /// <see cref="FontProgram"/>
        /// . This font program may come from the cache
        /// </returns>
        /// <exception cref="System.IO.IOException"/>
        public static FontProgram CreateFont(String fontProgram) {
            return CreateFont(fontProgram, null, DEFAULT_CACHED);
        }

        /// <summary>Creates a new font program.</summary>
        /// <remarks>
        /// Creates a new font program. This font program can be one of the 14 built in fonts,
        /// a Type1 font referred to by an AFM or PFM file, a TrueType font (simple or one from collection) or
        /// a CJK font from the Adobe Asian Font Pack.
        /// TrueType fonts and CJK fonts can have an optional style modifier
        /// appended to the name. These modifiers are: Bold, Italic and BoldItalic. An
        /// example would be "STSong-Light,Bold". Note that this modifiers do not work if
        /// the font is embedded. Fonts in TrueType Collections are addressed by index such as "msgothic.ttc,1".
        /// This would get the second font (indexes start at 0), in this case "MS PGothic".
        /// <p/>
        /// The fonts are cached and if they already exist they are extracted from the cache,
        /// not parsed again.
        /// <p/>
        /// </remarks>
        /// <param name="fontProgram">the name of the font or its location on file</param>
        /// <param name="cached">whether to to cache this font program after it has been loaded</param>
        /// <returns>
        /// returns a new
        /// <see cref="FontProgram"/>
        /// . This font program may come from the cache
        /// </returns>
        /// <exception cref="System.IO.IOException"/>
        public static FontProgram CreateFont(String fontProgram, bool cached) {
            return CreateFont(fontProgram, null, cached);
        }

        /// <summary>Creates a new font program.</summary>
        /// <remarks>
        /// Creates a new font program. This font program can be one of the 14 built in fonts,
        /// a Type1 font referred to by an AFM or PFM file, a TrueType font (simple only) or
        /// a CJK font from the Adobe Asian Font Pack.
        /// TrueType fonts and CJK fonts can have an optional style modifier
        /// appended to the name. These modifiers are: Bold, Italic and BoldItalic. An
        /// example would be "STSong-Light,Bold". Note that this modifiers do not work if
        /// the font is embedded. Fonts in TrueType Collections are addressed by index such as "msgothic.ttc,1".
        /// This would get the second font (indexes start at 0), in this case "MS PGothic".
        /// <p/>
        /// The fonts are cached and if they already exist they are extracted from the cache,
        /// not parsed again.
        /// <p/>
        /// </remarks>
        /// <param name="fontProgram">the byte contents of the font program</param>
        /// <returns>
        /// returns a new
        /// <see cref="FontProgram"/>
        /// . This font program may come from the cache
        /// </returns>
        /// <exception cref="System.IO.IOException"/>
        public static FontProgram CreateFont(byte[] fontProgram) {
            return CreateFont(null, fontProgram, DEFAULT_CACHED);
        }

        /// <summary>Creates a new font program.</summary>
        /// <remarks>
        /// Creates a new font program. This font program can be one of the 14 built in fonts,
        /// a Type 1 font referred to by an AFM or PFM file, a TrueType font (simple only) or
        /// a CJK font from the Adobe Asian Font Pack.
        /// TrueType fonts and CJK fonts can have an optional style modifier
        /// appended to the name. These modifiers are: Bold, Italic and BoldItalic. An
        /// example would be "STSong-Light,Bold". Note that this modifiers do not work if
        /// the font is embedded. Fonts in TrueType Collections are addressed by index such as "msgothic.ttc,1".
        /// This would get the second font (indexes start at 0), in this case "MS PGothic".
        /// <p/>
        /// The fonts are cached and if they already exist they are extracted from the cache,
        /// not parsed again.
        /// <p/>
        /// </remarks>
        /// <param name="fontProgram">the byte contents of the font program</param>
        /// <param name="cached">whether to to cache this font program</param>
        /// <returns>
        /// returns a new
        /// <see cref="FontProgram"/>
        /// . This font program may come from the cache
        /// </returns>
        /// <exception cref="System.IO.IOException"/>
        public static FontProgram CreateFont(byte[] fontProgram, bool cached) {
            return CreateFont(null, fontProgram, cached);
        }

        /// <summary>This method is deprecated and will be made private in 7.1</summary>
        /// <exception cref="System.IO.IOException"/>
        [System.ObsoleteAttribute(@"Use CreateFont(byte[], bool) or CreateFont(System.String, bool)")]
        public static FontProgram CreateFont(String name, byte[] fontProgram, bool cached) {
            String baseName = FontProgram.GetBaseName(name);
            //yes, we trying to find built-in standard font with original name, not baseName.
            bool isBuiltinFonts14 = FontConstants.BUILTIN_FONTS_14.Contains(name);
            bool isCidFont = !isBuiltinFonts14 && FontCache.IsPredefinedCidFont(baseName);
            FontProgram fontFound;
            FontCacheKey fontKey = null;
            if (cached) {
                fontKey = CreateFontCacheKey(name, fontProgram);
                fontFound = FontCache.GetFont(fontKey);
                if (fontFound != null) {
                    return fontFound;
                }
            }
            FontProgram fontBuilt = null;
            if (name == null) {
                if (fontProgram != null) {
                    try {
                        fontBuilt = new TrueTypeFont(fontProgram);
                    }
                    catch (Exception) {
                    }
                    if (fontBuilt == null) {
                        try {
                            fontBuilt = new Type1Font(null, null, fontProgram, null);
                        }
                        catch (Exception) {
                        }
                    }
                }
            }
            else {
                if (isBuiltinFonts14 || name.ToLowerInvariant().EndsWith(".afm") || name.ToLowerInvariant().EndsWith(".pfm"
                    )) {
                    fontBuilt = new Type1Font(name, null, null, null);
                }
                else {
                    if (isCidFont) {
                        fontBuilt = new CidFont(name, FontCache.GetCompatibleCmaps(baseName));
                    }
                    else {
                        if (baseName.ToLowerInvariant().EndsWith(".ttf") || baseName.ToLowerInvariant().EndsWith(".otf")) {
                            if (fontProgram != null) {
                                fontBuilt = new TrueTypeFont(fontProgram);
                            }
                            else {
                                fontBuilt = new TrueTypeFont(name);
                            }
                        }
                        else {
                            int ttcSplit = baseName.ToLowerInvariant().IndexOf(".ttc,", StringComparison.Ordinal);
                            if (ttcSplit > 0) {
                                try {
                                    String ttcName = baseName.JSubstring(0, ttcSplit + 4);
                                    //count(.ttc) = 4
                                    int ttcIndex = System.Convert.ToInt32(baseName.Substring(ttcSplit + 5));
                                    //count(.ttc,) = 5)
                                    fontBuilt = new TrueTypeFont(ttcName, ttcIndex);
                                }
                                catch (FormatException nfe) {
                                    throw new iText.IO.IOException(nfe.Message, nfe);
                                }
                            }
                        }
                    }
                }
            }
            if (fontBuilt == null) {
                if (name != null) {
                    throw new iText.IO.IOException(iText.IO.IOException.TypeOfFont1IsNotRecognized).SetMessageParams(name);
                }
                else {
                    throw new iText.IO.IOException(iText.IO.IOException.TypeOfFontIsNotRecognized);
                }
            }
            return cached ? FontCache.SaveFont(fontBuilt, fontKey) : fontBuilt;
        }

        /// <summary>This method is deprecated and will be completely removed in 7.1</summary>
        /// <exception cref="System.IO.IOException"/>
        [System.ObsoleteAttribute(@"Use CreateType1Font(byte[], byte[]) or CreateType1Font(System.String, System.String) instead"
            )]
        public static FontProgram CreateType1Font(String name, byte[] afm, byte[] pfb, bool cached) {
            FontProgram fontProgram;
            FontCacheKey fontKey = null;
            if (cached) {
                fontKey = CreateFontCacheKey(name, afm);
                fontProgram = FontCache.GetFont(fontKey);
                if (fontProgram != null) {
                    return fontProgram;
                }
            }
            fontProgram = new Type1Font(name, null, afm, pfb);
            return cached ? FontCache.SaveFont(fontProgram, fontKey) : fontProgram;
        }

        /// <summary>Creates a new Type 1 font by the byte contents of the corresponding AFM/PFM and PFB files</summary>
        /// <param name="afm">the contents of the AFM or PFM metrics file</param>
        /// <param name="pfb">the contents of the PFB file</param>
        /// <returns>
        /// created
        /// <see cref="FontProgram"/>
        /// instance
        /// </returns>
        /// <exception cref="System.IO.IOException"/>
        public static FontProgram CreateType1Font(byte[] afm, byte[] pfb) {
            return CreateType1Font(afm, pfb, DEFAULT_CACHED);
        }

        /// <summary>Creates a new Type 1 font by the byte contents of the corresponding AFM/PFM and PFB files</summary>
        /// <param name="afm">the contents of the AFM or PFM metrics file</param>
        /// <param name="pfb">the contents of the PFB file</param>
        /// <param name="cached">
        /// specifies whether to cache the created
        /// <see cref="FontProgram"/>
        /// or not
        /// </param>
        /// <returns>
        /// created
        /// <see cref="FontProgram"/>
        /// instance
        /// </returns>
        /// <exception cref="System.IO.IOException"/>
        public static FontProgram CreateType1Font(byte[] afm, byte[] pfb, bool cached) {
            return CreateType1Font(null, null, afm, pfb, cached);
        }

        /// <summary>Creates a new Type 1 font by the corresponding AFM/PFM and PFB files</summary>
        /// <param name="metricsPath">path to the AFM or PFM metrics file</param>
        /// <param name="binaryPath">path to the contents of the PFB file</param>
        /// <returns>
        /// created
        /// <see cref="FontProgram"/>
        /// instance
        /// </returns>
        /// <exception cref="System.IO.IOException"/>
        public static FontProgram CreateType1Font(String metricsPath, String binaryPath) {
            return CreateType1Font(metricsPath, binaryPath, DEFAULT_CACHED);
        }

        /// <summary>Creates a new Type 1 font by the corresponding AFM/PFM and PFB files</summary>
        /// <param name="metricsPath">path to the AFM or PFM metrics file</param>
        /// <param name="binaryPath">path to the contents of the PFB file</param>
        /// <param name="cached">
        /// specifies whether to cache the created
        /// <see cref="FontProgram"/>
        /// or not
        /// </param>
        /// <returns>
        /// created
        /// <see cref="FontProgram"/>
        /// instance
        /// </returns>
        /// <exception cref="System.IO.IOException"/>
        public static FontProgram CreateType1Font(String metricsPath, String binaryPath, bool cached) {
            return CreateType1Font(metricsPath, binaryPath, null, null, cached);
        }

        /// <summary>Creates a new TrueType font program from ttc (TrueType Collection) file.</summary>
        /// <param name="ttc">location  of TrueType Collection file (*.ttc)</param>
        /// <param name="ttcIndex">the index of the font file from the collection to be read</param>
        /// <param name="cached">
        /// true if the font comes from the cache or is added to
        /// the cache if new, false if the font is always created new
        /// </param>
        /// <returns>
        /// returns a new
        /// <see cref="FontProgram"/>
        /// instance. This font may come from the cache but only if cached
        /// is true, otherwise it will always be created new
        /// </returns>
        /// <exception cref="System.IO.IOException"/>
        public static FontProgram CreateFont(String ttc, int ttcIndex, bool cached) {
            FontCacheKey fontCacheKey = FontCacheKey.Create(ttc, ttcIndex);
            if (cached) {
                FontProgram fontFound = FontCache.GetFont(fontCacheKey);
                if (fontFound != null) {
                    return fontFound;
                }
            }
            FontProgram fontBuilt = new TrueTypeFont(ttc, ttcIndex);
            return cached ? FontCache.SaveFont(fontBuilt, fontCacheKey) : fontBuilt;
        }

        /// <summary>Creates a new TrueType font program from ttc (TrueType Collection) file bytes.</summary>
        /// <param name="ttc">the content of a TrueType Collection file (*.ttc)</param>
        /// <param name="ttcIndex">the index of the font file from the collection to be read</param>
        /// <param name="cached">
        /// true if the font comes from the cache or is added to
        /// the cache if new, false if the font is always created new
        /// </param>
        /// <returns>
        /// returns a new
        /// <see cref="FontProgram"/>
        /// instance. This font may come from the cache but only if cached
        /// is true, otherwise it will always be created new
        /// </returns>
        /// <exception cref="System.IO.IOException"/>
        public static FontProgram CreateFont(byte[] ttc, int ttcIndex, bool cached) {
            FontCacheKey fontKey = FontCacheKey.Create(ttc, ttcIndex);
            if (cached) {
                FontProgram fontFound = FontCache.GetFont(fontKey);
                if (fontFound != null) {
                    return fontFound;
                }
            }
            FontProgram fontBuilt = new TrueTypeFont(ttc, ttcIndex);
            return cached ? FontCache.SaveFont(fontBuilt, fontKey) : fontBuilt;
        }

        /// <summary>Creates a FontProgram from the font file that has been previously registered.</summary>
        /// <param name="fontName">
        /// either a font alias, if the font file has been registered with an alias,
        /// or just a font name otherwise
        /// </param>
        /// <param name="style">
        /// the style of the font to look for. Possible values are listed in
        /// <see cref="FontConstants"/>
        /// .
        /// See
        /// <see cref="FontConstants.BOLD"/>
        /// ,
        /// <see cref="FontConstants.ITALIC"/>
        /// ,
        /// <see cref="FontConstants.NORMAL"/>
        /// ,
        /// <see cref="FontConstants.BOLDITALIC"/>
        /// ,
        /// <see cref="FontConstants.UNDEFINED"/>
        /// </param>
        /// <param name="cached">whether to try to get the font program from cache</param>
        /// <returns>
        /// created
        /// <see cref="FontProgram"/>
        /// </returns>
        /// <exception cref="System.IO.IOException"/>
        public static FontProgram CreateRegisteredFont(String fontName, int style, bool cached) {
            return fontRegisterProvider.GetFont(fontName, style, cached);
        }

        /// <summary>Creates a FontProgram from the font file that has been previously registered.</summary>
        /// <param name="fontName">
        /// either a font alias, if the font file has been registered with an alias,
        /// or just a font name otherwise
        /// </param>
        /// <param name="style">
        /// the style of the font to look for. Possible values are listed in
        /// <see cref="FontConstants"/>
        /// .
        /// See
        /// <see cref="FontConstants.BOLD"/>
        /// ,
        /// <see cref="FontConstants.ITALIC"/>
        /// ,
        /// <see cref="FontConstants.NORMAL"/>
        /// ,
        /// <see cref="FontConstants.BOLDITALIC"/>
        /// ,
        /// <see cref="FontConstants.UNDEFINED"/>
        /// </param>
        /// <returns>
        /// created
        /// <see cref="FontProgram"/>
        /// </returns>
        /// <exception cref="System.IO.IOException"/>
        public static FontProgram CreateRegisteredFont(String fontName, int style) {
            return fontRegisterProvider.GetFont(fontName, style);
        }

        /// <summary>Creates a FontProgram from the font file that has been previously registered.</summary>
        /// <param name="fontName">
        /// either a font alias, if the font file has been registered with an alias,
        /// or just a font name otherwise
        /// </param>
        /// <returns>
        /// created
        /// <see cref="FontProgram"/>
        /// </returns>
        /// <exception cref="System.IO.IOException"/>
        public static FontProgram CreateRegisteredFont(String fontName) {
            return fontRegisterProvider.GetFont(fontName, FontConstants.UNDEFINED);
        }

        /// <summary>Register a font by giving explicitly the font family and name.</summary>
        /// <param name="familyName">the font family</param>
        /// <param name="fullName">the font name</param>
        /// <param name="path">the font path</param>
        public static void RegisterFontFamily(String familyName, String fullName, String path) {
            fontRegisterProvider.RegisterFontFamily(familyName, fullName, path);
        }

        /// <summary>Registers a .ttf, .otf, .afm, .pfm, or a .ttc font file.</summary>
        /// <remarks>
        /// Registers a .ttf, .otf, .afm, .pfm, or a .ttc font file.
        /// In case if TrueType Collection (.ttc), an additional parameter may be specified defining the index of the font
        /// to be registered, e.g. "path/to/font/collection.ttc,0". The index is zero-based.
        /// </remarks>
        /// <param name="path">the path to a font file</param>
        public static void RegisterFont(String path) {
            RegisterFont(path, null);
        }

        /// <summary>Register a font file and use an alias for the font contained in it.</summary>
        /// <param name="path">the path to a font file</param>
        /// <param name="alias">the alias you want to use for the font</param>
        public static void RegisterFont(String path, String alias) {
            fontRegisterProvider.RegisterFont(path, alias);
        }

        /// <summary>Register all the fonts in a directory.</summary>
        /// <param name="dir">the directory</param>
        /// <returns>the number of fonts registered</returns>
        public static int RegisterFontDirectory(String dir) {
            return fontRegisterProvider.RegisterFontDirectory(dir);
        }

        /// <summary>Register fonts in some probable directories.</summary>
        /// <remarks>
        /// Register fonts in some probable directories. It usually works in Windows,
        /// Linux and Solaris.
        /// </remarks>
        /// <returns>the number of fonts registered</returns>
        public static int RegisterSystemFontDirectories() {
            return fontRegisterProvider.RegisterSystemFontDirectories();
        }

        /// <summary>Gets a set of registered font names.</summary>
        /// <returns>a set of registered fonts</returns>
        public static ICollection<String> GetRegisteredFonts() {
            return fontRegisterProvider.GetRegisteredFonts();
        }

        /// <summary>Gets a set of registered font names.</summary>
        /// <returns>a set of registered font families</returns>
        public static ICollection<String> GetRegisteredFontFamilies() {
            return fontRegisterProvider.GetRegisteredFontFamilies();
        }

        /// <summary>Checks if a certain font is registered.</summary>
        /// <param name="fontName">the name of the font that has to be checked.</param>
        /// <returns>true if the font is found</returns>
        public static bool IsRegisteredFont(String fontName) {
            return fontRegisterProvider.IsRegisteredFont(fontName);
        }

        /// <exception cref="System.IO.IOException"/>
        private static FontProgram CreateType1Font(String metricsPath, String binaryPath, byte[] afm, byte[] pfb, 
            bool cached) {
            FontProgram fontProgram;
            FontCacheKey fontKey = null;
            if (cached) {
                fontKey = CreateFontCacheKey(metricsPath, afm);
                fontProgram = FontCache.GetFont(fontKey);
                if (fontProgram != null) {
                    return fontProgram;
                }
            }
            fontProgram = new Type1Font(metricsPath, binaryPath, afm, pfb);
            return cached ? FontCache.SaveFont(fontProgram, fontKey) : fontProgram;
        }

        private static FontCacheKey CreateFontCacheKey(String name, byte[] fontProgram) {
            FontCacheKey key;
            if (name != null) {
                key = FontCacheKey.Create(name);
            }
            else {
                key = FontCacheKey.Create(fontProgram);
            }
            return key;
        }

        public static void ClearRegisteredFonts() {
            fontRegisterProvider.ClearRegisteredFonts();
        }

        public static void ClearRegisteredFontFamilies() {
            fontRegisterProvider.ClearRegisteredFontFamilies();
        }
    }
}
