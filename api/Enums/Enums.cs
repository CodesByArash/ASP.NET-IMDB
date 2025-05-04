using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Enums
{
    public enum ScoreEnum
    {
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5
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

    public enum ContentTypeEnum
    {
        Movie,
        Series,
    }

    public enum GenreEnum
    {
        Action,
        Adventure,
        Animation,
        Biography,
        Comedy,
        Crime,
        Documentary,
        Drama,
        Family,
        Fantasy,
        History,
        Horror,
        Music,
        Musical,
        Mystery,
        Romance,
        SciFi,
        Sport,
        Thriller,
        War,
        Western
    }
}