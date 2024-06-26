using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace WebAzureReportBlobStorageExample.Services {
    public class CustomReportBlobContainerClientProvider {
        readonly BlobContainerClient blobContainerClient;

        public CustomReportBlobContainerClientProvider(IConfiguration configuration) {
            var connectionString = configuration.GetConnectionString("ProdAzureBlobStorageAccountConnectionString");
            blobContainerClient = new BlobContainerClient(connectionString, "reports");
        }

        public BlobContainerClient GetBlobContainerClient() {
            return blobContainerClient;
        }

        public void FirstTimeInitialize() {
            blobContainerClient.CreateIfNotExists();
        }
    }
}
