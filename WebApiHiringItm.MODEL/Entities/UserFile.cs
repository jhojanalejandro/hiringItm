﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WebApiHiringItm.MODEL.Entities
{
    public partial class UserFile
    {
        public Guid Id { get; set; }
        public Guid UserFileType { get; set; }
        public string FileData { get; set; }
        public Guid? RollId { get; set; }
        public string OwnerFirm { get; set; }
        public string UserfileName { get; set; }
        public Guid? UserId { get; set; }
        public string FileType { get; set; }
        public string FileNameC { get; set; }

        public virtual Roll Roll { get; set; }
        public virtual UserT User { get; set; }
        public virtual UserFileType UserFileTypeNavigation { get; set; }
    }
}