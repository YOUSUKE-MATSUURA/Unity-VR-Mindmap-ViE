using System.IO;
using System.Runtime.CompilerServices;

namespace UIExperimentVR
{
    //クラスLibraryForVRTextbookは静的メソッドをまとめたライブラリーとして扱います。
    public class LibraryForVRTextbook
    {
        //静的メソッドGetSourceFileNameは、自身を呼んだ元のソースファイルの名前を取得します。
        public static string GetSourceFileName([CallerFilePath] string sourceFilePath = "")
            => Path.GetFileName(sourceFilePath.Replace(@"\", "/"));
        //静的メソッドGetCallerMemberは、自身を呼んだ元のメソッド名を取得します。
        public static string GetCallerMember([CallerMemberName] string memberName = "") 
            => memberName;
    }
    
}
