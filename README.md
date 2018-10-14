## PdfDocumentParser

### Prehistory
Once I needed to parse bunches of invoices issued by different companies and provided in PDF files. Invoices had predictable layouts so having a parsing template programmed for every layout they could be parsed very well. In the next phase a GUI template editor was developed to allow creating and debugging templates just with a mouse. 
Later, the entire application was split onto a versatile set of PDF parsing routines (PdfDocumentParser) and a desktop application (InvoiceParser) that implements a custom processing routine. To keep PdfDocumentParser as flexible as possible, some important but restrictive features like keeping templates are left in the custom application. 
The main idea is that anybody who would use PdfDocumentParser, should not look much into PdfDocumentParser itself, but learn its usage from InvoiceParser instead.

### Overview
PdfDocumentParser is a parsing engine designed to easily extract demanded text/images from PDF documents that conform to a predefined graphic layout - such as invoices, CV's, forms and the like.

PdfDocumentParser is a .NET library that can be incorporated into a custom application hopefully without need of change. It provides:
- a template editor where parsing templates can be created and debugged in a visual and easy manner;
- a parsing engine API that can be called while processing a PDF file and return parsed data back to the calling code;

The main goal of PdfDocumentParser is to be used in applications that do periodic parsing bunches of PDF files. 

### Technology
The main approach of parsing is based on finding certain text or image fragments in PDF pages and then extracting text/images located relatively to those fragments.

Within this scope PdfDocumentParser implements the following strategies:
- PDF entity processing (the main way to operate with text in PdfDocumentParser);
- OCR (intended for scanned documents);
- image search (used either in native and scanned PDF documents);

### InvoiceParser
[Invoice Parser](https://github.com/sergeystoyan/PdfDocumentParser/tree/lib%2Bcustomization/InvoiceParser) is a custom desktop application based on the leverages provided by [PdfDocumentParser](https://github.com/sergeystoyan/PdfDocumentParser) and developed for certain needs. Most likely it will not fit your needs from scratch but can be used as a sample.

### Support
Contact me through github if you want me to upgrade PdfDocumentParser or develop a custom application based on it.

[More details...](https://sergeystoyan.github.io/PdfDocumentParser/)
