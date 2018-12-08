using System;
using FLS.ServerSide.SharingObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FLS.ServerSide.EFCore.Entities
{
    public partial class FLSDbContext : DbContext
    {
        public virtual DbSet<CapitalCost> CapitalCost { get; set; }
        public virtual DbSet<CurrentInStock> CurrentInStock { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<ExpenditureDocket> ExpenditureDocket { get; set; }
        public virtual DbSet<ExpenditureDocketDetail> ExpenditureDocketDetail { get; set; }
        public virtual DbSet<ExpenditureDocketType> ExpenditureDocketType { get; set; }
        public virtual DbSet<FarmingSeason> FarmingSeason { get; set; }
        public virtual DbSet<FarmingSeasonHistory> FarmingSeasonHistory { get; set; }
        public virtual DbSet<FarmRegion> FarmRegion { get; set; }
        public virtual DbSet<FeedConversionRate> FeedConversionRate { get; set; }
        public virtual DbSet<FishPond> FishPond { get; set; }
        public virtual DbSet<InStockHistory> InStockHistory { get; set; }
        public virtual DbSet<LivestockHistoryDetail> LivestockHistoryDetail { get; set; }
        public virtual DbSet<Personnel> Personnel { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductGroup> ProductGroup { get; set; }
        public virtual DbSet<ProductSubgroup> ProductSubgroup { get; set; }
        public virtual DbSet<ProductUnit> ProductUnit { get; set; }
        public virtual DbSet<ProductUnitProduct> ProductUnitProduct { get; set; }
        public virtual DbSet<StockHistoryDetail> StockHistoryDetail { get; set; }
        public virtual DbSet<StockIssueDocket> StockIssueDocket { get; set; }
        public virtual DbSet<StockIssueDocketDetail> StockIssueDocketDetail { get; set; }
        public virtual DbSet<StockIssueDocketType> StockIssueDocketType { get; set; }
        public virtual DbSet<StockReceiveDocket> StockReceiveDocket { get; set; }
        public virtual DbSet<StockReceiveDocketDetail> StockReceiveDocketDetail { get; set; }
        public virtual DbSet<StockReceiveDocketType> StockReceiveDocketType { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<SupplierBranch> SupplierBranch { get; set; }
        public virtual DbSet<TaxPercent> TaxPercent { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }
        public virtual DbSet<WarehouseType> WarehouseType { get; set; }

        public FLSDbContext(DbContextOptions<FLSDbContext> options)
            : base(options) { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseMySql("server=128.199.142.56;user id=hr_user_db3;password=hr_user_db3;port=5306;database=hr_db3;persistsecurityinfo=True;ConvertZeroDatetime=True;");
        //    }
        //}
        private void CreateStoredBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Query<StockReceiveDocketDetailModel>();
            modelBuilder.Query<StockIssueDocketDetailModel>();
            modelBuilder.Query<ProductUnitProductModel>();
            modelBuilder.Query<ReportLivestockHistoryDetail>();
            modelBuilder.Query<ReportFeedConversionRate>();
            modelBuilder.Query<ReportFarmingSeasonHistoryStock>();
            modelBuilder.Query<ReportFarmingSeason>();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CapitalCost>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CapitalCost1)
                    .HasColumnName("capital_cost")
                    .HasColumnType("decimal(27,9)")
                    .HasDefaultValueSql("'0.000000000'");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Month)
                    .HasColumnName("month")
                    .HasColumnType("date");

                entity.Property(e => e.PreviousCapitalCost)
                    .HasColumnName("previous_capital_cost")
                    .HasColumnType("decimal(27,9)")
                    .HasDefaultValueSql("'0.000000000'");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.WarehouseId)
                    .HasColumnName("warehouse_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<CurrentInStock>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.AmountExpect)
                    .HasColumnName("amount_expect")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductUnitId)
                    .HasColumnName("product_unit_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.WarehouseId)
                    .HasColumnName("warehouse_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.TaxCode)
                    .HasColumnName("tax_code")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Website)
                    .HasColumnName("website")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<ExpenditureDocket>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.BillCode)
                    .HasColumnName("bill_code")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.BillSerial)
                    .HasColumnName("bill_serial")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.BillTemplateCode)
                    .HasColumnName("bill_template_code")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.BillDate)
                    .HasColumnName("bill_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsReceipt)
                    .HasColumnName("is_receipt")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.ExpendDate)
                    .HasColumnName("expend_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.PartnerId)
                    .HasColumnName("partner_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PartnerName)
                    .HasColumnName("partner_name")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.StockDocketId)
                    .HasColumnName("stock_docket_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TotalAmount)
                    .HasColumnName("total_amount")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.UnpaidAmount)
                    .HasColumnName("unpaid_amount")
                    .HasColumnType("decimal(27,9)")
                    .HasDefaultValueSql("'0.000000000'");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Vat)
                    .HasColumnName("VAT")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.WarehouseId)
                    .HasColumnName("warehouse_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<ExpenditureDocketDetail>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.ExpenditureDocketId)
                    .HasColumnName("expenditure_docket_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ExpenditureTypeId)
                    .HasColumnName("expenditure_type_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.TotalAmount)
                    .HasColumnName("total_amount")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Vat)
                    .HasColumnName("VAT")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.VatPercent)
                    .HasColumnName("vat_percent")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<ExpenditureDocketType>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsReceipt)
                    .HasColumnName("is_receipt")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<FarmingSeason>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.FinishFarmDate)
                    .HasColumnName("finish_farm_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.FinishFarmDateExpected)
                    .HasColumnName("finish_farm_date_expected")
                    .HasColumnType("datetime");

                entity.Property(e => e.FishPondId)
                    .HasColumnName("fish_pond_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsFinished)
                    .HasColumnName("is_finished")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.StartFarmDate)
                    .HasColumnName("start_farm_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<FarmingSeasonHistory>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ActionDate)
                    .HasColumnName("action_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ActionType)
                    .HasColumnName("action_type")
                    .HasColumnType("int(1)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(2000)");

                entity.Property(e => e.FarmingSeasonId)
                    .HasColumnName("farming_season_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<FarmRegion>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<FeedConversionRate>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.FarmingSeasonId)
                    .HasColumnName("farming_season_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fcr)
                    .HasColumnName("FCR")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.IsAuto)
                    .HasColumnName("is_auto")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.LostPercent)
                    .HasColumnName("lost_percent")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.MassAmount)
                    .HasColumnName("mass_amount")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("product_name")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.SurveyDate)
                    .HasColumnName("survey_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Weight)
                    .HasColumnName("weight")
                    .HasColumnType("decimal(27,9)");
            });

            modelBuilder.Entity<FishPond>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.A).HasColumnType("decimal(27,9)");

                entity.Property(e => e.B).HasColumnType("decimal(27,9)");

                entity.Property(e => e.C).HasColumnType("decimal(27,9)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.D).HasColumnType("decimal(27,9)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Depth)
                    .HasColumnName("depth")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.FarmRegionId)
                    .HasColumnName("farm_region_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FishPondTypeId)
                    .HasColumnName("fish_pond_type_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.WarehouseId)
                    .HasColumnName("warehouse_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.WaterSurfaceArea)
                    .HasColumnName("water_surface_area")
                    .HasColumnType("decimal(27,9)");
            });

            modelBuilder.Entity<InStockHistory>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.AmountExpect)
                    .HasColumnName("amount_expect")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.MonthCode)
                    .HasColumnName("month_code")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductUnitId)
                    .HasColumnName("product_unit_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.WarehouseId)
                    .HasColumnName("warehouse_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<LivestockHistoryDetail>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeadstockRatio)
                    .HasColumnName("deadstock_ratio")
                    .HasColumnType("decimal(6,4)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.HistoryId)
                    .HasColumnName("history_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.LivestockId)
                    .HasColumnName("livestock_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LivestockName)
                    .HasColumnName("livestock_name")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.LivestockSize)
                    .HasColumnName("livestock_size")
                    .HasColumnType("decimal(6,4)");

                entity.Property(e => e.MassAmount)
                    .HasColumnName("mass_amount")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Weight)
                    .HasColumnName("weight")
                    .HasColumnType("decimal(27,9)");
            });

            modelBuilder.Entity<Personnel>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.BeginEmployDate)
                    .HasColumnName("begin_employ_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Birthday)
                    .HasColumnName("birthday")
                    .HasColumnType("datetime");

                entity.Property(e => e.CareerTitleId)
                    .HasColumnName("career_title_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CompareCode)
                    .IsRequired()
                    .HasColumnName("compare_code")
                    .HasColumnType("varchar(53)");

                entity.Property(e => e.ContractCode)
                    .IsRequired()
                    .HasColumnName("contract_code")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CurrentAddress)
                    .IsRequired()
                    .HasColumnName("current_address")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CurrentDistrictId)
                    .HasColumnName("current_district_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CurrentPrecinctId)
                    .HasColumnName("current_precinct_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CurrentProvinceId)
                    .HasColumnName("current_province_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.EndEmployDate)
                    .HasColumnName("end_employ_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.EthnicId)
                    .HasColumnName("ethnic_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FatherHometownId)
                    .HasColumnName("father_hometown_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasColumnName("full_name")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HometownAddress)
                    .IsRequired()
                    .HasColumnName("hometown_address")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.HometownDistrictId)
                    .HasColumnName("hometown_district_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HometownPrecinctId)
                    .HasColumnName("hometown_precinct_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HometownProvinceId)
                    .HasColumnName("hometown_province_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdIssueDate)
                    .HasColumnName("id_issue_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdIssuePlaceId)
                    .HasColumnName("id_issue_place_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdNumber)
                    .IsRequired()
                    .HasColumnName("id_number")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.IdcIssueDate)
                    .HasColumnName("idc_issue_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdcIssuePlaceId)
                    .HasColumnName("idc_issue_place_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdcNumber)
                    .IsRequired()
                    .HasColumnName("idc_number")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasColumnType("varchar(169)");

                entity.Property(e => e.MajorLevelId)
                    .HasColumnName("major_level_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NationalityId)
                    .HasColumnName("nationality_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.PlaceOfBirthId)
                    .HasColumnName("place_of_birth_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PositionId)
                    .HasColumnName("position_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ReligionId)
                    .HasColumnName("religion_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StudyLevelId)
                    .HasColumnName("study_level_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TaxCode)
                    .IsRequired()
                    .HasColumnName("tax_code")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.WaterBodyId)
                    .HasColumnName("water_body_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DefaultUnitId)
                    .HasColumnName("default_unit_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(2000)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.ProductGroupId)
                    .HasColumnName("product_group_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductSubgroupId)
                    .HasColumnName("product_subgroup_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TaxPercent)
                    .HasColumnName("tax_percent")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<ProductGroup>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(2000)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsSystem)
                    .HasColumnName("is_system")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<ProductSubgroup>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(2000)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.ProductGroupId)
                    .HasColumnName("product_group_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<ProductUnit>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.HasScale)
                    .HasColumnName("has_scale")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<ProductUnitProduct>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DefaultUnitValue)
                    .HasColumnName("default_unit_value")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductUnitId)
                    .HasColumnName("product_unit_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<StockHistoryDetail>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.HistoryId)
                    .HasColumnName("history_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductName)
                    .HasColumnName("product_name")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.ProductUnitId)
                    .HasColumnName("product_unit_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<StockIssueDocket>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.ApprovedDate)
                    .HasColumnName("approved_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ApproverCode)
                    .HasColumnName("approver_code")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CapitalCost)
                    .HasColumnName("capital_cost")
                    .HasColumnType("decimal(27,9)")
                    .HasDefaultValueSql("'0.000000000'");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CustomerName)
                    .HasColumnName("customer_name")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(2000)");

                entity.Property(e => e.DocketNumber)
                    .HasColumnName("docket_number")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.DocketTemplateCode)
                    .HasColumnName("docket_template_code")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.ExecutedDate)
                    .HasColumnName("executed_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ExecutorCode)
                    .IsRequired()
                    .HasColumnName("executor_code")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IssueDate)
                    .HasColumnName("issue_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.StockIssueDocketTypeId)
                    .HasColumnName("stock_issue_docket_type_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StockReceiveDocketId)
                    .HasColumnName("stock_receive_docket_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TotalAmount)
                    .HasColumnName("total_amount")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Vat)
                    .HasColumnName("VAT")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.WarehouseId)
                    .HasColumnName("warehouse_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<StockIssueDocketDetail>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.CapitalCost)
                    .HasColumnName("capital_cost")
                    .HasColumnType("decimal(27,9)")
                    .HasDefaultValueSql("'0.000000000'");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductUnitId)
                    .HasColumnName("product_unit_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.StockIssueDocketId)
                    .HasColumnName("stock_issue_docket_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TotalAmount)
                    .HasColumnName("total_amount")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.UnitPrice)
                    .HasColumnName("unit_price")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Vat)
                    .HasColumnName("VAT")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.VatPercent)
                    .HasColumnName("vat_percent")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<StockIssueDocketType>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ApprovalNeeded)
                    .HasColumnName("approval_needed")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(2000)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsSystem)
                    .HasColumnName("is_system")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.PickingPrice)
                    .HasColumnName("picking_price")
                    .HasColumnType("int(1)");

                entity.Property(e => e.ReceiptNeeded)
                    .HasColumnName("receipt_needed")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ReceiptTypeId)
                    .HasColumnName("receipt_type_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<StockReceiveDocket>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ActuallyReceivedCode)
                    .HasColumnName("actually_received_code")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.ActuallyReceivedDate)
                    .HasColumnName("actually_received_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.ApprovedDate)
                    .HasColumnName("approved_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ApproverCode)
                    .HasColumnName("approver_code")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(2000)");

                entity.Property(e => e.DocketNumber)
                    .HasColumnName("docket_number")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.ExecutedDate)
                    .HasColumnName("executed_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ExecutorCode)
                    .IsRequired()
                    .HasColumnName("executor_code")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.IsActuallyReceived)
                    .HasColumnName("is_actually_received")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ReceiveDate)
                    .HasColumnName("receive_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.StockIssueDocketId)
                    .HasColumnName("stock_issue_docket_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StockReceiveDocketTypeId)
                    .HasColumnName("stock_receive_docket_type_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TotalAmount)
                    .HasColumnName("total_amount")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Vat)
                    .HasColumnName("VAT")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.WarehouseId)
                    .HasColumnName("warehouse_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<StockReceiveDocketDetail>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductUnitId)
                    .HasColumnName("product_unit_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.StockReceiveDocketId)
                    .HasColumnName("stock_receive_docket_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SupplierBranchId)
                    .HasColumnName("supplier_branch_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SupplierBranchName)
                    .HasColumnName("supplier_branch_name")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.TotalAmount)
                    .HasColumnName("total_amount")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.UnitPrice)
                    .HasColumnName("unit_price")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Vat)
                    .HasColumnName("VAT")
                    .HasColumnType("decimal(27,9)");

                entity.Property(e => e.VatPercent)
                    .HasColumnName("vat_percent")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<StockReceiveDocketType>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ApprovalNeeded)
                    .HasColumnName("approval_needed")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(2000)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsSystem)
                    .HasColumnName("is_system")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.PayslipNeeded)
                    .HasColumnName("payslip_needed")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.PayslipTypeId)
                    .HasColumnName("payslip_type_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.TaxCode)
                    .HasColumnName("tax_code")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Website)
                    .HasColumnName("website")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<SupplierBranch>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsMain)
                    .HasColumnName("is_main")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.SupplierId)
                    .HasColumnName("supplier_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TaxCode)
                    .HasColumnName("tax_code")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Website)
                    .HasColumnName("website")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<TaxPercent>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DefaultWarehouseId)
                    .HasColumnName("default_warehouse_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.FarmRegionId)
                    .HasColumnName("farm_region_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.WarehouseTypeId)
                    .HasColumnName("warehouse_type_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<WarehouseType>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasColumnName("created_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deleted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedUser)
                    .HasColumnName("deleted_user")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsSystem)
                    .HasColumnName("is_system")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasColumnName("updated_user")
                    .HasColumnType("varchar(50)");
            });

            CreateStoredBuilder(modelBuilder);
        }
    }
}
