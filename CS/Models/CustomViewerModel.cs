using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.XtraReports.Web.WebDocumentViewer;

namespace WebAzureReportBlobStorageExample.Models {
    public class CustomViewerModel {
        public WebDocumentViewerModel ViewerModel { get; set; }
        public Dictionary<string, string> AvailableReports { get; set; }
        public string ReportName { get; set; }
    }
}
