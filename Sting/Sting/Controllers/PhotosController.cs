using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Sting.Controllers
{
    public class PhotosController : ApiController
    {
        public IEnumerable<string> Get(string type, int id)
        {
            var filePaths = Directory.GetFiles(HttpContext.Current.Server.MapPath(GetBasePath(type, id)));
            List<string> paths = new List<string>();
            foreach (string filePath in filePaths)
            {
                paths.Add(HttpContext.Current.Server.MapPath(filePath));
            }
            return paths;
        }

        public IEnumerable<string> Post(string type, int id)
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    string filePath = HttpContext.Current.Server.MapPath(GetBasePath(type, id) + Guid.NewGuid().ToString());
                    postedFile.SaveAs(filePath);
                    docfiles.Add(filePath);
                }
                return docfiles;
            }
            return new List<string>();
        }

        private string GetBasePath(string type, int id)
        {
            return Path.Combine("~/UploadedImages/", type, id.ToString());
        }
    }
}
