﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WpfApp1.Models
{
    /// <summary>
    /// Модель, описывающая раздел в информационной системе.
    /// </summary>
    public partial class Sections
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        /// <summary>
        /// Id родительского раздела.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Английское название раздела, может использоваться для идентификации.
        /// </summary>
        public string SectionKey { get; set; } = null!;

        public virtual ICollection<SectionRights> SectionRights { get; set; }

        public Sections()
        {
            SectionRights = new HashSet<SectionRights>();
        }
    }
}