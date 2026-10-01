using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data
{
    public class SupermarketDbContext : DbContext
    {
        public SupermarketDbContext(DbContextOptions<SupermarketDbContext> options)
            : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // === 15 DANH MỤC ===
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Rau củ quả hữu cơ", Description = "Rau xanh, củ quả sạch đạt chuẩn Organic, thu hoạch trong ngày" },
                new Category { CategoryId = 2, CategoryName = "Trái cây tươi", Description = "Trái cây nội địa và nhập khẩu tươi ngon, không hóa chất bảo quản" },
                new Category { CategoryId = 3, CategoryName = "Thịt & Thủy hải sản", Description = "Thịt heo sạch, bò thả rông, cá tươi sống đánh bắt tự nhiên" },
                new Category { CategoryId = 4, CategoryName = "Thực phẩm khô & Hạt", Description = "Các loại hạt dinh dưỡng, ngũ cốc nguyên cám, gạo lứt" },
                new Category { CategoryId = 5, CategoryName = "Sữa & Sản phẩm từ sữa", Description = "Sữa tươi thanh trùng, sữa chua hữu cơ, bơ phô mai tự nhiên" },
                new Category { CategoryId = 6, CategoryName = "Đồ uống & Trà thảo mộc", Description = "Trà hoa cúc, mật ong rừng nguyên chất, nước ép trái cây tươi" },
                new Category { CategoryId = 7, CategoryName = "Gia vị & Dầu ăn hữu cơ", Description = "Dầu ô liu, nước mắm truyền thống, hạt nêm hữu cơ, muối thảo dược" },
                new Category { CategoryId = 8, CategoryName = "Thực phẩm chay & Dinh dưỡng", Description = "Thực phẩm chay sạch, đạm thực vật, bột dinh dưỡng các loại" },
                new Category { CategoryId = 9, CategoryName = "Thực phẩm chế biến sẵn sạch", Description = "Đồ ăn vặt hữu cơ, bánh kẹo healthy, thực phẩm đóng hộp không phụ gia" },
                new Category { CategoryId = 10, CategoryName = "Mẹ và Bé (Sản phẩm hữu cơ)", Description = "Thực phẩm ăn dặm, sữa hạt, sản phẩm chăm sóc an toàn cho bé" },
                new Category { CategoryId = 11, CategoryName = "Thảo dược & Đông dược", Description = "Các loại sâm, nấm linh chi, thảo mộc bồi bổ sức khỏe tự nhiên" },
                new Category { CategoryId = 12, CategoryName = "Trứng gia cầm sạch", Description = "Trứng gà ta, trứng gà ác, trứng vịt thả vườn không chất tăng trọng" },
                new Category { CategoryId = 13, CategoryName = "Bánh mì & Ngũ cốc tươi", Description = "Bánh mì nguyên cám, bánh ngọt healthy làm từ bột mì hữu cơ" },
                new Category { CategoryId = 14, CategoryName = "Sản phẩm chăm sóc cá nhân Organic", Description = "Mỹ phẩm thiên nhiên, dầu gội, sữa tắm hữu cơ an toàn cho da" },
                new Category { CategoryId = 15, CategoryName = "Sản phẩm gia dụng thân thiện môi trường", Description = "Túi vải sinh học, ống hút tre, chất tẩy rửa hữu cơ lành tính" }
            );

            // === 15 SẢN PHẨM ===
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 104, Barcode = "8930000001041", ProductName = "Bông cải xanh hữu cơ", CategoryId = 1, Price = 42000, StockQuantity = 75, Unit = "Kg" },
                new Product { ProductId = 204, Barcode = "8930000002048", ProductName = "Dâu tây Đà Lạt sạch", CategoryId = 2, Price = 120000, StockQuantity = 40, Unit = "Hộp 500g" },
                new Product { ProductId = 304, Barcode = "8930000003045", ProductName = "Tôm sú sinh thái tự nhiên", CategoryId = 3, Price = 290000, StockQuantity = 25, Unit = "Kg" },
                new Product { ProductId = 403, Barcode = "8930000004039", ProductName = "Hạt óc chó Mỹ tách vỏ", CategoryId = 4, Price = 160000, StockQuantity = 60, Unit = "Túi 300g" },
                new Product { ProductId = 503, Barcode = "8930000005036", ProductName = "Phô mai tự nhiên hữu cơ", CategoryId = 5, Price = 85000, StockQuantity = 35, Unit = "Hộp 200g" },
                new Product { ProductId = 603, Barcode = "8930000006033", ProductName = "Nước ép táo nguyên chất không đường", CategoryId = 6, Price = 45000, StockQuantity = 50, Unit = "Chai 330ml" },
                new Product { ProductId = 701, Barcode = "8930000007016", ProductName = "Dầu ô liu nguyên chất Extra Virgin", CategoryId = 7, Price = 210000, StockQuantity = 30, Unit = "Chai 500ml" },
                new Product { ProductId = 801, Barcode = "8930000008013", ProductName = "Sườn non thực vật hữu cơ", CategoryId = 8, Price = 55000, StockQuantity = 80, Unit = "Gói 200g" },
                new Product { ProductId = 901, Barcode = "8930000009010", ProductName = "Bánh quy yến mạch nguyên cám", CategoryId = 9, Price = 65000, StockQuantity = 90, Unit = "Hộp 150g" },
                new Product { ProductId = 1001, Barcode = "8930000010014", ProductName = "Bánh ăn dặm hữu cơ vị gạo lứt", CategoryId = 10, Price = 48000, StockQuantity = 65, Unit = "Hộp 50g" },
                new Product { ProductId = 1101, Barcode = "8930000011011", ProductName = "Nấm đông cô khô hữu cơ", CategoryId = 11, Price = 115000, StockQuantity = 45, Unit = "Túi 100g" },
                new Product { ProductId = 1201, Barcode = "8930000012018", ProductName = "Trứng gà ta thả vườn hữu cơ", CategoryId = 12, Price = 45000, StockQuantity = 100, Unit = "Vỉ 10 quả" },
                new Product { ProductId = 1301, Barcode = "8930000013015", ProductName = "Bánh mì sandwich nguyên cám", CategoryId = 13, Price = 38000, StockQuantity = 40, Unit = "Túi 350g" },
                new Product { ProductId = 1401, Barcode = "8930000014012", ProductName = "Dầu gội bồ kết thảo dược thiên nhiên", CategoryId = 14, Price = 180000, StockQuantity = 20, Unit = "Chai 400ml" },
                new Product { ProductId = 1501, Barcode = "8930000015019", ProductName = "Túi lưới đựng rau củ tái sử dụng", CategoryId = 15, Price = 25000, StockQuantity = 150, Unit = "Set 3 cái" }
            );

            // === 3 KHÁCH HÀNG ===
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, CustomerName = "Nguyễn Văn An", PhoneNumber = "0901122334", Address = "123 Lê Lợi, Phường Bến Thành, Quận 1, Hồ Chí Minh", MembershipRank = "Kim Cương", RewardPoints = 1250 },
                new Customer { CustomerId = 2, CustomerName = "Trần Thị Bình", PhoneNumber = "0918877665", Address = "45 Nguyễn Chí Thanh, Quận Đống Đa, Hà Nội", MembershipRank = "Vàng", RewardPoints = 680 },
                new Customer { CustomerId = 3, CustomerName = "Lê Văn Cường", PhoneNumber = "0983344556", Address = "78 Hùng Vương, Quận Hải Châu, Đà Nẵng", MembershipRank = "Bạc", RewardPoints = 240 },
                new Customer { CustomerId = 4, CustomerName = "Phạm Thị Dung", PhoneNumber = "0935566778", Address = "12 Nguyễn Văn Cừ, Quận Ninh Kiều, Cần Thơ", MembershipRank = "Thân Thiết", RewardPoints = 85 },
                new Customer { CustomerId = 5, CustomerName = "Hoàng Minh Đức", PhoneNumber = "0977889900", Address = "56 Trần Hưng Đạo, Thành phố Nha Trang, Khánh Hòa", MembershipRank = "Mới", RewardPoints = 20 }
            );
        }
    }
}