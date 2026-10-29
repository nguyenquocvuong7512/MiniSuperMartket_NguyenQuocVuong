namespace MiniSupermarket.API.Dto
{
    // DTO trả về số liệu 4 Thẻ chỉ số tổng quan (KPI Summary Cards)
    public class DashboardSummaryDto
    {
        // 1. Doanh thu hôm nay (VNĐ)
        public decimal TodayRevenue { get; set; }

        // Doanh thu theo kỳ lọc
        public decimal PeriodRevenue { get; set; }

        // 2. Tổng số hóa đơn đã xuất (toàn hệ thống)
        public int TotalOrders { get; set; }

        // Số đơn theo kỳ lọc
        public int PeriodOrdersCount { get; set; }

        // 3. Số lượng khách hàng thành viên
        public int TotalCustomers { get; set; }

        // 4. Số mặt hàng sắp hết hàng (tồn kho <= 10)
        public int LowStockProductsCount { get; set; }
        public int LowStockCount => LowStockProductsCount;

        // Tổng doanh thu toàn hệ thống
        public decimal GrandTotalRevenue { get; set; }

        // Số đơn hàng hôm nay
        public int TodayOrdersCount { get; set; }

        public string GeneratedAt { get; set; } = string.Empty;
    }

    // DTO trả về số liệu doanh thu gom nhóm theo từng ngày phục vụ vẽ biểu đồ Bar Chart
    public class DailyRevenueDto
    {
        public string Date { get; set; } = string.Empty; // yyyy-MM-dd
        public decimal Revenue { get; set; }             // Doanh thu trong ngày
        public decimal TotalRevenue => Revenue;          // Hỗ trợ cả 2 tên thuộc tính
        public int OrderCount { get; set; }              // Số lượng hóa đơn
    }
}
