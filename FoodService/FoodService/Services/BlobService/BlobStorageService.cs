﻿using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Services.BlobService
{
    public class BlobStorageService : IBlobStorageService
    {
        IConfiguration configuration;

        public BlobStorageService(IConfiguration configuration
            )
        {
            this.configuration = configuration;
        }

        public void deleteBlobFolder(long id)
        {
            CloudBlobContainer blobContainer = GetCloudBlobContainerAsync();
            foreach (IListBlobItem blob in blobContainer.GetDirectoryReference(id.ToString()).ListBlobs(true))
            {
                if (blob.GetType() == typeof(CloudBlob) || blob.GetType().BaseType == typeof(CloudBlob))
                {
                    ((CloudBlob)blob).DeleteIfExists();
                }
            }
        }

        public CloudBlobContainer GetCloudBlobContainerAsync()
        {
            string storageConnectionString = configuration["storageconnectionstring"];
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("mealimages");

            if (blobContainer.CreateIfNotExists())
            {
                blobContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }
            return blobContainer;
        }

        public async Task<CloudBlockBlob> makeBlobFolderAndSaveImageAsync(long id, IFormFile image)
        {
            CloudBlobContainer blobContainer = GetCloudBlobContainerAsync();
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(id + "/" + image.Name + ".jpg");
            using (var stream = image.OpenReadStream())
            {
                await blob.UploadFromStreamAsync(stream);
            }

            return blob;
        }
    }
}

