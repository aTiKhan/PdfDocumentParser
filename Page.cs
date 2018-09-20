//********************************************************************************************
//Author: Sergey Stoyan
//        sergey.stoyan@gmail.com
//        http://www.cliversoft.com
//********************************************************************************************
using System;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.IO;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
using System.Text.RegularExpressions;
//using iTextSharp.text.pdf.parser;
using System.Drawing;

namespace Cliver.PdfDocumentParser
{
    /// <summary>
    /// pdf page parsing API
    /// </summary>
    public class Page : IDisposable
    {
        internal Page(PageCollection pageCollection, int pageI)
        {
            this.pageCollection = pageCollection;
            this.pageI = pageI;
        }
        int pageI;
        PageCollection pageCollection;

        ~Page()
        {
            Dispose();
        }

        public void Dispose()
        {
            lock (this)
            {
                if (_bitmap != null)
                {
                    _bitmap.Dispose();
                    _bitmap = null;
                }
                if (_activeTemplateBitmap != null)
                {
                    _activeTemplateBitmap.Dispose();
                    _activeTemplateBitmap = null;
                }
                if (_activeTemplateImageData != null)
                {
                    //_activeTemplateImageData.Dispose();
                    _activeTemplateImageData = null;
                }
                if (_pdfCharBoxs != null)
                {
                    _pdfCharBoxs = null;
                }
                if (_activeTemplateOcrCharBoxs != null)
                {
                    _activeTemplateOcrCharBoxs = null;
                }
            }
        }

        internal Bitmap Bitmap
        {
            get
            {
                if (_bitmap == null)
                    _bitmap = Pdf.RenderBitmap(pageCollection.PdfFile, pageI, Settings.ImageProcessing.PdfPageImageResolution);
                return _bitmap;
            }
        }
        Bitmap _bitmap;

        internal void OnActiveTemplateUpdating(Template newTemplate)
        {
            if (pageCollection.ActiveTemplate == null)
                return;

            if (newTemplate.PagesRotation != pageCollection.ActiveTemplate.PagesRotation || newTemplate.AutoDeskew != pageCollection.ActiveTemplate.AutoDeskew)
            {
                if (_activeTemplateImageData != null)
                {
                    //_activeTemplateImageData.Dispose();
                    _activeTemplateImageData = null;
                }
                if (_activeTemplateBitmap != null)
                {
                    _activeTemplateBitmap.Dispose();
                    _activeTemplateBitmap = null;
                }
                if (_activeTemplateOcrCharBoxs != null)
                    _activeTemplateOcrCharBoxs = null;

                floatingAnchorHashes2rectangles.Clear();
            }

            //if (pageCollection.ActiveTemplate.Name != newTemplate.Name)
            //    floatingAnchorValueStrings2rectangles.Clear();
        }

        Bitmap getRectangleFromActiveTemplateBitmap(float x, float y, float w, float h)
        {
            RectangleF r = new RectangleF(0, 0, ActiveTemplateBitmap.Width, ActiveTemplateBitmap.Height);
            r.Intersect(new RectangleF(x, y, w, h));
            return ActiveTemplateBitmap.Clone(r, System.Drawing.Imaging.PixelFormat.Undefined);
            //return ImageRoutines.GetCopy(ActiveTemplateBitmap, new RectangleF(x, y, w, h));
        }

        internal Bitmap ActiveTemplateBitmap
        {
            get
            {
                if (_activeTemplateBitmap == null)
                {
                    if (pageCollection.ActiveTemplate == null)
                        return null;

                    //From stackoverflow:
                    //Using the Graphics class to modify the clone (created with .Clone()) will not modify the original.
                    //Similarly, using the LockBits method yields different memory blocks for the original and clone.
                    //...change one random pixel to a random color on the clone... seems to trigger a copy of all pixel data from the original.
                    Bitmap b = Bitmap.Clone(new Rectangle(0, 0, Bitmap.Width, Bitmap.Height), System.Drawing.Imaging.PixelFormat.Undefined);

                    if (pageCollection.ActiveTemplate.PagesRotation != Template.PageRotations.NONE)
                    {
                        //b = ImageRoutines.GetCopy(Bitmap);
                        switch (pageCollection.ActiveTemplate.PagesRotation)
                        {
                            case Template.PageRotations.NONE:
                                break;
                            case Template.PageRotations.Clockwise90:
                                b.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                break;
                            case Template.PageRotations.Clockwise180:
                                b.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                break;
                            case Template.PageRotations.Clockwise270:
                                b.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                break;
                            default:
                                throw new Exception("Unknown option: " + pageCollection.ActiveTemplate.PagesRotation);
                        }
                    }

                    if (pageCollection.ActiveTemplate.AutoDeskew)
                    {
                        using (ImageMagick.MagickImage image = new ImageMagick.MagickImage(b))
                        {
                            //image.Density = new PointD(600, 600);
                            //image.AutoLevel();
                            //image.Negate();
                            //image.AdaptiveThreshold(10, 10, new ImageMagick.Percentage(20));
                            //image.Negate();
                            image.Deskew(new ImageMagick.Percentage(pageCollection.ActiveTemplate.AutoDeskewThreshold));
                            //image.AutoThreshold(AutoThresholdMethod.OTSU);
                            //image.Despeckle();
                            //image.WhiteThreshold(new Percentage(20));
                            //image.Trim();
                            b.Dispose();
                            b = image.ToBitmap();
                        }
                    }

                    _activeTemplateBitmap = b;
                }
                return _activeTemplateBitmap;
            }
        }

        Bitmap _activeTemplateBitmap = null;

        internal PointF? GetFloatingAnchorPoint0(int floatingAnchorId)
        {
            Template.FloatingAnchor fa = pageCollection.ActiveTemplate.FloatingAnchors.Find(a => a.Id == floatingAnchorId);
            List<RectangleF> rs = GetFloatingAnchorRectangles(fa);
            if (rs == null || rs.Count < 1)
                return null;
            return new PointF(rs[0].X, rs[0].Y);
        }

        internal List<RectangleF> GetFloatingAnchorRectangles(Template.FloatingAnchor fa)
        {
            List<RectangleF> rs;
            string fas = SerializationRoutines.Json.Serialize(fa);
            if (!floatingAnchorHashes2rectangles.TryGetValue(fas, out rs))
            {
                rs = findFloatingAnchor(fa);
                floatingAnchorHashes2rectangles[fas] = rs;
            }
            return rs;
        }
        Dictionary<string, List<RectangleF>> floatingAnchorHashes2rectangles = new Dictionary<string, List<RectangleF>>();

        List<RectangleF> findFloatingAnchor(Template.FloatingAnchor fa)
        {
            if (fa == null || fa.Value == null)
                return null;

            switch (fa.ValueType)
            {
                case Template.ValueTypes.PdfText:
                    {
                        Template.FloatingAnchor.PdfTextValue ptv = (Template.FloatingAnchor.PdfTextValue)fa.Value;
                        List<Template.FloatingAnchor.PdfTextValue.CharBox> faes = ptv.CharBoxs;
                        if (faes.Count < 1)
                            return null;
                        IEnumerable<Pdf.CharBox> bt0s;
                        if (ptv.SearchRectangleMargin < 0)
                            bt0s = PdfCharBoxs.Where(a => a.Char == faes[0].Char);
                        else
                        {
                            RectangleF sr = Template.FloatingAnchor.BaseValue.GetSearchRectangle(faes[0].Rectangle, ptv.SearchRectangleMargin);
                            bt0s = PdfCharBoxs.Where(a => a.Char == faes[0].Char && sr.Contains(a.R));
                        }
                        List<Pdf.CharBox> bts = new List<Pdf.CharBox>();
                        foreach (Pdf.CharBox bt0 in bt0s)
                        {
                            bts.Clear();
                            bts.Add(bt0);
                            for (int i = 1; i < faes.Count; i++)
                            {
                                float x, y;
                                if (ptv.PositionDeviationIsAbsolute)
                                {
                                    x = bt0.R.X + faes[i].Rectangle.X - faes[0].Rectangle.X;
                                    y = bt0.R.Y + faes[i].Rectangle.Y - faes[0].Rectangle.Y;
                                }
                                else
                                {
                                    x = bts[i - 1].R.X + faes[i].Rectangle.X - faes[i - 1].Rectangle.X;
                                    y = bts[i - 1].R.Y + faes[i].Rectangle.Y - faes[i - 1].Rectangle.Y;
                                }
                                foreach (Pdf.CharBox bt in PdfCharBoxs.Where(a => a.Char == faes[i].Char))
                                    if (Math.Abs(bt.R.X - x) <= ptv.PositionDeviation && Math.Abs(bt.R.Y - y) <= ptv.PositionDeviation)
                                    {
                                        bts.Add(bt);
                                        break;
                                    }
                                if (bts.Count - 1 < i)
                                    break;
                            }
                            if (bts.Count == faes.Count)
                                return bts.Select(x => x.R).ToList();
                        }
                    }
                    return null;
                case Template.ValueTypes.OcrText:
                    {
                        Template.FloatingAnchor.OcrTextValue otv = (Template.FloatingAnchor.OcrTextValue)fa.Value;
                        List<Template.FloatingAnchor.OcrTextValue.CharBox> faes = otv.CharBoxs;
                        if (faes.Count < 1)
                            return null;
                        IEnumerable<Ocr.CharBox> bt0s;
                        if (otv.SearchRectangleMargin < 0)
                            bt0s = ActiveTemplateOcrCharBoxs.Where(a => a.Char == faes[0].Char);
                        else
                        {
                            RectangleF sr = Template.FloatingAnchor.BaseValue.GetSearchRectangle(faes[0].Rectangle, otv.SearchRectangleMargin);
                            bt0s = ActiveTemplateOcrCharBoxs.Where(a => a.Char == faes[0].Char && sr.Contains(a.R));
                        }
                        List<Ocr.CharBox> bts = new List<Ocr.CharBox>();
                        foreach (Ocr.CharBox bt0 in bt0s)
                        {
                            bts.Clear();
                            bts.Add(bt0);
                            for (int i = 1; i < faes.Count; i++)
                            {
                                float x, y;
                                if (otv.PositionDeviationIsAbsolute)
                                {
                                    x = bt0.R.X + faes[i].Rectangle.X - faes[0].Rectangle.X;
                                    y = bt0.R.Y + faes[i].Rectangle.Y - faes[0].Rectangle.Y;
                                }
                                else
                                {
                                    x = bts[i - 1].R.X + faes[i].Rectangle.X - faes[i - 1].Rectangle.X;
                                    y = bts[i - 1].R.Y + faes[i].Rectangle.Y - faes[i - 1].Rectangle.Y;
                                }
                                foreach (Ocr.CharBox bt in ActiveTemplateOcrCharBoxs.Where(a => a.Char == faes[i].Char))
                                    if (Math.Abs(bt.R.X - x) <= otv.PositionDeviation && Math.Abs(bt.R.Y - y) <= otv.PositionDeviation)
                                    {
                                        bts.Add(bt);
                                        break;
                                    }
                                if (bts.Count - 1 < i)
                                    break;
                            }
                            if (bts.Count == faes.Count)
                                return bts.Select(x => x.R).ToList();
                        }
                    }
                    return null;
                case Template.ValueTypes.ImageData:
                    {
                        Template.FloatingAnchor.ImageDataValue idv = (Template.FloatingAnchor.ImageDataValue)fa.Value;
                        List<Template.FloatingAnchor.ImageDataValue.ImageBox> ibs = idv.ImageBoxs;
                        if (ibs.Count < 1)
                            return null;
                        Point shift;
                        ImageData id0;
                        if (idv.SearchRectangleMargin < 0)
                        {
                            id0 = ActiveTemplateImageData;
                            shift = new Point(0, 0);
                        }
                        else
                        {
                            RectangleF sr = Template.FloatingAnchor.BaseValue.GetSearchRectangle(ibs[0].Rectangle, idv.SearchRectangleMargin);
                            id0 = new ImageData(getRectangleFromActiveTemplateBitmap(sr.X / Settings.ImageProcessing.Image2PdfResolutionRatio, sr.Y / Settings.ImageProcessing.Image2PdfResolutionRatio, sr.Width / Settings.ImageProcessing.Image2PdfResolutionRatio, sr.Height / Settings.ImageProcessing.Image2PdfResolutionRatio));
                            shift = new Point(sr.X < 0 ? 0 : (int)sr.X, sr.Y < 0 ? 0 : (int)sr.Y);
                        }
                        List<RectangleF> bestRs = null;
                        float minDeviation = 1;
                        ibs[0].ImageData.FindWithinImage(id0, idv.BrightnessTolerance, idv.DifferentPixelNumberTolerance, (Point point0, float deviation) =>
                        {
                            point0 = new Point(shift.X + point0.X, shift.Y + point0.Y);
                            List<RectangleF> rs = new List<RectangleF>();
                            rs.Add(new RectangleF(point0, new SizeF(ibs[0].Rectangle.Width, ibs[0].Rectangle.Height)));
                            for (int i = 1; i < ibs.Count; i++)
                            {
                                //Template.RectangleF r = new Template.RectangleF(point0.X + ibs[i].Rectangle.X - ibs[0].Rectangle.X, point0.Y + ibs[i].Rectangle.Y - ibs[0].Rectangle.Y, ibs[i].Rectangle.Width, ibs[i].Rectangle.Height);
                                //using (Bitmap rb = getRectangleFromActiveTemplateBitmap(r.X / Settings.ImageProcessing.Image2PdfResolutionRatio, r.Y / Settings.ImageProcessing.Image2PdfResolutionRatio, r.Width / Settings.ImageProcessing.Image2PdfResolutionRatio, r.Height / Settings.ImageProcessing.Image2PdfResolutionRatio))
                                //{
                                //    if (!ibs[i].ImageData.ImageIsSimilar(new ImageData(rb), pageCollection.ActiveTemplate.BrightnessTolerance, pageCollection.ActiveTemplate.DifferentPixelNumberTolerance))
                                //        return true;
                                //}
                                Template.RectangleF r = new Template.RectangleF(point0.X + ibs[i].Rectangle.X - ibs[0].Rectangle.X - idv.PositionDeviation, point0.Y + ibs[i].Rectangle.Y - ibs[0].Rectangle.Y - idv.PositionDeviation, ibs[i].Rectangle.Width + idv.PositionDeviation, ibs[i].Rectangle.Height + idv.PositionDeviation);
                                using (Bitmap rb = getRectangleFromActiveTemplateBitmap(r.X / Settings.ImageProcessing.Image2PdfResolutionRatio, r.Y / Settings.ImageProcessing.Image2PdfResolutionRatio, r.Width / Settings.ImageProcessing.Image2PdfResolutionRatio, r.Height / Settings.ImageProcessing.Image2PdfResolutionRatio))
                                {
                                    if (null == ibs[i].ImageData.FindWithinImage(new ImageData(rb), idv.BrightnessTolerance, idv.DifferentPixelNumberTolerance, false))
                                        return true;
                                }
                                rs.Add(r.GetSystemRectangleF());
                            }
                            if (!idv.FindBestImageMatch)
                            {
                                bestRs = rs;
                                return false;
                            }
                            if (deviation < minDeviation)
                            {
                                minDeviation = deviation;
                                bestRs = rs;
                            }
                            return true;
                        });
                        return bestRs;
                    }
                default:
                    throw new Exception("Unknown option: " + fa.ValueType);
            }
        }

        internal ImageData ActiveTemplateImageData
        {
            get
            {
                if (_activeTemplateImageData == null)
                    _activeTemplateImageData = new ImageData(ActiveTemplateBitmap);
                return _activeTemplateImageData;
            }
        }
        ImageData _activeTemplateImageData;

        internal List<Pdf.CharBox> PdfCharBoxs
        {
            get
            {
                if (_pdfCharBoxs == null)
                    _pdfCharBoxs = Pdf.GetCharBoxsFromPage(pageCollection.PdfReader, pageI);
                return _pdfCharBoxs;
            }
        }
        List<Pdf.CharBox> _pdfCharBoxs;

        internal List<Ocr.CharBox> ActiveTemplateOcrCharBoxs
        {
            get
            {
                if (_activeTemplateOcrCharBoxs == null)
                {
                    _activeTemplateOcrCharBoxs = Ocr.This.GetCharBoxs(ActiveTemplateBitmap);
                }
                return _activeTemplateOcrCharBoxs;
            }
        }
        List<Ocr.CharBox> _activeTemplateOcrCharBoxs;

        public bool IsDocumentFirstPage()
        {
            string error;
            return IsDocumentFirstPage(out error);
        }

        public bool IsDocumentFirstPage(out string error)
        {
            if (pageCollection.ActiveTemplate.DocumentFirstPageRecognitionMarks == null || pageCollection.ActiveTemplate.DocumentFirstPageRecognitionMarks.Count < 1)
            {
                error = "Template '" + pageCollection.ActiveTemplate.Name + "' has no DocumentFirstPageRecognitionMarks defined.";
                return false;
            }
            foreach (Template.Mark m in pageCollection.ActiveTemplate.DocumentFirstPageRecognitionMarks)
            {
                if (m.FloatingAnchorId != null && m.Value == null)
                {
                    PointF? p0 = GetFloatingAnchorPoint0((int)m.FloatingAnchorId);
                    if (p0 == null)
                    {
                        error = "FloatingAnchor[" + m.FloatingAnchorId + "] not found.";
                        return false;
                    }
                    continue;
                }
                object v2 = GetValue(m.FloatingAnchorId, m.Rectangle, m.ValueType, out error);
                if (v2 == null)
                    return false;
                switch (m.ValueType)
                {
                    case Template.ValueTypes.PdfText:
                        {
                            Template.Mark.PdfTextValue ptv1 = (Template.Mark.PdfTextValue)m.Value;
                            string t1 = ptv1.Text;//it is expected the text is saved normalized
                            string t2 = NormalizeText((string)v2);
                            if (t1 == t2)
                                continue;
                            error = "documentFirstPageRecognitionMark[" + pageCollection.ActiveTemplate.DocumentFirstPageRecognitionMarks.IndexOf(m) + "]:\r\n" + t2 + "\r\n <> \r\n" + t1;
                            return false;
                        }
                    case Template.ValueTypes.OcrText:
                        {
                            Template.Mark.OcrTextValue otv1 = (Template.Mark.OcrTextValue)m.Value;
                            string t1 = otv1.Text;//it is expected the text is saved normalized
                            string t2 = NormalizeText((string)v2);
                            if (t1 == t2)
                                continue;
                            error = "documentFirstPageRecognitionMark[" + pageCollection.ActiveTemplate.DocumentFirstPageRecognitionMarks.IndexOf(m) + "]:\r\n" + t2 + "\r\n <> \r\n" + t1;
                            return false;
                        }
                    case Template.ValueTypes.ImageData:
                        {
                            Template.Mark.ImageDataValue idv1 = (Template.Mark.ImageDataValue)m.Value;
                            if (idv1.ImageData.ImageIsSimilar((ImageData)v2, idv1.BrightnessTolerance, idv1.DifferentPixelNumberTolerance))
                                continue;
                            error = "documentFirstPageRecognitionMark[" + pageCollection.ActiveTemplate.DocumentFirstPageRecognitionMarks.IndexOf(m) + "]: image is not similar.";
                            return false;
                        }
                    default:
                        throw new Exception("Unknown option: " + m.ValueType);
                }
            }
            error = null;
            return true;
        }

        //        public bool IsDocumentFirstPage(out string error)
        //        {
        //            if (pageCollection.ActiveTemplate.DocumentFirstPageRecognitionMarks == null || pageCollection.ActiveTemplate.DocumentFirstPageRecognitionMarks.Count < 1)
        //            {
        //                error = "Template '" + pageCollection.ActiveTemplate.Name + "' has no DocumentFirstPageRecognitionMarks defined.";
        //                return false;
        //            }
        //            foreach (Template.Mark m in pageCollection.ActiveTemplate.DocumentFirstPageRecognitionMarks)
        //            {
        //                if (m.FloatingAnchorId != null && m.GetValue() == null)
        //                {
        //                    PointF? p0 = GetFloatingAnchorPoint0((int)m.FloatingAnchorId);
        //                    if (p0 == null)
        //                    {
        //                        error = "FloatingAnchor[" + m.FloatingAnchorId + "] not found.";
        //                        return false;
        //                    }
        //                    continue;
        //                }
        //                switch (m.ValueType)
        //                {
        //                    case Template.ValueTypes.PdfText:
        //                        {
        //                            List<Template.Mark.FloatingAnchor.PdfTextValue.CharBox> ses = ((Template.FloatingAnchor.PdfTextValue)fa.GetValue()).CharBoxs;
        //                        if (ses.Count< 1)
        //                            return null;
        //                        List<Pdf.CharBox> bts = new List<Pdf.CharBox>();
        //                        foreach (Pdf.CharBox bt0 in PdfCharBoxs.Where(a => a.Char == ses[0].Char))
        //                        {
        //                            bts.Clear();
        //                            bts.Add(bt0);
        //                            for (int i = 1; i<ses.Count; i++)
        //                            {
        //                                float x = bt0.R.X + ses[i].Rectangle.X - ses[0].Rectangle.X;
        //        float y = bt0.R.Y + ses[i].Rectangle.Y - ses[0].Rectangle.Y;
        //                                foreach (Pdf.CharBox bt in PdfCharBoxs.Where(a => a.Char == ses[i].Char))
        //                                {
        //                                    if (Math.Abs(bt.R.X - x) > fa.PositionDeviation)
        //                                        continue;
        //                                    if (Math.Abs(bt.R.Y - y) > fa.PositionDeviation)
        //                                        continue;
        //                                    if (bts.Contains(bt))
        //                                        continue;
        //                                    bts.Add(bt);
        //                                }
        //}
        //                            if (bts.Count == ses.Count)
        //                                return bts.Select(x => x.R).ToList();
        //                        }
        //                    }
        //                    return null;
        //                case Template.ValueTypes.OcrText:
        //                    {
        //                        List<Template.FloatingAnchor.OcrTextValue.CharBox> ses = ((Template.FloatingAnchor.OcrTextValue)fa.GetValue()).CharBoxs;
        //                        if (ses.Count< 1)
        //                            return null;
        //                        List<Ocr.CharBox> bts = new List<Ocr.CharBox>();
        //                        foreach (Ocr.CharBox bt0 in ActiveTemplateOcrCharBoxs.Where(a => a.Char == ses[0].Char))
        //                        {
        //                            bts.Clear();
        //                            bts.Add(bt0);
        //                            for (int i = 1; i<ses.Count; i++)
        //                            {
        //                                float x = bt0.R.X + ses[i].Rectangle.X - ses[0].Rectangle.X;
        //float y = bt0.R.Y + ses[i].Rectangle.Y - ses[0].Rectangle.Y;
        //                                foreach (Ocr.CharBox bt in ActiveTemplateOcrCharBoxs.Where(a => a.Char == ses[i].Char))
        //                                {
        //                                    if (Math.Abs(bt.R.X - x) > fa.PositionDeviation)
        //                                        continue;
        //                                    if (Math.Abs(bt.R.Y - y) > fa.PositionDeviation)
        //                                        continue;
        //                                    if (bts.Contains(bt))
        //                                        continue;
        //                                    bts.Add(bt);
        //                                }
        //                            }
        //                            if (bts.Count == ses.Count)
        //                                return bts.Select(x => x.R).ToList();
        //                        }
        //                    }
        //                    return null;
        //                case Template.ValueTypes.ImageData:
        //                    List<Template.FloatingAnchor.ImageDataValue.ImageBox> ibs = ((Template.FloatingAnchor.ImageDataValue)fa.GetValue()).ImageBoxs;
        //                    if (ibs.Count< 1)
        //                        return null;
        //                    List<RectangleF> bestRs = null;
        //float minDeviation = 1;
        //ibs[0].ImageData.FindWithinImage(ActiveTemplateImageData, pageCollection.ActiveTemplate.BrightnessTolerance, pageCollection.ActiveTemplate.DifferentPixelNumberTolerance, (Point point0, float deviation) =>
        //                    {
        //                        List<RectangleF> rs = new List<RectangleF>();
        //rs.Add(new RectangleF(point0, new SizeF(ibs[0].Rectangle.Width, ibs[0].Rectangle.Height)));
        //                        for (int i = 1; i<ibs.Count; i++)
        //                        {
        //                            //Template.RectangleF r = new Template.RectangleF(point0.X + ibs[i].Rectangle.X - ibs[0].Rectangle.X, point0.Y + ibs[i].Rectangle.Y - ibs[0].Rectangle.Y, ibs[i].Rectangle.Width, ibs[i].Rectangle.Height);
        //                            //using (Bitmap rb = getRectangleFromActiveTemplateBitmap(r.X / Settings.ImageProcessing.Image2PdfResolutionRatio, r.Y / Settings.ImageProcessing.Image2PdfResolutionRatio, r.Width / Settings.ImageProcessing.Image2PdfResolutionRatio, r.Height / Settings.ImageProcessing.Image2PdfResolutionRatio))
        //                            //{
        //                            //    if (!ibs[i].ImageData.ImageIsSimilar(new ImageData(rb), pageCollection.ActiveTemplate.BrightnessTolerance, pageCollection.ActiveTemplate.DifferentPixelNumberTolerance))
        //                            //        return true;
        //                            //}
        //                            Template.RectangleF r = new Template.RectangleF(point0.X + ibs[i].Rectangle.X - ibs[0].Rectangle.X - fa.PositionDeviation, point0.Y + ibs[i].Rectangle.Y - ibs[0].Rectangle.Y - fa.PositionDeviation, ibs[i].Rectangle.Width + fa.PositionDeviation, ibs[i].Rectangle.Height + fa.PositionDeviation);
        //                            using (Bitmap rb = getRectangleFromActiveTemplateBitmap(r.X / Settings.ImageProcessing.Image2PdfResolutionRatio, r.Y / Settings.ImageProcessing.Image2PdfResolutionRatio, r.Width / Settings.ImageProcessing.Image2PdfResolutionRatio, r.Height / Settings.ImageProcessing.Image2PdfResolutionRatio))
        //                            {
        //                                if (null == ibs[i].ImageData.FindWithinImage(new ImageData(rb), pageCollection.ActiveTemplate.BrightnessTolerance, pageCollection.ActiveTemplate.DifferentPixelNumberTolerance, false))
        //                                    return true;
        //                            }
        //                            rs.Add(r.GetSystemRectangleF());
        //                        }
        //                        if (!pageCollection.ActiveTemplate.FindBestImageMatch)
        //                        {
        //                            bestRs = rs;
        //                            return false;
        //                        }
        //                        if (deviation<minDeviation)
        //                        {
        //                            minDeviation = deviation;
        //                            bestRs = rs;
        //                        }
        //                        return true;
        //                    });
        //                    return bestRs;
        //                default:
        //                    throw new Exception("Unknown option: " + fa.ValueType);
        //            }
        //    }

        public object GetValue(int? floatingAnchorId, Template.RectangleF r_, Template.ValueTypes valueType, out string error)
        {
            //try
            //{
            if (r_ == null)
            {
                error = "Rectangular is not defined.";
                return null;
            }
            if (r_.Width <= Settings.ImageProcessing.CoordinateDeviationMargin || r_.Height <= Settings.ImageProcessing.CoordinateDeviationMargin)
            {
                error = "Rectangular is malformed.";
                return null;
            }
            PointF point0 = new PointF(0, 0);
            if (floatingAnchorId != null)
            {
                PointF? p0;
                p0 = GetFloatingAnchorPoint0((int)floatingAnchorId);
                if (p0 == null)
                {
                    error = "FloatingAnchor[" + floatingAnchorId + "] is not found.";
                    return null;
                }
                point0 = (PointF)p0;
            }
            Template.RectangleF r = new Template.RectangleF(r_.X + point0.X, r_.Y + point0.Y, r_.Width, r_.Height);
            error = null;
            switch (valueType)
            {
                case Template.ValueTypes.PdfText:
                    return Pdf.GetTextByTopLeftCoordinates(PdfCharBoxs, r.GetSystemRectangleF());
                case Template.ValueTypes.OcrText:
                    //return Ocr.GetTextByTopLeftCoordinates(ActiveTemplateOcrCharBoxs, r.GetSystemRectangleF());//for unknown reason tesseract often parses a whole page much worse than a fragment and so ActiveTemplateOcrCharBoxs give not reliable result.
                    return Ocr.This.GetText(ActiveTemplateBitmap, r.GetSystemRectangleF());
                case Template.ValueTypes.ImageData:
                    using (Bitmap rb = getRectangleFromActiveTemplateBitmap(r.X / Settings.ImageProcessing.Image2PdfResolutionRatio, r.Y / Settings.ImageProcessing.Image2PdfResolutionRatio, r.Width / Settings.ImageProcessing.Image2PdfResolutionRatio, r.Height / Settings.ImageProcessing.Image2PdfResolutionRatio))
                    {
                        return new ImageData(rb);
                    }
                default:
                    throw new Exception("Unknown option: " + valueType);
            }
            //}
            //catch(Exception e)
            //{
            //    error = Log.GetExceptionMessage(e);
            //}
            //return null;
        }
        static Dictionary<Bitmap, ImageData> bs2id = new Dictionary<Bitmap, ImageData>();

        Dictionary<string, string> fieldNames2texts = new Dictionary<string, string>();

        public static string NormalizeText(string value)
        {
            if (value == null)
                return null;
            value = FieldPreparation.RemoveNonPrintablesRegex.Replace(value, " ");
            value = Regex.Replace(value, @"\s+", " ");
            value = value.Trim();
            return value;
        }
    }
}