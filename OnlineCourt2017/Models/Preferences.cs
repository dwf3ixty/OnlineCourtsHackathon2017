using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineCourt2017.Models
{
    public class Preferences
    {
        public bool FaceToFace { get; set; }
        public bool Telephone { get; set; }
        public bool VideoConference { get; set; }
        public bool Email { get; set; }

        public string Name { get; set; }

    }
}