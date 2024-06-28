<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/820323720/23.2.6%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1240159)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# Reporting for ASP.NET Core - Azure Report Storage

In this example, Azure Blob Storage is used as a report repository for an ASP.NET Core Reporting application with Document Viewer and Report Designer.


![Document Viewer](/images/screenshot.png)


The `CustomReportBlobContainerClientProvider` service supplies the [BlobContainerClient](https://learn.microsoft.com/en-us/dotnet/api/azure.storage.blobs.blobcontainerclient) to the `CustomReportStorageWebExtension` service. A `CustomReportStorageWebExtension` service is a [ReportStorageWebExtension]([DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension)) class descendant, and uses [BlobContainerClient](https://learn.microsoft.com/en-us/dotnet/api/azure.storage.blobs.blobcontainerclient) methods to store and retrieve report data.

Before you run the project, specify [Azure Storage Access Key](https://learn.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet) as the `ProdAzureBlobStorageAccountConnectionString` connection string in the `appsettings.json` file. 


## Files to Review

- [CustomReportBlobContainerClientProvider.cs](./CS/Services/CustomReportBlobContainerClientProvider.cs)
- [CustomReportStorageWebExtension.cs](./CS/Services/CustomReportStorageWebExtension.cs)
- [Program.cs](.CS/Program.cs)
- [appsettings.json](.CS/appsettings.json)

## Documentation

- [Cloud Integration](https://docs.devexpress.com/XtraReports/404819/cloud-integration)
- [How to Implement an Azure Storage Cache](https://docs.devexpress.com/XtraReports/404824/cloud-integration/azure-storage-cache-implementation)
- [Add a Report Storage (ASP.NET Core)](https://docs.devexpress.com/XtraReports/400211/web-reporting/asp-net-core-reporting/end-user-report-designer-in-asp-net-applications/add-a-report-storage)

## More Examples

- [WinForms and WPF Report Designers - How to Implement a Custom Report Gallery Storage](https://github.com/DevExpress-Examples/reporting-provide-custom-report-gallery-storage-in-report-designer)
- [Reporting for ASP.NET Core - How to Use the Microsoft Azure Translator Text API in Report Localization](https://github.com/DevExpress-Examples/Reporting-Register-Azure-Cognitive-Translation-Service)


<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=reporting-asp-net-core-azure-blob-storage&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=reporting-asp-net-core-azure-blob-storage&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
