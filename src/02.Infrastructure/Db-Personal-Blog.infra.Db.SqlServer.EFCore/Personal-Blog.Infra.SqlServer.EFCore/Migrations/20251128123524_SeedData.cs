using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Personal_Blog.Infra.SqlServer.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Email", "FirstName", "ImageUrl", "LastName", "Password", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { 1, "ghazal@gmail.com", "غزل", null, "مسیحا", "passGhazal", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ghazal" },
                    { 2, "mina@gmail.com", "مینا", null, "محمدی", "passMina", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mina" },
                    { 3, "reza@gmail.com", "رضا", null, "رضایی", "passReza", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "reza" },
                    { 4, "sara@gmail.com", "سارا", null, "کاظمی", "passSara", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sara" },
                    { 5, "hossein@gmail.com", "حسین", null, "رضوانی", "passHossein", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hossein" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "AuthorId", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, "تکنولوژی", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, "سفر", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 2, "سبک زندگی", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 3, "برنامه‌نویسی .NET", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 4, "کتاب و مطالعه", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 5, "سلامت و ورزش", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "ImageUrl", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 3, 4, "در این مقاله قصد داریم در قدم اول به چگونگی ایجاد یک پروژه ساده ASP.NET Core MVC بپردازیم. ابتدا باید SDK دات‌نت نصب شود، سپس یک پروژه جدید با دستور dotnet new mvc ساخته شود. پس از آن تنظیمات اتصال به دیتابیس و پیکربندی EF Core را انجام می‌دهیم. ادامه‌ی مطلب … (متن کامل را خودتان اضافه کنید)", null, "شروع کار با ASP.NET Core MVC", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 3, 4, "Entity Framework Core به ما این امکان را می‌دهد که مدل‌های C# را به جداول دیتابیس تبدیل کنیم. با مایگریشن، هر تغییر در مدل به‌صورت خودکار به دیتابیس اعمال می‌شود. در این نوشته درباره مزایا، معایب و نکات مهم EF Core بحث خواهیم کرد. ادامه‌ی مطلب …", null, "معرفی EF Core و مایگریشن‌ها", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, 2, "شمال ایران با طبیعت بکر و آب‌وهوا دلپذیر همواره مقصد بسیاری از مسافران بوده است. در این نوشته تجربه من از سفری چندروزه به استان گیلان و مازندران آورده شده است: صبح زود از تهران حرکت کردیم … ادامه مطلب …", null, "سفر به شمال ایران: تجربه‌ای متفاوت", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 2, 3, "داشتن سبک زندگی سالم نیازمند توجه به تغذیه، خواب، ورزش و آرامش ذهنی است. در این مقاله چند عادت ساده و عملی برای زندگی سالم پیشنهاد می‌شود: ۱) پیاده‌روی روزانه … ادامه مطلب …", null, "سبک زندگی سالم — عادات روزمره", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 4, 5, "خواندن کتاب یکی از بهترین روش‌ها برای بهبود مهارت‌های برنامه‌نویسی است. در این پست ۵ کتاب در حوزه دات‌نت و معماری نرم‌افزار معرفی می‌شود: ۱) Pro ASP.NET Core MVC … ادامه مطلب …", null, "۵ کتاب عالی برای توسعه‌دهندگان .NET", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 5, 6, "تغذیه مناسب و ورزش منظم به بدن انرژی می‌دهد و سلامت جسم و ذهن را تضمین می‌کند. در این مقاله نکاتی درباره رژیم غذایی، آب کافی و ورزش پیوسته گفته می‌شود … ادامه مطلب …", null, "تغذیه سالم و ورزش روزانه", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 3, 4, "پس از توسعه پروژه وب، زمان deploy و استقرار آن می‌رسد. در این مطلب گام‌های لازم برای deploy یک پروژه ASP.NET Core MVC روی سرور یا هاست بررسی شده است … ادامه مطلب …", null, "چگونه پروژه MVC را deploy کنیم؟", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 1, 2, "اگر قصد سفر به اروپا دارید، این راهنما برای شماست: مسافرت اقتصادی، انتخاب کشورها، زمان مناسب و نکات مهم … ادامه مطلب …", null, "راهنمای سفر به اروپا", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 2, 3, "زندگی پرشتاب امروزی استرس زیادی به ما تحمیل می‌کند. در این مقاله با مفهوم Mindfulness و تکنیک‌های کاهش استرس آشنا می‌شویم … ادامه مطلب …", null, "Mindfulness و کاهش استرس", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 4, 4, "Blazor چارچوب جدیدی برای ساخت رابط کاربری با C# و دات‌نت است. در این مطلب ابتدا با مفاهیم پایه آشنا می‌شویم … ادامه مطلب …", null, "شروع یادگیری Blazor", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "PostId", "Rating", "Status", "Text", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "guest1@example.com", "کاربر مهمان", 1, 5, "Confirmed", "خیلی ممنون، شروع خوبی برای پروژه بود!", new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "maryam@example.com", "مریم رضایی", 1, 4, "Pending", "مفید بود — لطفاً ادامه دهید.", new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "hossein@example.com", "حسین کریمی", 2, 5, "Confirmed", "مقاله عالی، خیلی کمک کرد.", new DateTime(2025, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2025, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "shakiba@example.com", "شکیبا نوروزی", 2, 3, "Rejected", "خوبه ولی انتظار داشتم جزئیات فنی بیشتری باشه.", new DateTime(2025, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "sara@example.com", "سارا رضوانی", 3, 5, "Confirmed", "چه خاطره خوبی!", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "reza.abassi@example.com", "رضا عباسی", 3, 4, "Pending", "دوست دارم وقتی سفر میکنم اینها رو بخونم.", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "ali.h@example.com", "علی حسینی", 4, 5, "Confirmed", "مطالب زندگی سالم خیلی لازم بود، مرسی.", new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "mina.k@example.com", "مینا کریمی", 4, 4, "Pending", "لطفاً منابع هم معرفی کنید.", new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2025, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "booklover@example.com", "کاربر عاشق کتاب", 5, 5, "Confirmed", "کتاب‌ها عالی هستند، لطفاً نقد هم بزنید.", new DateTime(2025, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2025, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "shahram@example.com", "شهرام رضائی", 5, 3, "Pending", "کتاب‌ها آشنا نبودند، لطفاً منابع انگلیسی هم معرفی کنید.", new DateTime(2025, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "maryam.n@example.com", "مریم نعمتی", 6, 4, "Confirmed", "مفید بود، به نظرم ادامه این موضوع هم مهم است.", new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "traveller@example.com", "مسافر مشتاق", 8, 5, "Confirmed", "مطلب خیلی مفید بود — امیدوارم به زودی سفر کنم!", new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, new DateTime(2025, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "peace@example.com", "کاربر آرامش", 9, 5, "Confirmed", "خیلی خوب — نیاز امروز بود.", new DateTime(2025, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, new DateTime(2025, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "devnew@example.com", "توسعه‌دهنده تازه‌کار", 10, 5, "Pending", "Blazor جالب است — آیا آموزش کامل دارید؟", new DateTime(2025, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
