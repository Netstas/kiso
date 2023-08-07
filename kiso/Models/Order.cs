using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.Xml;

namespace kiso.Models
{
    public partial class Order
    {
        public int Id { get; set; }

        [Display(Name = "Mã đơn hàng")]
        public string? MaDonHang { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Thanh toán")]
        public bool? Payment { get; set; }

        [Display(Name = "Loại thanh toán")]
        public int? TypePay { get; set; }

        [Display(Name = "Vận chuyển")]
        public int? Transport { get; set; }

        [Display(Name = "Ngày vẫn chuyển")]
        public DateTime? TransportDate { get; set; }

        [Display(Name = "Trạng thái")]
        public int? Status { get; set; }

        [Display(Name = "Đã xem")]
        public bool Viewed { get; set; }

        [Display(Name = "Thông tin khách hàng Họ và tên")]
        public string? CustomerInfoFullname { get; set; }

        [Display(Name = "Thông tin địa chỉ khách hàng")]
        public string? CustomerInfoAddress { get; set; }

        [Display(Name = "Thông tin số điện thoại khách hàng")]
        public string? CustomerInfoMobile { get; set; }

        [Display(Name = "Thông tin email khách hàng")]
        public string? CustomerInfoEmail { get; set; }

        [Display(Name = "Nội dung thông tin khách hàng")]
        public string? CustomerInfoBody { get; set; }

        [Display(Name = "Thông tin khách hàng là thành viên mới")]
        public bool? CustomerInfoIsNewMember { get; set; }

        [Display(Name = "Thanh toán trước")]
        public int? ThanhToanTruoc { get; set; }

        [Display(Name = "Miễn phí giao hàng")]
        public int ShipFee { get; set; }
        [Display(Name = "Thành phố")]
        public int? CityId { get; set; }
        [Display(Name = "Huyện")]
        public int? DistrictId { get; set; }

        [Display(Name = "Xã")]
        public int? WardId { get; set; }

        [Display(Name = "Thông tin khách hàng Khác")]
        public string CustomerInfoOrther { get; set; }
        [Display(Name = "Thành phố")]
        public virtual City? City { get; set; }
        [Display(Name = "Huyện")]
        public virtual District? District { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        [Display(Name = "Xã")]
        public virtual Ward? Ward { get; set; }
        public Order()
        {
            Random random = new Random();

            int randomNumber = random.Next();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            string randomString = new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            MaDonHang = randomString;
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
