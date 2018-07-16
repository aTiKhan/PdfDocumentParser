using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data.Linq;
using System.Linq;
using System.Drawing;
using System.Collections.Specialized;

namespace Cliver.PdfDocumentParser
{
    public partial class Settings
    {
        [Cliver.Settings.Obligatory]
        public static readonly TemplatesSettings Templates;

        public class TemplatesSettings : Cliver.Settings
        {
            public Template InitialTemplate;
            public List<Template> Templates = new List<Template>();//preserving order of matching: only the first match is to be applied

            public override void Loaded()
            {
                //if (Templates.Count < 1)
                //    Templates.Add(CreateInitialTemplate());
            }

            public override void Saving()
            {
            }

            public Template CreateInitialTemplate()
            {
                if (InitialTemplate != null)
                {
                    string ts = SerializationRoutines.Json.Serialize(Settings.Templates.InitialTemplate);
                    return SerializationRoutines.Json.Deserialize<Template>(ts);
                }
                if (Settings.ImageProcessing == null)
                    Config.PreloadField("ImageProcessing");
                if (Settings.Appearance == null)
                    Config.PreloadField("Appearance");
                return new Template
                {
                    Active = true,
                    AutoDeskew = false,
                    BrightnessTolerance = Settings.ImageProcessing.BrightnessTolerance,
                    DifferentPixelNumberTolerance = Settings.ImageProcessing.DifferentPixelNumberTolerance,
                    Fields = new List<Template.Field> {
                        new Template.Field { Name = "INVOICE#" , Rectangle=new Template.RectangleF(0,0,10,10)},
                        new Template.Field { Name = "JOB#", Rectangle=new Template.RectangleF(0,0,10,10) },
                        new Template.Field { Name = "PO#", Rectangle=new Template.RectangleF(0,0,10,10) },
                        new Template.Field { Name = "COST" , Rectangle=new Template.RectangleF(0,0,10,10)},
                    },
                    Name = "-new-",
                    FileFilterRegex = new Regex(@"\.pdf$", RegexOptions.IgnoreCase),
                    FindBestImageMatch = false,
                    FloatingAnchors = new List<Template.FloatingAnchor>(),
                    DocumentFirstPageRecognitionMarks = new List<Template.Mark>(),
                    PagesRotation = Template.PageRotations.NONE,
                    TestPictureScale = Settings.Appearance.TestPictureScale,
                    TestFile = "",
                };
            }
        }
    }
}
