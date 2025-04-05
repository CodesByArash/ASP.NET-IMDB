using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Enums
{
    public enum MovieScoreEnum
    {
        zero  = 0,
        one   = 1,
        two   = 2,
        three = 3,
        four  = 4,
        five  = 5,
    }

    public enum CastRole
    {
        Actor,             // بازیگر
        Director,          // کارگردان
        Writer,            // نویسنده
        Producer,          // تهیه‌کننده
        Cinematographer,   // فیلمبردار / مدیر تصویربرداری
        Editor,            // تدوین‌گر
        Composer,          // آهنگساز
        SoundDepartment,   // بخش صدا
        CostumeDesigner,   // طراح لباس
        MakeupArtist,      // گریمور
        VisualEffects,     // جلوه‌های ویژه
        ArtDirector,       // مدیر هنری / طراحی صحنه
        CastingDirector,   // مدیر انتخاب بازیگر
        Stunt,             // بدل‌کار
        ProductionManager, // مدیر تولید
        LightingDepartment,// بخش نورپردازی
        LocationManager,   // مسئول لوکیشن
        Other              // سایر نقش‌ها
    }
}