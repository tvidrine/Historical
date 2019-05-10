// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 08/09/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Providers;
using Apollo.Core.Domain.Document;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Responses;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using WebSupergoo.ABCpdf11;
using WebSupergoo.ABCpdf11.Operations;
using Blip = DocumentFormat.OpenXml.Drawing.Blip;
using Break = DocumentFormat.OpenXml.Wordprocessing.Break;
using Hyperlink = DocumentFormat.OpenXml.Wordprocessing.Hyperlink;
using Picture = DocumentFormat.OpenXml.Drawing.Pictures.Picture;

namespace Apollo.Reporting.Providers
{
    public class DocumentProvider : IDocumentProvider
    {
        private readonly ILogManager _logManager;
        private readonly IDictionary<MergeDocumentFieldTypes, Action<FieldToMerge, object>> _valueSetters;

        public DocumentProvider(ILogManager logManager)
        {
            WebSupergoo.WordGlue.Utilities.Settings.InstallLicense(@"CldvcmRHbHVlPT2oAAAAAQAAAAkxNTcxMzczOTgPWk9PTSBBdWRpdHMgTExDAQAAAIQBU3JjOkNsZXZlcmJyaWRnZQ0KUGlkOjEwNDQwNg0KUmVmOjE1NzEzNzM5OC0xDQpEYXQ6MjAxOC0xMi0xN1QyMTo0OTozMi42ODQ5OTdaDQpQcm86V29yZEdsdWUgLSBTaW5nbGUgTGljZW5zZQ0KTGljOlpPT00gQXVkaXRzIExMQw0K|@|gAAAAGqKtwXYqJh5AGi/WiJhKSrpq/gszxURpVVoRAbLjy9MNyssd5ZKvqCHhbbZSjLhHTMxvti4EskF3/MEzzjJZfGqXzx4Tij8WbAaOsY70UJxRSh0YsOGBlUfcTVtIi1gbB5yzpwxhcqiXt+sdL3FW2U6P6TnkcxfTnCu9MxzAKYh");
            _logManager = logManager;
            _valueSetters = new Dictionary<MergeDocumentFieldTypes, Action<FieldToMerge, object>>
            {
                {MergeDocumentFieldTypes.Image, SetImageValue},
                {MergeDocumentFieldTypes.String, SetStringValue},
                {MergeDocumentFieldTypes.Hyperlink, SetHyperlinkValue }
            };
        }
        public Task<GetResponse<MemoryStream>> MergeDocumentAsync(Stream documentStream, IDictionary<string, MergeDocumentField> values)
        {
            return Task.Run(() =>
            {
                var response = new GetResponse<MemoryStream> {Content = new MemoryStream()};

                try
                {
                    using(var ms = new MemoryStream())
                    {
                        documentStream.CopyTo(ms);
                        
                        using (var document = WordprocessingDocument.Open(ms, true))
                        {
                            // 1. Get the fields to merge
                            var fields = GetFieldsToMerge(document);

                            // 2. Set the values
                            SetValues(values, fields);


                            // 3. Save new document with merged values
                            document.MainDocumentPart.Document.Save();
                            document.Save();
                        }

                        ms.Position = 0;
                        
                        using (var wordGlue = new WebSupergoo.WordGlue.Doc(ms))
                        {
                            var tempFileName = Path.GetTempFileName();
                            var xpsFilename = tempFileName.Replace("tmp", "xps");
                            File.Move(tempFileName, xpsFilename);

                            wordGlue.SaveAs(xpsFilename);

                            using (var pdf = new Doc())
                            {
                                //wordGlue.Render(pdf);
                                pdf.Read(xpsFilename);

                                if (values.Values.Any(v => v.FieldType == MergeDocumentFieldTypes.Hyperlink))
                                    ProcessHyperLink(pdf,
                                        values.Values.Where(v => v.FieldType == MergeDocumentFieldTypes.Hyperlink));

                                pdf.Save(response.Content);
                            }
                            File.Delete(xpsFilename);
                        }
                    }

                }
                catch (Exception e)
                {
                    _logManager.LogError(e, "DocumentMergeProvider.MergeDocument");
                    response.AddError(e);
                }

                return response;
            });
 
        }

        public void SaveBatch(string fileName, MemoryStream document, FileTypes fileType)
        {
            // 1. Check to see we need to create the file
            if (!File.Exists(fileName))
            {
                File.WriteAllBytes(fileName, document.ToArray());
            }

            // 2. If it exists, append the new file
            else
            {
                using (var existingDocument = new Doc())
                {
                    using (var fs = File.Open(fileName, FileMode.Open))
                    {
                        existingDocument.Read(fs);

                        using (var docToAppend = new Doc())
                        {
                            document.Position = 0;
                            docToAppend.Read(document);
                            existingDocument.Append(docToAppend);
                        }
                    }

                    existingDocument.Save(fileName);
                }
            }

        }

        private IReadOnlyCollection<FieldToMerge> GetFieldsToMerge(WordprocessingDocument document)
        {
            // Get the Elements that have a tag value
            // Create a tuple with the tag value and the element
            var mainDocumentPart = document.MainDocumentPart;
            var fields = mainDocumentPart.Document.Body.Descendants<SdtElement>()
                .Where(e => e.SdtProperties.Elements<Tag>().Any())
                .Select(e => new FieldToMerge(
                    mainDocumentPart,
                    e.SdtProperties.GetFirstChild<Tag>().Val.Value, 
                    e))
                .ToList();
                    
            return fields;
        }
        private void SetValues(IDictionary<string, MergeDocumentField> values, IReadOnlyCollection<FieldToMerge> fields)
        {

            // Process the defined fields.  We may have multiple fields per value.
            foreach (var mergeField in values)
            {
                if (fields.Any(f => f.Tag.Equals(mergeField.Key)))
                {
                    fields.Where(f => f.Tag.Equals(mergeField.Key))
                        .ToList()
                        .ForEach(f =>
                            _valueSetters[mergeField.Value.FieldType](f, mergeField.Value.Value));
                   
                }
            }

        }
        private void SetImageValue(FieldToMerge field, object value)
        {
            if (value == null)
                return;

            var blipElement = field.Element.Descendants<Blip>().First();
            var sdtImage = field.Element.Descendants<Drawing>().First();
           
            if (blipElement != null)
            {
                var imageId = blipElement.Embed.Value;

                // Resize image to fit in container
                var imageData = ResizeImage((byte[])value, 
                    sdtImage.Inline.Extent.Cx, 
                    sdtImage.Inline.Extent.Cy);

                // Reset content size to match image so the image is not distorted.
                sdtImage.Descendants<Picture>().First()
                    .ShapeProperties.Transform2D.Extents.Cx = imageData.WidthInEmu;
                sdtImage.Descendants<Picture>().First()
                    .ShapeProperties.Transform2D.Extents.Cy = imageData.HeightInEmu;

                sdtImage.Inline.Extent.Cx = imageData.WidthInEmu;
                sdtImage.Inline.Extent.Cy = imageData.HeightInEmu;

                // Get the ImagePart
                if (field.DocumentPart.GetPartById(imageId) is ImagePart imagePart)
                {
                    using (var binaryWriter = new BinaryWriter(imagePart.GetStream()))
                    {
                        binaryWriter.Write(imageData.Data);
                    }
                }

            }
        }

        private void SetStringValue(FieldToMerge field, object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                field.Element.Remove();

                return;
                
            }

            var first = field.Element.Descendants<Text>().First();
            var run = first.Parent;
            run.Descendants<Text>()
                .Skip(1)
                .ToList()
                .ForEach(e => e.Remove());

            var lines = value.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            for (var i = 0; i < lines.Length; i++)
            {
                if (i == 0)
                {
                    var textElement = run.Descendants<Text>().First();
                    var newElement = textElement.CloneNode(true);
                    ((Text) newElement).Text = lines[0];
                    textElement.InsertAfterSelf(newElement);
                    textElement.Remove();
                }
                else
                    run.AppendChild(new Text(lines[i]));

                if (i != lines.Length - 1)
                    run.AppendChild(new Break());
            }
        }
        private void SetHyperlinkValue(FieldToMerge field, object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                field.Element.Remove();

                return;
            }

            var hLink = field.Element.Descendants<Hyperlink>().First();

            if (hLink != null)
            {
                var relationId = hLink.Id;
                var hr = field.DocumentPart.HyperlinkRelationships.FirstOrDefault(a => a.Id == relationId);

                if (hr != null)
                {
                    field.DocumentPart.DeleteReferenceRelationship(hr);
                    field.DocumentPart.AddHyperlinkRelationship(new Uri(value.ToString(), UriKind.Absolute), true, relationId);
                }


            }

        }
        private PictureContentData ResizeImage(byte[] imageData, float newWidthInEmu, float newHeightInEmu)
        {
            // Content height and width are being passed in EM units.
            // Convert the new height and width to pixels

            const int emusPerInch = 914400;

            using (var ms = new MemoryStream(imageData))
            {
                var image = Image.FromStream(ms);
                
                return new PictureContentData(
                    imageData,
                    (int)(image.Width / image.HorizontalResolution * emusPerInch),
                    (int)(image.Height / image.VerticalResolution * emusPerInch)
                );

            }


        }

        private struct FieldToMerge
        {
            public FieldToMerge(MainDocumentPart documentPart, string tag, SdtElement element)
            {
                DocumentPart = documentPart;
                Tag = tag;
                Element = element;
            }

            public MainDocumentPart DocumentPart { get;  }
            public string Tag { get; }
            public SdtElement Element { get; }
        }
        private struct PictureContentData
        {
            public PictureContentData(byte[] data, int widthInEmu, int heightInEmu)
            {
                Data = data;
                WidthInEmu = widthInEmu;
                HeightInEmu = heightInEmu;
            }

            public int HeightInEmu { get; }
            public byte[] Data { get; }
            public int WidthInEmu { get; }
        }

        #region WordGlue.Net Work around for hyperlinks
        private void ProcessHyperLink(Doc pdf, IEnumerable<MergeDocumentField> values)
        {
            if (values.Any(v => v.Value == null))
                return;
            
            for (var i = 0; i < pdf.PageCount; i++)
            {
                pdf.PageNumber = i + 1;

                foreach (var rect in GetColoredText(pdf, "5 99 193"))
                {
                    pdf.Rect.String = rect.String;
                    pdf.FrameRect();
                    AddLinkToUri(pdf, rect, values.First().Value.ToString());
                }
            }
        }
        private List<XRect> GetColoredText(Doc doc, string color)
        {
            var rects = new List<XRect>();
            var op = new TextOperation(doc);
            op.PageContents.AddPages(doc.PageNumber);
            var txt = op.GetText();
            var fragments = op.Select(0, txt.Length);
            var link = new List<TextFragment>();
            foreach (var fragment in fragments)
            {
                var isLink = fragment.FontColor.ToString() == color;
                if ((!isLink) && (link.Count > 0))
                {
                    rects.AddRange(op.Group(link)
                        .Select(@group => @group.Rect));
                    link.Clear();
                }
                if (isLink)
                    link.Add(fragment);
            }
            if (link.Count > 0)
            {
                rects.AddRange(op.Group(link)
                    .Select(@group => @group.Rect));
            }
            return rects;
        }
        static int AddLinkToUri(Doc doc, XRect rect, string uri)
        {
            var id = doc.AddObject("<< /Type /Annot /Subtype /Link /A << /Type /Action /S /URI /URI () >> /Border [0 0 0] >>");
            doc.SetInfo(doc.Page, "/Annots*[]:Ref", id);
            doc.SetInfo(id, "/Rect:Rect", doc.Rect.String);
            doc.SetInfo(id, "/A/URI:Text", uri);
            return id;
        }
        #endregion WordGlue.Net Work around for hyperlinks
    }
}