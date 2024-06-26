using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web.ClientControls;

namespace WebAzureReportBlobStorageExample.Services {
    public class CustomReportStorageWebExtension : DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension {
        const string DisplayNameMetadataKey = "displayName";
        static byte[] EmptyReportLayout;
        BlobContainerClient Container => customReportBlobContainerClientProvider.GetBlobContainerClient();
        private readonly CustomReportBlobContainerClientProvider customReportBlobContainerClientProvider;

        static CustomReportStorageWebExtension() {
            using(var report = new XtraReport()) {
                using(var ms = new MemoryStream()) {
                    report.SaveLayoutToXml(ms);
                    EmptyReportLayout = ms.ToArray();
                }
            }
        }
        public CustomReportStorageWebExtension(CustomReportBlobContainerClientProvider customReportBlobContainerClientProvider) {
            this.customReportBlobContainerClientProvider = customReportBlobContainerClientProvider;
        }

        public override async Task<Dictionary<string, string>> GetUrlsAsync() {
            Dictionary<string, string> reports = new Dictionary<string, string>();
            try {
                await foreach(BlobItem blob in Container.GetBlobsAsync(BlobTraits.Metadata)) {
                    var displayName = blob.Metadata.ContainsKey(DisplayNameMetadataKey) ? blob.Metadata[DisplayNameMetadataKey] : null;
                    reports.Add(blob.Name, displayName ?? blob.Name);
                }
            } catch(Azure.RequestFailedException e) {
                System.Diagnostics.Debug.WriteLine("Operation failed." + e.Message);
            }
            return reports;
        }

        public override bool IsValidUrl(string url) {
            return true;
        }
        public override async Task<byte[]> GetDataAsync(string url) {
            try {
                if(string.IsNullOrEmpty(url))
                    return EmptyReportLayout;

                var blob = Container.GetBlobClient(url);
                if(!await blob.ExistsAsync())
                    throw new ArgumentException("Report not found", "url");
                var content = await blob.DownloadContentAsync();
                return content.Value.Content.ToArray();
            } catch(RequestFailedException e) {
                System.Diagnostics.Debug.WriteLine("Operation failed. " + e.Message);
                throw;
            }
        }

        public override bool CanSetData(string url) {
            var blob = Container.GetBlobClient(url);
            return blob.Exists();
        }

        public override async Task SetDataAsync(XtraReport report, string url) {
            var blob = Container.GetBlobClient(url);
            if(await blob.ExistsAsync()) {
                using(var stream = new MemoryStream()) {
                    report.SaveLayoutToXml(stream);
                    stream.Position = 0;
                    await blob.UploadAsync(stream);
                }
            } else {
                new FaultException($"There is no report with specified id anymore. Try to use 'Save As' action to keep your changes.");
            }
        }

        public override async Task<string> SetNewDataAsync(XtraReport report, string defaultUrl) {
            string id = defaultUrl + "_" + Guid.NewGuid().ToString("N");
            string displayName = defaultUrl;
            var blob = Container.GetBlobClient(id);
            using(var stream = new MemoryStream()) {
                report.SaveLayoutToXml(stream);
                stream.Position = 0;
                await blob.UploadAsync(stream);
                await blob.SetMetadataAsync(new Dictionary<string, string> { [DisplayNameMetadataKey] = displayName });
            }
            return id;
        }
    }
}
