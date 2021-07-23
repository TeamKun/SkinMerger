using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace SkinMerger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var srcDir = Path.GetFullPath("素材");
                var dstDir = Path.GetFullPath("出力");
                Directory.CreateDirectory(srcDir);
                Directory.CreateDirectory(dstDir);

                if (args.Length == 0)
                {
                    Console.WriteLine("exeに服スキンをドラッグ・アンド・ドロップしてください");
                    Console.ReadLine();
                }

                var clothes = Path.GetFullPath(args[0]);
                var list = Directory.GetFiles(srcDir, "*.png", SearchOption.TopDirectoryOnly);

                var source2 = new Bitmap(clothes);

                int i = 0;
                foreach (var path in list)
                {
                    var name = Path.GetFileName(path);
                    Console.Write($"スキン「{name}」の合成({++i}/{list.Length})...");
                    try
                    {
                        var source1 = new Bitmap(Path.Combine(srcDir, name));
                        var target = new Bitmap(source1.Width, source1.Height, PixelFormat.Format32bppArgb);
                        var graphics = Graphics.FromImage(target);
                        graphics.CompositingMode = CompositingMode.SourceOver;

                        graphics.DrawImage(source1, 0, 0);
                        graphics.DrawImage(source2, 0, 0);

                        target.Save(Path.Combine(dstDir, $"{Path.GetFileNameWithoutExtension(name)}.png"), ImageFormat.Png);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"失敗 - 原因: {e.Message}");
                        Console.WriteLine(e.StackTrace);
                    }
                    Console.WriteLine($"成功");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"エラー: {e.Message}");
            }

            Console.WriteLine($"完了！ ×キーで終了してOKです");
            Console.ReadLine();
        }
    }
}