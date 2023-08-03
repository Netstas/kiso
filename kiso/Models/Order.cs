using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;

namespace kiso.Models
{
    public partial class Order
    {
        public int Id { get; set; }

        public string? MaDonHang { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool? Payment { get; set; }

        public int? TypePay { get; set; }

        public int? Transport { get; set; }

        public DateTime? TransportDate { get; set; }

        public int? Status { get; set; }

        public bool? Viewed { get; set; }

        public string? CustomerInfoFullname { get; set; }

        public string? CustomerInfoAddress { get; set; }

        public string? CustomerInfoMobile { get; set; }

        public string? CustomerInfoEmail { get; set; }

        public string? CustomerInfoBody { get; set; }

        public bool? CustomerInfoIsNewMember { get; set; }

        public int? ThanhToanTruoc { get; set; }

        public int? ShipFee { get; set; }

        public int? CityId { get; set; }

        public int? DistrictId { get; set; }

        public int? WardId { get; set; }

        public string CustomerInfoOrther { get; set; }

        public virtual City? City { get; set; }

        public virtual District? District { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public virtual Ward? Ward { get; set; }
        public Order()
        {
            MaDonHang = null;
            CreateDate = DateTime.Now;
            TransportDate = DateTime.Now;
            Payment = false;
            TypePay = 1;
            Viewed = false;
            Status = 0;
            CustomerInfoEmail = "null";
            Transport = 0;
            ThanhToanTruoc = 0;
            ShipFee = 0;
            CustomerInfoOrther = "null";
            Ward = null;
            CustomerInfoIsNewMember = false;
        }
    }
}
