using SmartHup.Models;
using SmartHup.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SmartHup.Models.New
{
    public abstract class CommonEntity
    {

        [Display(ResourceType = typeof(Labels), Name = "createdBy")]
        public long createdBy { get; set; }

        [Display(ResourceType = typeof(Labels), Name = "modifiedBy")]
        public long? modifiedBy { get; set; }

        [Display(ResourceType = typeof(Labels), Name = "deletedBy")]
        public long? deletedBy { get; set; }

        [Display(ResourceType = typeof(Labels), Name = "creationDate")]
        public DateTime creationDate { get; set; }

        [Display(ResourceType = typeof(Labels), Name = "modificationDate")]
        public DateTime? modificationDate { get; set; }

        [Display(ResourceType = typeof(Labels), Name = "deletedDate")]
        public DateTime? deletedDate { get; set; }

        [Display(ResourceType = typeof(Labels), Name = "status")]
        public int status { get; set; }

        [Display(ResourceType = typeof(Labels), Name = "version")]
        public int version { get; set; }

        public virtual EntityState entityStatus { get; set; }
    }

}