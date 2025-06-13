using core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repsotiry.seeds
{
    public static class AppDbInitializer
    {
        // ميثود الهدف منها إنها تجهز الأدمن والرولز وقت تشغيل المشروع أول مرة
        public static async Task SeedAdminAsync(UserManager<BaseUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // ✅ لو رول الأدمن مش موجود، اعمله
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            // ✅ لو رول اليوزر العادي مش موجود، اعمله
            if (!await roleManager.RoleExistsAsync("User"))
                await roleManager.CreateAsync(new IdentityRole("User"));

            // 📨 ايميل الأدمن اللي هنستخدمه في البحث أو الإنشاء
            var adminEmail = "admin@admin.com";

            // 🔍 بنشوف هل في يوزر بالإيميل ده فعلاً ولا لأ
            var admin = await userManager.FindByEmailAsync(adminEmail);

            // ❌ لو مش موجود، نبدأ نعمل الأدمن
            if (admin == null)
            {
                var newAdmin = new BaseUser
                {
                    UserName = "admin",             // 👤 اسم المستخدم
                    Email = adminEmail,             // 📧 الإيميل
                    DisplayName = "Super Admin",    // 🏷️ الاسم اللي هيظهر في البروفايل مثلاً
                    EmailConfirmed = true           // ✅ بنأكد إن الإيميل متأكد عشان مفيش لوجيك يوقفه
                };

                // 🛠️ بنعمل اليوزر بكلمة سر قوية مبدأيًا
                var result = await userManager.CreateAsync(newAdmin, "Admin@123");

                // ✅ لو الإنشاء تم بنجاح، نضيف الأدمن لرول "Admin"
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
            }
        }
    }
}
