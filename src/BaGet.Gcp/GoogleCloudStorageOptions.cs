using System.ComponentModel.DataAnnotations;
using BaGet.Core;
using BaGet.Core.Configuration;

namespace BaGet.Gcp
{
    public class GoogleCloudStorageOptions : StorageOptions
    {
        [Required]
        public string BucketName { get; set; }
    }
}
