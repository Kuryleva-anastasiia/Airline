using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Airline.Models
{
    internal class PartialClasses
    {

        [MetadataType(typeof(ПолетMetadata))]
        public partial class Полет
        {
        }

        [MetadataType(typeof(ВходMetadata))]
        public partial class Вход
        {
        }

        [MetadataType(typeof(ЗаявкаMetadata))]
        public partial class Заявка
        {
        }
    }

}