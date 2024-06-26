using System.Collections.Generic;
using System.Threading.Tasks;
using DevExpress.AspNetCore.Reporting.QueryBuilder;
using DevExpress.AspNetCore.Reporting.ReportDesigner;
using DevExpress.AspNetCore.Reporting.WebDocumentViewer;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web.Extensions;
using DevExpress.XtraReports.Web.ReportDesigner;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using Microsoft.AspNetCore.Mvc;
using WebAzureReportBlobStorageExample.Models;

namespace WebAzureReportBlobStorageExample.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }
        public IActionResult Error() {
            Models.ErrorModel model = new Models.ErrorModel();
            return View(model);
        }

        public async Task<IActionResult> Designer([FromServices] IReportDesignerClientSideModelGenerator reportDesignerModelGenerator) {
            Models.CustomReportDesignerModel model = new Models.CustomReportDesignerModel();
            // Create a SQL data source with the specified connection string.
            SqlDataSource ds = new SqlDataSource("NWindConnectionString");
            // Create a SQL query to access the Products data table.
            SelectQuery query = SelectQueryFluentBuilder.AddTable("Products").SelectAllColumnsFromTable().Build("Products");
            ds.Queries.Add(query);
            ds.RebuildResultSchema();
            var dataSources = new Dictionary<string, object>();
            dataSources.Add("Northwind", ds);

            var reportDesignerModel = await reportDesignerModelGenerator.GetModelAsync(new XtraReport(), dataSources, ReportDesignerController.DefaultUri, WebDocumentViewerController.DefaultUri, QueryBuilderController.DefaultUri);
            model.ReportDesignerModel = reportDesignerModel;
            return View(model);
        }

        public async Task<IActionResult> Viewer([FromForm] string ReportName,
            [FromServices] IWebDocumentViewerClientSideModelGenerator webDocumentViewerClientSideModelGenerator,
            [FromServices] ReportStorageWebExtension reportStorageWebExtension) {
            var availableReports = await reportStorageWebExtension.GetUrlsAsync();
            var viewrModel = string.IsNullOrEmpty(ReportName)
                ? await webDocumentViewerClientSideModelGenerator.GetModelAsync(new XtraReport(), WebDocumentViewerController.DefaultUri)
                : await webDocumentViewerClientSideModelGenerator.GetModelAsync(ReportName, WebDocumentViewerController.DefaultUri);
            var model = new CustomViewerModel {
                ViewerModel = viewrModel,
                AvailableReports = availableReports,
                ReportName = ReportName
            };
            return View(model);
        }
    }
}